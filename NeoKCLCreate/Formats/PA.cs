using Hack.io.BCSV;
using Hack.io.Utility;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace NeoKCLCreate.Formats;

public class PA : BCSV
{
    #region HASHES
    public const uint HASH_FLOOR_CODE = 0x1B5BC660; // Floor_code
    public const uint HASH_WALL_CODE = 0xCE698322; // Wall_code
    public const uint HASH_SOUND_CODE = 0x6260CB3D; // Sound_code
    public const uint HASH_CAMERA_ID = 0xEB9DA075; // camera_id
    public const uint HASH_CAMERA_THROUGH = 0xB506CBCB; // Camera_through

    public const uint HASH_NAME = 0x0024EEAB; // Name (unused in-game, only for development housekeeping)
    #endregion

    public new Entry this[int index]
    {
        get => (Entry)base[index];
        set => base[index] = value;
    }

    public string[] FloorCodeOptions { get; set; } = [];
    public string[] WallCodeOptions { get; set; } = [];
    public string[] SoundCodeOptions { get; set; } = [];
    public string[] CameraThroughOptions { get; set; } = [];

    public PA()
    {
        AddRange(PreMadeFields);
    }

    protected override Entry CreateEntry() => new() { Parent = this };

    public void CopyTo(PA finalPA)
    {
        throw new NotImplementedException();
    }

    public static void CalculateFieldData(ref BCSV target)
    {
        List<Field> SortedFields = [.. target.GetFieldIter()];
        SortedFields.Sort(JGadgetFieldSort); // Ideally the calling program already has these in the correct order...

        ushort TotalEntrySize = 0;
        byte CurrentBitOffset = 0;
        for (int i = 0; i < SortedFields.Count; i++)
        {
            Field currentfield = SortedFields[i];
            // We are going to ignore AutoRecalc because it simplifies the logic
            currentfield.AutoRecalc = false;


            switch (currentfield.DataType)
            {
                case DataTypes.INT32:
                    // First, find the largest value this field uses
                    int LargestValue = 0;
                    for (int j = 0; j < target.EntryCount; j++)
                    {
                        int value = (int)target[j][currentfield];
                        int bitsNeeded = 32 - BitOperations.LeadingZeroCount((uint)value);
                        LargestValue = Math.Max(LargestValue, bitsNeeded);
                    }
                    if (LargestValue <= 0)
                        LargestValue = 1; // Minimum 1 bit

                    currentfield.Bitmask = (1u << LargestValue) - 1;

                    if (CurrentBitOffset + LargestValue > 32) // If true, we need to spill over to the next int
                    {
                        CurrentBitOffset = 0;
                        TotalEntrySize += 4;
                    }

                    currentfield.ShiftAmount = CurrentBitOffset;
                    currentfield.Bitmask <<= currentfield.ShiftAmount;
                    CurrentBitOffset += (byte)LargestValue;
                    currentfield.EntryOffset = TotalEntrySize;

                    break;
                case DataTypes.STRING:
                    // This cannot be masked, so we need to chop it out to the next segment
                    if (CurrentBitOffset > 0) // No bits used, so we can just place the string here
                    {
                        // We have bits to move
                        int CurrentEntrySize = TotalEntrySize;
                        int BitOffsetBytes = CurrentBitOffset / 8; // How many bytes have been used?
                        if (BitOffsetBytes == 0 && CurrentBitOffset % 8 > 0)
                            BitOffsetBytes++; // Force add one if we are using any bits from byte zero
                        CurrentEntrySize += BitOffsetBytes;

                        int pad = StreamUtil.CalculatePaddingLength(CurrentEntrySize, 4);
                        TotalEntrySize += (ushort)(CurrentEntrySize + pad);
                    }
                    CurrentBitOffset = 32; // sanity, but may actually be required
                    currentfield.ShiftAmount = 0;
                    currentfield.Bitmask = GetDefaultBitmask(currentfield.DataType);
                    currentfield.EntryOffset = TotalEntrySize;
                    TotalEntrySize += 4;
                    break;
                default:
                    throw new NotImplementedException("PA does not use fields other than INT32 and STRING");
            }

        }
    }


    public new class Entry : BCSV.Entry
    {
        [AllowNull]
        [Browsable(false), ReadOnly(true)]
        public PA Parent { get; set; }

        [DisplayName("Floor Code"), Description("The behaviour of this material when it is detected as a Floor (Perpendicular to Gravity)")]
        [TypeConverter(typeof(IndexedStringConverter)), IndexedStringSource("Parent.FloorCodeOptions")]
        public int FloorCode
        {
            get => (int)Data[HASH_FLOOR_CODE];
            set => Data[HASH_FLOOR_CODE] = value;
        }
        [DisplayName("Wall Code"), Description("The behaviour of this material when it is detected as a Wall (Parallel to Gravity)")]
        [TypeConverter(typeof(IndexedStringConverter)), IndexedStringSource("Parent.WallCodeOptions")]
        public int WallCode
        {
            get => (int)Data[HASH_WALL_CODE];
            set => Data[HASH_WALL_CODE] = value;
        }
        [DisplayName("Sound Code"), Description("The sounds that are made when interacting with this material")]
        [TypeConverter(typeof(IndexedStringConverter)), IndexedStringSource("Parent.SoundCodeOptions")]
        public int SoundCode
        {
            get => (int)Data[HASH_SOUND_CODE];
            set => Data[HASH_SOUND_CODE] = value;
        }
        [DisplayName("Camera Index"), Description("The group camera index for this material."), DefaultValue(255)]
        public int CameraId
        {
            get => (int)Data[HASH_CAMERA_ID];
            set => Data[HASH_CAMERA_ID] = value;
        }
        [DisplayName("Camera Collision Type"), Description("The behaviour of the camera when interacting with this material"), DefaultValue(0)]
        [TypeConverter(typeof(IndexedStringConverter)), IndexedStringSource("Parent.CameraThroughOptions")]
        public int CameraThrough
        {
            get => (int)Data[HASH_CAMERA_THROUGH];
            set => Data[HASH_CAMERA_THROUGH] = value;
        }

        [DisplayName("Material Name"), Description("The name of this material; unused by the game. You can store this in the .pa file for future editing, toggleable in the settings.")]
        public string Name
        {
            get => (string)Data[HASH_NAME];
            set => Data[HASH_NAME] = value;
        }



        private static bool ShouldSerializeFloorCode() => false;
        private static bool ShouldSerializeWallCode() => false;
        private static bool ShouldSerializeSoundCode() => false;

        private static bool ShouldSerializeName() => false;
    }



    public static readonly uint[] PreferredHashOrder = [
        HASH_CAMERA_ID,
        HASH_SOUND_CODE,
        HASH_FLOOR_CODE,
        HASH_WALL_CODE,
        HASH_CAMERA_THROUGH,
        HASH_NAME,
    ];
    private static readonly DataTypes[] HashDataTypes = [
        DataTypes.INT32,
        DataTypes.INT32,
        DataTypes.INT32,
        DataTypes.INT32,
        DataTypes.INT32,
        DataTypes.STRING,
    ];
    public static List<Field> PreMadeFields
    {
        get
        {
            List<Field> mPreMadeFields = [];
            for (int i = 0; i < PreferredHashOrder.Length; i++)
                mPreMadeFields.Add(new Field() { AutoRecalc = true, HashName = PreferredHashOrder[i], DataType = HashDataTypes[i] });
            return mPreMadeFields;
        }
    }
}


[AttributeUsage(AttributeTargets.Property)]
public sealed class IndexedStringSourceAttribute(string sourcePropertyName) : Attribute
{
    public string SourcePropertyName { get; } = sourcePropertyName;
}
public sealed class IndexedStringConverter : Int32Converter
{
    private static IReadOnlyList<string>? GetChoices(ITypeDescriptorContext? context)
    {
        if (context?.PropertyDescriptor == null || context.Instance == null)
            return null;

        if (context.PropertyDescriptor.Attributes[typeof(IndexedStringSourceAttribute)] is not IndexedStringSourceAttribute attr)
            return null;

        IEnumerable<object> instances = context.Instance switch
        {
            object[] arr => arr,
            _ => [context.Instance]
        };

        IReadOnlyList<string>? resolved = null;

        foreach (var instance in instances)
        {
            object? current = instance;

            foreach (var part in attr.SourcePropertyName.Split('.'))
            {
                if (current is null)
                    break;

                var prop = current.GetType().GetProperty(part);
                if (prop is null)
                    return null;

                current = prop.GetValue(current);
            }

            if (current is not IReadOnlyList<string> choices)
                return null;

            if (resolved is null)
                resolved = choices;
            else if (!ReferenceEquals(resolved, choices))
                return resolved;
        }

        return resolved;
    }

    public override bool GetStandardValuesSupported(ITypeDescriptorContext? context) => true;
    public override bool GetStandardValuesExclusive(ITypeDescriptorContext? context) => false;

    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext? context)
    {
        IReadOnlyList<string>? choices = GetChoices(context);
        return new StandardValuesCollection(choices != null ? [.. Enumerable.Range(0, choices.Count)] : Array.Empty<int>());
    }

    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        if (destinationType != typeof(string) || value is not int index)
            return base.ConvertTo(context, culture, value, destinationType);

        if (GetChoices(context) is IReadOnlyList<string> choices && index >= 0 && index < choices.Count)
            return choices[index];

        // Out of range will show the raw number. In case someone is crazy enough to add custom codes OR is using the wrong game profile by accident
        return index.ToString(culture);
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is not string text)
            return base.ConvertFrom(context, culture, value);

        text = text.Trim();

        if (string.IsNullOrWhiteSpace(text))
            return base.ConvertFrom(context, culture, value);

        // Fully Numeric input is converted to a raw index (kinda like Scenaristar's options)
        if (int.TryParse(text, NumberStyles.Integer, culture, out int numeric))
            return numeric;

        // Matching strings are converted. Case insensitive
        if (GetChoices(context) is IReadOnlyList<string> choices)
        {
            int idx = choices.Select((s, i) => (s, i)).FirstOrDefault(x => string.Equals(x.s, text, StringComparison.OrdinalIgnoreCase)).i;
            if (idx >= 0)
                return idx;
        }

        return base.ConvertFrom(context, culture, value);
    }
}
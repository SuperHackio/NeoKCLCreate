using Hack.io.KCL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Hack.io.KCL.KCL.PaEntry;

namespace NeoKCLCreate
{
    public partial class PresetForm : Form
    {
        public PresetForm(MainForm parent)
        {
            MF = parent;
            InitializeComponent();
            CenterToParent();

            PresetListBox.Items.Clear();
            string fn = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Presets.txt");
            if (File.Exists(fn))
            {
                string[] presets = File.ReadAllLines(fn);
                for (int i = 0; i < presets.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(presets[i]) || presets[i].StartsWith("#"))
                        continue;
                    Presets.Add(new Preset(presets[i]));
                    PresetListBox.Items.Add(Presets[Presets.Count-1].Name);
                }
            }
        }

        MainForm MF;
        List<Preset> Presets = new List<Preset>();

        public struct Preset
        {
            public string Name, Description;
            public byte Camera;
            public int Sound, Floor, Wall;
            public bool Through;

            public Preset(string source)
            {
                string[] l = source.Split(',');
                if (l.Length != 7)
                    throw new ArgumentNullException("Preset Line", "The Preset line is incomplete");

                Name = l[0];
                Camera = byte.Parse(l[1]);
                Sound = int.Parse(l[2]);
                Floor = int.Parse(l[3]);
                Wall = int.Parse(l[4]);
                Through = l[5].ToLower().Equals("true");
                Description = l[6];
            }
        }

        private void PresetForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Visible = false;
            }
        }

        private void PresetListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PresetListBox.SelectedIndex < 0)
            {
                InfoLabel.Text = "Select a Collision Preset!";
                return;
            }
            string format =
@"{0}

Camera ID:  {1}
Sound Code: {2}
Floor Code: {3}
Wall Code:  {4}
Disable Camera Collision: {5}

{6}";
            Preset p = Presets[PresetListBox.SelectedIndex];
            InfoLabel.Text = string.Format(format, p.Name, p.Camera == 255 ? "Disabled (255)" : p.Camera.ToString(), SOUND_CODES[p.Sound], FLOOR_CODES[p.Floor], WALL_CODES[p.Wall], p.Through, p.Description);
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            if (PresetListBox.SelectedIndex < 0) return;

            Preset p = Presets[PresetListBox.SelectedIndex];
            MF.SetCollisionCode(p.Camera, p.Sound, p.Floor, p.Wall, p.Through);
        }
    }
}

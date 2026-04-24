namespace NeoKCLCreate.UI;

public partial class ImportForm : Form
{
    public float PrismThickness => (float)PrismThicknessNumericUpDown.Value;
    public int ValueRounding => (int)ValueRoundNumericUpDown.Value;
    public int MaximumTrianglesPerLeaf => (int)MaxTriNumericUpDown.Value;
    public int MinimumLeafSize => (int)MinLeafSizeNumericUpDown.Value;
    public int MaximumDepth => (int)MaxDepthNumericUpDown.Value;

    public ImportForm()
    {
        InitializeComponent();

        ValueRoundNumericUpDown.ValueChange2 += ValueRoundNumericUpDown_ValueChanged;

        DialogResult = DialogResult.Abort;
    }

    private void ConvertButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
        Close();
    }

    private void ExitButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    private void ValueRoundNumericUpDown_ValueChanged(EventArgs e)
    {
        decimal v = ValueRoundNumericUpDown.Value;
        if (v.Scale > 0)
        {
            ValueRoundNumericUpDown.Value = Math.Round(v);
            return;
        }
        ValueRoundNumericUpDown.DecimalPlaces = (int)(ValueRoundNumericUpDown.Maximum - v);
    }
}

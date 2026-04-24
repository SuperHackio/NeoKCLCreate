using System.Globalization;

namespace NeoKCLCreate;

internal static class Program
{
    public static string SETTINGS_PATH => GetFromAppPath("Settings.usrs");
    public static string ASSET_PATH => GetFromAppPath("Assets");
    public const string UPDATEALERT_URL = "https://raw.githubusercontent.com/SuperHackio/NeoKCLCreate/master/NeoKCLCreate/UpdateAlert.txt";
    public const string GIT_RELEASE_URL = "https://github.com/SuperHackio/NeoKCLCreate/releases";

    public static Settings Settings { get; private set; } = new(SETTINGS_PATH);
    public static bool IsGameFileLittleEndian { get; set; } = false;
    public static bool IsUnsavedChanges { get; set; } = false;
    public static UpdateInformation? UpdateInfo;
    private static MainForm? Editor;

    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static int Main(string[] args)
    {
        ApplicationConfiguration.Initialize();
        Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);


        int idx;
        if ((idx = Array.IndexOf(args, "-lang")) >= 0 && idx + 1 != args.Length) // User specified language region
        {
            string langcode = args[idx + 1];
            try
            {
                CultureInfo culture = new(langcode);
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
            catch (CultureNotFoundException)
            {
                // Localization sheets have already gone out so adding an error message here isn't possible :(
            }
            finally
            {
                List<string> Ihatethis = [];
                Ihatethis.AddRange(args[0..idx]);
                Ihatethis.AddRange(args[(idx + 2)..args.Length]);
                args = [.. Ihatethis];
            }
        }

        UpdateInfo = UpdateInformation.IsUpdateExist(UPDATEALERT_URL);
        if (UpdateInfo is not null && UpdateInfo.Value.IsNewer() && UpdateInfo.Value.IsUpdateRequired)
        {
            MessageBox.Show(string.Format("An update for Neo KCL Create is available: {0} {1}", GIT_RELEASE_URL, UpdateInfo.ToString()), "Update Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return -5;
        }

        if (args.Any(A => A.Equals("-le")))
        {
            IsGameFileLittleEndian = true;
            args = [.. args.Where(A => !A.Equals("-le"))];
        }


        Editor = new(args);
        ReloadTheme();
        Application.Run(Editor);
        return 0;
    }

    public static void ReloadTheme()
    {
        if (Editor is null)
            return;
        Editor.SuspendLayout();
        DarkModeForms.ProgramColors.ReloadTheme(Editor);
        Editor.ResumeLayout();
    }
    public static string GetFromAppPath(string Target) => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Target);
}
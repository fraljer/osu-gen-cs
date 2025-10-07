// Program.cs
using Dark.Net;
using osu_gen_cs;

internal static class Program
{


    // The fuck? I added Dark.Net for DARKMODE compatibility.
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        DarkNet.Instance.SetCurrentProcessTheme(Theme.Auto);

        Form mainForm = new Form1();
        DarkNet.Instance.SetWindowThemeForms(mainForm, Theme.Auto, new ThemeOptions { TitleBarBackgroundColor = Color.Turquoise });
        mainForm.MaximizeBox = false;
        Application.Run(mainForm);
    }

}
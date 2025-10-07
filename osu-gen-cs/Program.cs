using Dark.Net;

namespace osu_gen_cs
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            form main = new Form1();
            Application.Run(main);
            DarkNet.Instance.SetWindowThemeForms(main, Theme.Dark);

        }
    }
}
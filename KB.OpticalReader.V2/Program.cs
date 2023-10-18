using System.Windows.Forms;

namespace KB.OpticalReader.V2
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

            //ApplicationConfiguration.Initialize();
            //Application.Run(new frmMain());

            ApplicationConfiguration.Initialize();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SplashScreen splashScreen = new SplashScreen();
            splashScreen.Show();

            //System.Threading.Thread.Sleep(3000);

            //System.Timers.Timer timer = new System.Timers.Timer(3000);
            //timer.Elapsed += (sender, e) =>
            //{

            //    timer.Stop();


            //};
            //timer.Start();

            Task.Delay(3000).Wait();

            splashScreen.Close();

            Application.Run(new frmMain());


        }

    }
}
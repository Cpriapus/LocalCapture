namespace LocalCapture
{
    internal static class Program
    {
        public static bool IsCapturing = false;
        public static Dictionary<string,CaptureHistory> CaptureHistories = new();
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Main());
        }
    }
}
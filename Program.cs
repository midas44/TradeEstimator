namespace TradeEstimator
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);

            Form1 f1 = new Form1();
            //Form2 f2 = new Form2(); 

            Application.Run(f1);
        }
    }
}
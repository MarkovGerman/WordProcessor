
using Microsoft.Extensions.Configuration;
using WordProcessorApp.Configuration;
using WordProcessorApp.Parsers;
using WordProcessorApp.Repositories;

namespace WordProcessorApp
{
    public static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}
namespace CRUDWinFormsMVP
{
    using CRUDWinFormsMVP.Presenters;
    using CRUDWinFormsMVP.Repositories;
    using CRUDWinFormsMVP.Views;
    using System.Configuration;

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
            var petView = new PetView();
            string connectionString = ConfigurationManager.ConnectionStrings["VeterinaryDatabase"].ConnectionString;
            var petRepository = new PetRepository(connectionString);
            new PetPresenter(petView, petRepository);
            Application.Run(petView);
        }
    }
}
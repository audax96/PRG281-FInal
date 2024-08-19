using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;

internal class Program
{
    private static void Main(string[] args)
    {
        DataManager dataManager = new DataManager();
        List<Vehicle> vehicles = dataManager.LoadVehicles();
        List<Trip> trips = dataManager.LoadTrips();
        List<Driver> drivers = dataManager.LoadDrivers();
        List<User> users = dataManager.LoadUser();
        List<Finance> finance = dataManager.LoadFinance();
        string loggedInName = "";
        bool loggedin = false;
        int userRole = 0;
        while (true)
        {
            if (loggedin == false)
            {
                Console.Clear();
                Console.WriteLine("         Welcome to Fleet Manager");
                Console.WriteLine("╔═════════════════════════════════════════╗");
                Console.WriteLine("║                 LOGIN                   ║");
                Console.WriteLine("╚═════════════════════════════════════════╝");
                userRole = UserManagement.Login(users ,ref loggedInName);
                loggedin = true;
            }
            else if (userRole == 1 || userRole == 2)
            {
                loggedin = LandingPages.AdminLanding(trips, vehicles, drivers, users, finance, loggedInName);
            }
            else if (userRole == 3)
            {
                loggedin = LandingPages.DriverLanding(drivers, trips, vehicles, loggedInName);
            }
            else if (userRole == 4)
            {
                LandingPages.FinanceLanding(finance, trips, loggedInName);
            }
        }  
    }
}
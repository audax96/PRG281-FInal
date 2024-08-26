using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;

internal class Program
{
    static bool keepRunning = true;
    private static void Main(string[] args)
    {
        DataManager dataManager = new DataManager();
        List<Vehicle> vehicles = dataManager.LoadVehicles();
        List<Trip> trips = dataManager.LoadTrips();
        List<Driver> drivers = dataManager.LoadDrivers();
        List<User> users = dataManager.LoadUser();
        List<Finance> finance = dataManager.LoadFinance();
        LandingPages landingPages = new LandingPages();
        Thread loadingThread = new Thread(ShowLoadingScreen);
        UserManagement.LoggedIn += LoggedInMsg;
        UserManagement.Error += ErrorMsg;
        string loggedInName = "";
        bool loggedin = false;
        int userRole = 0;

        while (true)
        {
            if (loggedin == false)
            {
                userRole = UserManagement.Login(users, ref loggedInName);
                loggedin = true;
                Thread.Sleep(1200);
                loadingThread.Start();
            }
            else if (userRole == 1 || userRole == 2)
            {
                Thread.Sleep(2500);
                keepRunning = false;
                loadingThread.Join();
                loggedin = landingPages.AdminLanding(trips, vehicles, drivers, users, finance, loggedInName);
            }
            else if (userRole == 3)
            {
                Thread.Sleep(2500);
                keepRunning = false;
                loadingThread.Join();
                loggedin = landingPages.DriverLanding(drivers, trips, vehicles, loggedInName);
            }
            else if (userRole == 4)
            {
                Thread.Sleep(2500);
                keepRunning = false;
                landingPages.FinanceLanding(finance, trips, loggedInName);
            }
        }
    }

    static void ShowLoadingScreen()
    {
        string message = " Welcome to Fleet Manager ";
        string border = new string('*', message.Length + 4);

        while (keepRunning)
        {
            Console.Clear();
            Console.WriteLine("╔═════════════════════════════════════════╗");
            Console.WriteLine("║         Loading... Fleet Manager        ║");
            Console.WriteLine("╚═════════════════════════════════════════╝");
            Thread.Sleep(5000);
        }
        Console.Clear();
    }

    static void LoggedInMsg()
    {
        Console.Clear();
        Console.WriteLine("╔═════════════════════════════════════════╗");
        Console.WriteLine("║           Logged In Successfully        ║");
        Console.WriteLine("╚═════════════════════════════════════════╝");
        Thread.Sleep(1200);
    }

    public static void ErrorMsg()
    {
        Console.Clear();
        Console.WriteLine("\n===========================\nInvalid Email or Password. \n===========================");
        Thread.Sleep(2000);
    }

}
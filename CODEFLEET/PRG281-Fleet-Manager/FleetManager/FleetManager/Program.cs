internal class Program
{
    private static void Main(string[] args)
    {
        DataManager dataManager = new DataManager();
        Admin admin = new Admin();
        List<Vehicle> vehicles = dataManager.LoadVehicles();
        List<Trip> trips = dataManager.LoadTrips();
        List<Driver> drivers = dataManager.LoadDrivers();
        List<User> users = dataManager.LoadUser();
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
                userRole = UserManagement.Login(users);
                loggedin = true;
            }
            else if (userRole == 1 || userRole == 2)
            {
                loggedin= LandingPages.AdminLanding();
            }
            else if (userRole == 3)
            {
                loggedin = LandingPages.DriverLanding();
            }
            else if (userRole == 4)
            {
                LandingPages.FinanceLanding();
            }
        }

    }
}
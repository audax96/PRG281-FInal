public class LandingPages
{
        static DataManager dataManager = new DataManager();
        Admin admin = new Admin();
        static List<Vehicle> vehicles = dataManager.LoadVehicles();
        static List<Trip> trips = dataManager.LoadTrips();
        static List<Driver> drivers = dataManager.LoadDrivers();
        static List<User> users = dataManager.LoadUser();


     public static bool AdminLanding()
    {
        Console.Clear();
        Console.WriteLine("╔═════════════════════════════════════════╗");
        Console.WriteLine("║             Admin Home Page             ║");
        Console.WriteLine("╚═════════════════════════════════════════╝");
        Console.WriteLine("1. Admin");
        Console.WriteLine("2. Log Trip");
        Console.WriteLine("3. Reports");
        Console.WriteLine("0. Exit");
        Console.Write("Choose an option: ");
        string option = Console.ReadLine();
        switch (option)
        {
            case "1":
                AdminDisplay();
                return true;
                break;
                case "2":
                Trip.LogTrip(vehicles, trips, dataManager);
                return true;
                break;
                case "3":
                DisplayReports();
                return true;
                break;
            case "0":
                return false;
                break;
            default:
                Console.WriteLine("Invalid option. Please try again.");
                return true;
                break;
        }
    }
    public static void AdminDisplay()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("╔═════════════════════════════════════════╗");
                Console.WriteLine("║              Admin Home Page            ║");
                Console.WriteLine("╚═════════════════════════════════════════╝");
                Console.WriteLine("1. Add Vehicle");
                Console.WriteLine("2. Add Driver");
                Console.WriteLine("3. Add User");
                Console.WriteLine("4. Remove Vehicle");
                Console.WriteLine("5. Remove Driver");
                Console.WriteLine("6. Toggle User Status");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                string option = Console.ReadLine();
                switch (option)
                {
                    case "1": Admin.AddVehicle(vehicles, dataManager); break;

                    case "2": Admin.AddDriver(drivers, dataManager); break;

                    case "3": UserManagement.CreateUser(users); break;

                    case "4": vehicles = Admin.RemoveVehicle(vehicles); break;

                    case "5": drivers = Admin.RemoveDriver(drivers); break;

                    case "6": users = UserManagement.DeactivateUser(users); break;

                    case "0": return ;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    public static bool DriverLanding()
    {
        Console.Clear();
        Console.WriteLine("╔═════════════════════════════════════════╗");
        Console.WriteLine("║            Driver Home Page             ║");
        Console.WriteLine("╚═════════════════════════════════════════╝");
        Console.WriteLine("1. Reports");
        Console.WriteLine("0. Exit");
        Console.Write("Choose an option: ");
        string option = Console.ReadLine();
        switch (option)
        {
            case "1":
                DisplayReports();
                return true;
                break;
            case "0":
                return false;
                break;
            default:
                Console.WriteLine("Invalid option. Please try again.");
                return true;
                break;
        }
    }

    public static void DisplayReports()
    {
        Reports reports = new();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("REPORTS:");
            Console.WriteLine("1. Trips");
            Console.WriteLine("2. Drivers");
            Console.WriteLine("3. Vehicles");
            Console.WriteLine("0. Exit");
            Console.Write("Choose an option: ");

            string option = Console.ReadLine();
            switch (option)
            {
                case "1": reports.ReportsTrips(drivers, trips); break;

                case "2": Reports.ReportsDrivers(drivers, trips); break;

                case "3": Reports.ReportsVehicle(drivers, trips, vehicles); break;

                case "0": return;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }

    }

    public static void FinanceLanding()
    {
        Console.Clear();
        Console.WriteLine("╔═════════════════════════════════════════╗");
        Console.WriteLine("║           Finance Home Page             ║");
        Console.WriteLine("╚═════════════════════════════════════════╝");
        Console.ReadKey();
    }
}
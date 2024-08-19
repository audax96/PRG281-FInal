public class LandingPages
{
    static DataManager dataManager = new DataManager();
    Admin admin = new Admin();


    public static bool AdminLanding(List<Trip> trips, List<Vehicle> vehicles, List<Driver> drivers, List<User> users, List<Finance> finance, string loggedInName)
    {
        Console.Clear();
        Console.WriteLine("╔═════════════════════════════════════════╗");
        Console.WriteLine("║             Admin Home Page             ║");
        Console.WriteLine("╚═════════════════════════════════════════╝");
        Console.WriteLine("===========================================");
        Console.WriteLine($"{loggedInName,20}, Welcome");
        Console.WriteLine("===========================================");
        Console.WriteLine("1. Admin");
        Console.WriteLine("2. Log Trip");
        Console.WriteLine("3. Reports");
        Console.WriteLine("4. Finance");
        Console.WriteLine("0. Exit");
        Console.Write("Choose an option: ");
        string option = Console.ReadLine();
        switch (option)
        {
            case "1":
                AdminDisplay(users, vehicles, drivers, trips);
                return true;
                break;
            case "2":
                Trip.LogTrip(vehicles, trips, dataManager);
                return true;
                break;
            case "3":
                DisplayReports(drivers, trips, vehicles);
                return true;
                break;
            case "4":
                FinanceLanding(finance, trips, loggedInName);
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
    public static void AdminDisplay(List<User> users, List<Vehicle> vehicles, List<Driver> drivers, List<Trip> trips)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("╔═════════════════════════════════════════╗");
            Console.WriteLine("║               Admin Funtions            ║");
            Console.WriteLine("╚═════════════════════════════════════════╝");
            Console.WriteLine("1. Add Vehicle");
            Console.WriteLine("2. Add Driver");
            Console.WriteLine("3. Add User");
            Console.WriteLine("4. Deactivate Vehicle");
            Console.WriteLine("5. Deactivate Driver");
            Console.WriteLine("6. Toggle User Status");
            Console.WriteLine("7. Delete Trip");
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

                case "7": trips = Admin.RemoveTrip(trips); break;

                case "0": return;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
    public static bool DriverLanding(List<Driver> drivers, List<Trip> trips, List<Vehicle> vehicles, string loggedInName)
    {
        Console.Clear();
        Console.WriteLine("╔═════════════════════════════════════════╗");
        Console.WriteLine("║            Driver Home Page             ║");
        Console.WriteLine("╚═════════════════════════════════════════╝");
        Console.WriteLine("===========================================");
        Console.WriteLine($"{loggedInName,20}, Welcome");
        Console.WriteLine("===========================================");
        Console.WriteLine("1. Reports");
        Console.WriteLine("0. Exit");
        Console.Write("Choose an option: ");
        string option = Console.ReadLine();
        switch (option)
        {
            case "1":
                DisplayReports(drivers, trips, vehicles);
                return true;
            case "0":
                return false;
            default:
                Console.WriteLine("Invalid option. Please try again.");
                return true;
        }
    }

    public static void DisplayReports(List<Driver> drivers, List<Trip> trips, List<Vehicle> vehicles)
    {

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
                case "1": Reports.ReportsTrips(drivers, trips); break;

                case "2": Reports.ReportsDrivers(drivers, trips); break;

                case "3": Reports.ReportsVehicle(drivers, trips, vehicles); break;

                case "0": return;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }

    }

    public static void FinanceLanding(List<Finance> finances, List<Trip> trips, string loggedInName)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("===========================================");
            Console.WriteLine($"{loggedInName,20}, Welcome");
            Console.WriteLine("===========================================");
            Console.WriteLine("╔═════════════════════════════════════════╗");
            Console.WriteLine("║               Handled Trips             ║");
            Console.WriteLine("╚═════════════════════════════════════════╝");
            if (finances == null)
            {
                Console.WriteLine("Finances list is null. Please check the initialization. Press Enter to continue.");
                Console.ReadKey();
                return;
            }
            Admin.AddFinance(finances, trips, dataManager);
            foreach (var record in finances)
            {
                if (record.Handled)
                {
                    string date = record.DateOfTrip.ToString("dd/MM/yyyy");
                    Console.WriteLine($"|| Trip Number: {record.TripNumber} || Cost: R{record.CostOfTrip} || Distance: {record.KMsLogged}km || Date: {date} ||");
                }
            }
            Console.WriteLine("╔═════════════════════════════════════════╗");
            Console.WriteLine("║              Unhandled Trips            ║");
            Console.WriteLine("╚═════════════════════════════════════════╝");
            foreach (var record in finances)
            {
                if (!record.Handled)
                {
                    string date = record.DateOfTrip.ToString("dd/MM/yyyy");
                    Console.WriteLine($"|| Trip Number: {record.TripNumber} || Cost: R{record.CostOfTrip} || Distance: {record.KMsLogged}km || Date: {date} ||");
                }
            }

            Console.Write("Enter Trip Number Once handled (Enter '0' to Exit): ");
            if (int.TryParse(Console.ReadLine(), out int handled))
            {
                var record = finances.FirstOrDefault(r => r.TripNumber == handled);
                if (record != null)
                {
                    record.Handled = true;
                    dataManager.SaveFinance(finances);
                    Console.WriteLine($"Trip {record.TripNumber} has been handled. Press Enter to continue:");
                    Console.ReadKey();
                }
                else if (handled == 0)
                {
                    Console.WriteLine("exit");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    Console.WriteLine("Trip number not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid trip number.");
            }
        }
    }
}
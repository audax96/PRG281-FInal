using System.Globalization;

public class Reports
{


    public void ReportsTrips(List<Driver> drivers, List<Trip> trips)
    {

        while (true)
        {
            Console.Clear();
            Console.WriteLine("REPORTS:");
            Console.WriteLine("1. Daily");
            Console.WriteLine("2. Weekly");
            Console.WriteLine("3. Monthly");
            Console.WriteLine("4. Overall");
            Console.WriteLine("0. Exit");
            Console.Write("Choose an option: ");

            string option = Console.ReadLine();
            switch (option)
            {
                case "1": GenerasteDailyReportTrips(drivers, trips); break;

                case "2": GenerateWeeklyReportTrips(drivers, trips); break;

                case "3": GenerateMonthlyReportTrips(drivers, trips); break;

                case "4": break;

                case "0": return;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }

    }

    public static void ReportsDrivers(List<Driver> drivers, List<Trip> trips)
    {

        while (true)
        {
            Console.Clear();
            Console.WriteLine("REPORTS:");
            Console.WriteLine("1. Daily");
            Console.WriteLine("2. Weekly");
            Console.WriteLine("3. Monthly");
            Console.WriteLine("4. Overall");
            Console.WriteLine("0. Exit");
            Console.Write("Choose an option: ");

            string option = Console.ReadLine();
            switch (option)
            {
                case "1": GenerateDailyReportDrivers(drivers, trips); break;

                case "2": GenerateWeeklyReportDrivers(drivers, trips); break;

                case "3": GenerateMonthlyReportDrivers(drivers, trips); break;

                case "4": break;

                case "0": return;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }

    }


    public static void ReportsVehicle(List<Driver> drivers, List<Trip> trips, List<Vehicle> vehicles)
    {

        while (true)
        {
            Console.Clear();
            Console.WriteLine("REPORTS:");
            Console.WriteLine("1. Daily");
            Console.WriteLine("2. Weekly");
            Console.WriteLine("3. Monthly");
            Console.WriteLine("4. Overall");
            Console.WriteLine("0. Exit");
            Console.Write("Choose an option: ");

            string option = Console.ReadLine();
            switch (option)
            {
                case "1": GenerateDailyReportVehicles(vehicles, drivers, trips); break;

                case "2": GenerateWeeklyReportVehicles(vehicles, drivers, trips); break;

                case "3": GenerateMonthlyReportVehicles(vehicles, drivers, trips); break;

                case "4": break;

                case "0": return;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }

    }
    public static void GenerasteDailyReportTrips(List<Driver> drivers, List<Trip> trips)
    {
        Console.WriteLine("Trips For Today:");
        foreach (var trip in trips)
        {
            if (trip.Date == DateTime.Today)
            {
                string driverFullname = "";
                foreach (var driver in drivers)
                {

                    if (driver.DriverNumber == trip.DriverNumber)
                    {
                        driverFullname = driver.DisplayFullname();
                    }
                }
                Console.WriteLine($"Vehicle ID:{trip.VehicleId}, Driver Name: {driverFullname}, ");
                Console.Write($"Trip Distance:{trip.CalculateDistance()}, Fuel Efficiency: {trip.FuelEfficiency}, Trip Date: {trip.Date} ");
            }
        }
        Console.WriteLine("Press Any Key To Continue:");
        Console.ReadKey();

    }

    public static void GenerateWeeklyReportTrips(List<Driver> drivers, List<Trip> trips)
    {
        Console.WriteLine("Trips For The Last Week:");
        foreach (var trip in trips)
        {
            if (trip.Date > DateTime.Today.AddDays(-7))
            {
                string driverFullname = "";
                foreach (var driver in drivers)
                {

                    if (driver.DriverNumber == trip.DriverNumber)
                    {
                        driverFullname = driver.DisplayFullname();
                    }
                }

                Console.WriteLine($"Vehicle ID:{trip.VehicleId}, Driver Name: {driverFullname}, Trip Distance:{trip.CalculateDistance()}, Fuel Efficiency: {trip.FuelEfficiency}, Trip Date: {trip.Date.ToString("dd/MM/yyyy")}");
            }
        }
        Console.WriteLine("Press Any Key To Continue:");
        Console.ReadKey();
    }

    async public void GenerateMonthlyReportTrips(List<Driver> drivers, List<Trip> trips)

    {

        Console.WriteLine("Enter The Month Number You Want A Report On (January = 1):");
        int chosenMonth = Int32.Parse(Console.ReadLine());
        foreach (var trip in trips)
        {
            if (trip.Date.Month == chosenMonth)
            {
                string driverFullname = "";
                foreach (var driver in drivers)
                {

                    if (driver.DriverNumber == trip.DriverNumber)
                    {
                        driverFullname = driver.DisplayFullname();
                    }
                }

                Console.WriteLine($"Vehicle ID:{trip.VehicleId}, Driver Name: {driverFullname}, Trip Cost:{trip.FuelUsed}, Fuel Efficiency: {trip.FuelEfficiency}, Trip Date: {trip.Date.ToString("dd/MM/yyyy")} ");
            }

        }
        Console.WriteLine("Press Enter To Continue:");
        Console.ReadLine();
    }



    private static void GenerateDailyReportDrivers(List<Driver> drivers, List<Trip> trips)
    {
        List<Driver> selectedDrivers = SelectDrivers(drivers);
        DateTime today = DateTime.Today;
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════════╗");
        Console.WriteLine($"║  DAILY REPORT FOR DRIVERS ({today:dd/MM/yyyy}):  ║");
        Console.WriteLine("╚══════════════════════════════════════════╝");

        bool isFirstDriver = true;

        foreach (var driver in selectedDrivers)
        {
            if (!isFirstDriver)
            {
                Console.WriteLine("==========================================================");
            }
            isFirstDriver = false;

            var driverTrips = trips.Where(t => t.DriverNumber == driver.DriverNumber && t.Date.Date == today).ToList();

            if (driverTrips.Count != 0)
            {
                double totalDistance = 0;
                double totalFuelEfficiency = 0;
                int tripCount = 0;

                Console.WriteLine($"{driver.DisplayFullname().ToUpper()}:\n");


                Console.WriteLine("  Trip ID  |  Vehicle ID  |  Distance  |  Fuel Efficiency ");
                foreach (var trip in driverTrips)
                {
                    Console.WriteLine($"  {trip.TripId,6}   |  {trip.VehicleId,5}       |  {trip.CalculateDistance(),5} km  |    {trip.FuelEfficiency:00.00} L/km");
                    totalDistance += trip.Distance;
                    totalFuelEfficiency += trip.FuelEfficiency;
                    tripCount++;
                }
                Console.WriteLine("╔══════════════════════════════════════════╗");
                Console.WriteLine("║ SUMMARY:                                 ║");
                Console.WriteLine($"║    Total Distance: {totalDistance:F2} km             ║");
                Console.WriteLine($"║    Average Fuel Efficiency: {totalFuelEfficiency / tripCount:00.00} L/km   ║");
                Console.WriteLine("╚══════════════════════════════════════════╝");
            }
            else
            {
                Console.WriteLine($"{driver.DisplayFullname()}:");
                Console.WriteLine("  No Trips in selected timeframe\n");
            }
        }

        Console.WriteLine("Press Any Key To Continue:");
        Console.ReadKey();
    }



    private static void GenerateWeeklyReportDrivers(List<Driver> drivers, List<Trip> trips)
    {
        List<Driver> selectedDrivers = SelectDrivers(drivers);
        DateTime oneWeekAgo = DateTime.Today.AddDays(-7);

        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════════════════════════╗");
        Console.WriteLine($"║  WEEKLY REPORT FOR DRIVERS ({oneWeekAgo:dd/MM/yyyy} - {DateTime.Today:dd/MM/yyyy}):  ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════╝");

        bool isFirstDriver = true;

        foreach (var driver in selectedDrivers)
        {
            if (!isFirstDriver)
            {
                Console.WriteLine("==========================================================");
            }
            isFirstDriver = false;

            var driverTrips = trips.Where(t => t.DriverNumber == driver.DriverNumber && t.Date >= oneWeekAgo && t.Date <= DateTime.Today).ToList();

            if (driverTrips.Count != 0)
            {
                double totalDistance = 0;
                double totalFuelEfficiency = 0;
                int tripCount = 0;

                Console.WriteLine($"{driver.DisplayFullname().ToUpper()}:\n");

                Console.WriteLine("  Trip ID  |  Vehicle ID  |  Distance  |  Fuel Efficiency ");
                foreach (var trip in driverTrips)
                {
                    Console.WriteLine($"  {trip.TripId,6}   |  {trip.VehicleId,5}       |  {trip.CalculateDistance(),5} km  |    {trip.FuelEfficiency:00.00} L/km");
                    totalDistance += trip.Distance;
                    totalFuelEfficiency += trip.FuelEfficiency;
                    tripCount++;
                }
                Console.WriteLine("     ╔══════════════════════════════════════════╗");
                Console.WriteLine("     ║ SUMMARY:                                 ║");
                Console.WriteLine($"     ║    Total Distance: {totalDistance:0000.00} km            ║");
                Console.WriteLine($"     ║    Average Fuel Efficiency: {totalFuelEfficiency / tripCount:00.00} L/km   ║");
                Console.WriteLine("     ╚══════════════════════════════════════════╝");
            }
            else
            {
                Console.WriteLine($"{driver.DisplayFullname()}:");
                Console.WriteLine("  No Trips in selected timeframe\n");
            }
        }
        Console.WriteLine("Press Any Key To Continue:");
        Console.ReadKey();
    }

    private static void GenerateMonthlyReportDrivers(List<Driver> drivers, List<Trip> trips)
    {
        List<Driver> selectedDrivers = SelectDrivers(drivers);

        Console.WriteLine("Enter The Month Number You Want A Report On (January = 1):");
        int chosenMonth = 0;
        string monthName = "";
        bool isValidMonth = false;

        while (!isValidMonth)
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out chosenMonth) && chosenMonth >= 1 && chosenMonth <= 12)
            {
                isValidMonth = true;

                DateTime date = new(1, chosenMonth, 1);
                monthName = date.ToString("MMMM", CultureInfo.CurrentCulture);
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid month number (1-12):");
            }
        }

        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════════╗");
        Console.WriteLine($"║  MONTHLY REPORT FOR DRIVERS ({monthName.ToUpper(),9}):  ║");
        Console.WriteLine("╚══════════════════════════════════════════╝");

        bool isFirstDriver = true;

        foreach (var driver in selectedDrivers)
        {
            if (!isFirstDriver)
            {
                Console.WriteLine("==========================================================");
            }
            isFirstDriver = false;

            var driverTrips = trips.Where(t => t.DriverNumber == driver.DriverNumber && t.Date.Month == chosenMonth && t.Date.Year == DateTime.Today.Year).ToList();

            if (driverTrips.Count != 0)
            {
                double totalDistance = 0;
                double totalFuelEfficiency = 0;
                int tripCount = 0;

                Console.WriteLine($"{driver.DisplayFullname().ToUpper()}:\n");

                Console.WriteLine("  Trip ID  |  Vehicle ID  |  Distance  |  Fuel Efficiency ");
                foreach (var trip in driverTrips)
                {
                    Console.WriteLine($"  {trip.TripId,6}   |  {trip.VehicleId,5}       |  {trip.CalculateDistance(),5} km  |    {trip.FuelEfficiency:00.00} L/km");
                    totalDistance += trip.Distance;
                    totalFuelEfficiency += trip.FuelEfficiency;
                    tripCount++;
                }
                Console.WriteLine("     ╔══════════════════════════════════════════╗");
                Console.WriteLine("     ║ SUMMARY:                                 ║");
                Console.WriteLine($"     ║    Total Distance: {totalDistance:0000.00} km            ║");
                Console.WriteLine($"     ║    Average Fuel Efficiency: {totalFuelEfficiency / tripCount:00.00} L/km   ║");
                Console.WriteLine("     ╚══════════════════════════════════════════╝");
            }
            else
            {
                Console.WriteLine($"{driver.DisplayFullname()}:");
                Console.WriteLine("  No Trips in selected timeframe\n");
            }
        }
        Console.WriteLine("Press Any Key To Continue:");
        Console.ReadKey();
    }

    private static List<Driver> SelectDrivers(List<Driver> drivers)
    {

        List<Driver> selectedDrivers = [];

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Select a Driver :");
            Console.WriteLine("(Select 0 to generate report for selected Drivers)");
            Console.WriteLine();
            Console.WriteLine("0. Generate Report");
            Console.WriteLine("1. All Drivers");
            Console.WriteLine();
            Console.WriteLine("Drivers:");
            for (int i = 0; i < drivers.Count; i++)
            {
                Console.WriteLine($"Driver Num: {drivers[i].DriverNumber}. {drivers[i].DisplayFullname()}");
            }
            Console.Write("\nChoose an Drive by Driver Num or 1 for all Drivers: ");
            string input = Console.ReadLine();
            Console.WriteLine("");

            if (int.TryParse(input, out int choice))
            {
                switch (choice)
                {
                    case 0:
                        if (selectedDrivers.Count != 0)
                        {
                            return selectedDrivers;
                        }
                        else
                        {
                            Console.WriteLine("No drivers has been selected!");
                            Console.ReadKey();
                        }
                        break;

                    case 1:
                        return drivers;

                    default:
                        {
                            bool found = false;
                            foreach (var driver in drivers)
                            {
                                if (driver.DriverNumber == choice)
                                {
                                    selectedDrivers.Add(driver);
                                    found = true;
                                    break;
                                }
                            }
                            if (!found)
                            {
                                Console.WriteLine("Please provide a valid Driver Number!");
                                Console.ReadKey();
                            }
                            break;
                        }

                }

            }
            else
            {
                Console.WriteLine("Please select a valid option");
                Console.ReadKey();
            }

        }


    }

    private static void DisplayTripDetails(Trip trip, List<Driver> drivers)
    {
        string driverName = drivers.FirstOrDefault(d => d.DriverNumber == trip.DriverNumber)?.DisplayFullname() ?? "Unknown Driver";
        Console.WriteLine($"Trip ID: {trip.TripId}, Vehicle ID: {trip.VehicleId}, Driver: {driverName}, Distance: {trip.CalculateDistance()} km, Fuel Efficiency: {trip.FuelEfficiency} L/km, Date: {trip.Date:dd/MM/yyyy}");
    }

    private static void GenerateDailyReportVehicles(List<Vehicle> vehicles, List<Driver> drivers, List<Trip> trips)
    {
        List<Vehicle> selected = SelectVehicles(vehicles);
        DateTime today = DateTime.Today;
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════════╗");
        Console.WriteLine($"║  DAILY REPORT FOR VEHICLES ({today:dd/MM/yyyy}):  ║");
        Console.WriteLine("╚══════════════════════════════════════════╝");

        bool isFirstDriver = true;

        foreach (var vehicle in selected)
        {
            if (!isFirstDriver)
            {
                Console.WriteLine("==========================================================");
            }
            isFirstDriver = false;

            var vehicleTrips = trips.Where(t => t.VehicleId == vehicle.VehicleId && t.Date.Date == today).ToList();

            if (vehicleTrips.Count != 0)
            {
                double totalDistance = 0;
                double totalFuelEfficiency = 0;
                int tripCount = 0;

                Console.WriteLine($"{vehicle.VehicleLicence.ToUpper()}:\n");


                Console.WriteLine("  Trip ID  |  Driver No  |  Distance  |  Fuel Efficiency ");
                foreach (var trip in vehicleTrips)
                {
                    Console.WriteLine($"  {trip.TripId,6}   |  {trip.DriverNumber,5}       |  {trip.CalculateDistance(),5} km  |    {trip.FuelEfficiency:00.00} L/km");
                    totalDistance += trip.Distance;
                    totalFuelEfficiency += trip.FuelEfficiency;
                    tripCount++;
                }
                Console.WriteLine("╔══════════════════════════════════════════╗");
                Console.WriteLine("║ SUMMARY:                                 ║");
                Console.WriteLine($"║    Total Distance: {totalDistance:F2} km             ║");
                Console.WriteLine($"║    Average Fuel Efficiency: {totalFuelEfficiency / tripCount:00.00} L/km   ║");
                Console.WriteLine("╚══════════════════════════════════════════╝");
            }
            else
            {
                Console.WriteLine($"{vehicle.VehicleLicence.ToUpper()}:\n");
                Console.WriteLine("  No Trips in selected timeframe\n");
            }
        }

        Console.WriteLine("Press Any Key To Continue:");
        Console.ReadKey();
    }

    private static void GenerateWeeklyReportVehicles(List<Vehicle> vehicles, List<Driver> drivers, List<Trip> trips)
    {
        List<Vehicle> selectedVehicles = SelectVehicles(vehicles);
        DateTime oneWeekAgo = DateTime.Today.AddDays(-7);

        Console.WriteLine("Weekly Report for Vehicles:");
        foreach (var vehicle in selectedVehicles)
        {
            var vehicleTrips = trips.Where(t => t.VehicleId == vehicle.VehicleId && t.Date >= oneWeekAgo && t.Date <= DateTime.Today).ToList();
            if (vehicleTrips.Any())
            {
                Console.WriteLine($"Vehicle: {vehicle.Make} {vehicle.Model}");
                foreach (var trip in vehicleTrips)
                {
                    DisplayTripDetails(trip, drivers);
                }
            }
        }
        Console.WriteLine("Press Any Key To Continue:");
        Console.ReadKey();
    }

    private static void GenerateMonthlyReportVehicles(List<Vehicle> vehicles, List<Driver> drivers, List<Trip> trips)
    {
        List<Vehicle> selectedVehicles = SelectVehicles(vehicles);

        Console.WriteLine("Enter The Month Number You Want A Report On (January = 1):");
        int chosenMonth = Int32.Parse(Console.ReadLine());

        Console.WriteLine("Monthly Report for Vehicles:");
        foreach (var vehicle in selectedVehicles)
        {
            var vehicleTrips = trips.Where(t => t.VehicleId == vehicle.VehicleId && t.Date.Month == chosenMonth && t.Date.Year == DateTime.Today.Year).ToList();
            if (vehicleTrips.Any())
            {
                Console.WriteLine($"Vehicle: {vehicle.Make} {vehicle.Model}");
                foreach (var trip in vehicleTrips)
                {
                    DisplayTripDetails(trip, drivers);
                }
            }
        }
        Console.WriteLine("Press Any Key To Continue:");
        Console.ReadKey();
    }

    private static List<Vehicle> SelectVehicles(List<Vehicle> vehicles)
    {

        List<Vehicle> selectedVehicles = [];

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Select a Vehicles :");
            Console.WriteLine("(Select 0 to generate report for selected Vehicles)");
            Console.WriteLine();
            Console.WriteLine("0. Generate Report");
            Console.WriteLine("1. All Vehicles");
            Console.WriteLine();
            Console.WriteLine("Vehicles:");
            for (int i = 0; i < vehicles.Count; i++)
            {
                Console.WriteLine($"Vehicle ID: {vehicles[i].VehicleId}. {vehicles[i].Make} {vehicles[i].Model}");
            }
            Console.Write("\nChoose a Vehicles by Vehicles ID or 1 for all Vehicles:");
            string input = Console.ReadLine();
            Console.WriteLine("");

            if (int.TryParse(input, out int choice))
            {
                switch (choice)
                {
                    case 0:
                        if (selectedVehicles.Count != 0)
                        {
                            return selectedVehicles;
                        }
                        else
                        {
                            Console.WriteLine("No Vehicels has been selected!");
                            Console.ReadKey();
                        }
                        break;

                    case 1:
                        return vehicles;

                    default:
                        {
                            bool found = false;
                            foreach (var vehicle in vehicles)
                            {
                                if (vehicle.VehicleId == choice)
                                {
                                    selectedVehicles.Add(vehicle);
                                    found = true;
                                    break;
                                }
                            }
                            if (!found)
                            {
                                Console.WriteLine("Please provide a valid Vehicle ID!");
                                Console.ReadKey();
                            }
                            break;
                        }

                }

            }
            else
            {
                Console.WriteLine("Please select a valid option");
                Console.ReadKey();
            }

        }


    }


}







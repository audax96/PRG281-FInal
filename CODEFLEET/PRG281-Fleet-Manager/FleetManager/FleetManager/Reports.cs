using System.Globalization;

public class Reports
{


    public static void ReportsTrips(List<Driver> drivers, List<Trip> trips)
    {

        while (true)
        {
            Console.Clear();
            Console.WriteLine("╔═════════════════════════════════════════╗");
            Console.WriteLine("║                  Reports                ║");
            Console.WriteLine("╚═════════════════════════════════════════╝");
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

                case "4": GenerateOverallReportTrips(drivers, trips); break;

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
            Console.WriteLine("╔═════════════════════════════════════════╗");
            Console.WriteLine("║              Driver   Reports           ║");
            Console.WriteLine("╚═════════════════════════════════════════╝");
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

                case "4": GenerateOverallReportDrivers(drivers, trips); break;

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
            Console.WriteLine("╔═════════════════════════════════════════╗");
            Console.WriteLine("║                 Reports                 ║");
            Console.WriteLine("╚═════════════════════════════════════════╝");
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

                case "4": GenerateOverallReportVehicles(vehicles, drivers, trips); break;

                case "0": return;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }

    }
    private static void GenerateOverallReportTrips(List<Driver> drivers, List<Trip> trips)
    {
        Console.Clear();
        Console.WriteLine("╔═════════════════════════════╗");
        Console.WriteLine($"║  OVERALL REPORT FOR TRIPS   ║");
        Console.WriteLine("╚═════════════════════════════╝");

        Console.WriteLine("╔══════════╦══════════════╦═══════════════════════════╦═══════════╦══════════════════╗");
        Console.WriteLine("║ Trip ID  ║  Vehicle ID  ║           Driver          ║ Distance  ║  Fuel Efficiency ║");
        Console.WriteLine("╠══════════╬══════════════╬═══════════════════════════╬═══════════╬══════════════════╣");
        foreach (var trip in trips)
        {
            string DriverFullName = "";
            foreach (var driver in drivers)
            {
                if (driver.DriverNumber == trip.DriverNumber)
                {
                    DriverFullName = driver.DisplayFullname();
                }
            }

            Console.WriteLine($"║ {trip.TripId,6}   ║  {trip.VehicleId,5}       ║   {DriverFullName,20}    ║ {trip.Distance,5} km  ║    {trip.FuelEfficiency:00.00} L/km    ║");

        }
        Console.WriteLine("╚══════════╩══════════════╩═══════════════════════════╩═══════════╩══════════════════╝");
        Console.WriteLine("Press Any Key To Continue:");
        Console.ReadKey();

    }
    private static void GenerateOverallReportDrivers(List<Driver> drivers, List<Trip> trips)
    {
        List<Driver> selectedDrivers = SelectDrivers(drivers);


        Console.Clear();
        Console.WriteLine("╔══════════════════════════════╗");
        Console.WriteLine($"║  OVERALL REPORT FOR DRIVERS  ║");
        Console.WriteLine("╚══════════════════════════════╝");

        bool isFirstDriver = true;

        foreach (var driver in selectedDrivers)
        {
            if (!isFirstDriver)
            {
                Console.WriteLine("==========================================================");
            }
            isFirstDriver = false;

            var driverTrips = trips.Where(t => t.DriverNumber == driver.DriverNumber).ToList();

            if (driverTrips.Count != 0)
            {
                double totalDistance = 0;
                double totalFuelEfficiency = 0;
                int tripCount = 0;

                Console.WriteLine($"{driver.DisplayFullname().ToUpper()}:\n");
                Console.WriteLine("╔══════════╦══════════════╦════════════╦══════════════════╗");
                Console.WriteLine("║ Trip ID  ║  Vehicle ID  ║  Distance  ║  Fuel Efficiency ║");
                Console.WriteLine("╠══════════╬══════════════╬════════════╬══════════════════╣");
                foreach (var trip in driverTrips)
                {
                    Console.WriteLine($"║ {trip.TripId,6}   ║  {trip.VehicleId,5}       ║  {trip.Distance,5} km  ║    {trip.FuelEfficiency:00.00} L/km    ║");
                    totalDistance += trip.Distance;
                    totalFuelEfficiency += trip.FuelEfficiency;
                    tripCount++;
                }
                Console.WriteLine("╚══════════╩══════════════╩════════════╩══════════════════╝");
                Console.WriteLine("        ╔══════════════════════════════════════════╗");
                Console.WriteLine("        ║ SUMMARY:                                 ║");
                Console.WriteLine($"        ║    Total Distance: {totalDistance:0000.00} km            ║");
                Console.WriteLine($"        ║    Average Fuel Efficiency: {totalFuelEfficiency / tripCount:00.00} L/km   ║");
                Console.WriteLine("        ╚══════════════════════════════════════════╝");
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
    private static void GenerateOverallReportVehicles(List<Vehicle> vehicles, List<Driver> drivers, List<Trip> trips)
    {
        List<Vehicle> selected = SelectVehicles(vehicles);
        Console.Clear();
        Console.Clear();
        Console.WriteLine("╔═══════════════════════════════╗");
        Console.WriteLine($"║  OVERALL REPORT FOR VEHICLES  ║");
        Console.WriteLine("╚═══════════════════════════════╝");



        foreach (var vehicle in selected)
        {


            var vehicleTrips = trips.Where(t => t.VehicleId == vehicle.VehicleId).ToList();

            Console.WriteLine("======================================================================================");
            if (vehicleTrips.Count != 0)
            {
                double totalDistance = 0;
                double totalFuelEfficiency = 0;
                int tripCount = 0;

                Console.WriteLine($"{vehicle.VehicleLicence.ToUpper()}:\n");

                Console.WriteLine("╔══════════╦══════════════╦═══════════════════════════╦═══════════╦══════════════════╗");
                Console.WriteLine("║ Trip ID  ║  Vehicle ID  ║           Driver          ║ Distance  ║  Fuel Efficiency ║");
                Console.WriteLine("╠══════════╬══════════════╬═══════════════════════════╬═══════════╬══════════════════╣");
                foreach (var trip in trips)
                {
                    if (trip.VehicleId == vehicle.VehicleId)
                    {
                        string DriverFullName = "";
                        foreach (var driver in drivers)
                        {
                            if (driver.DriverNumber == trip.DriverNumber)
                            {
                                DriverFullName = driver.DisplayFullname();
                            }
                        }


                        Console.WriteLine($"║ {trip.TripId,6}   ║  {trip.VehicleId,5}       ║   {DriverFullName,20}    ║ {trip.Distance,5} km  ║    {trip.FuelEfficiency:00.00} L/km    ║");
                        totalDistance += trip.Distance;
                        totalFuelEfficiency += trip.FuelEfficiency;
                        tripCount++;
                    }
                }
                Console.WriteLine("╚══════════╩══════════════╩═══════════════════════════╩═══════════╩══════════════════╝");
                Console.WriteLine("                     ╔══════════════════════════════════════════╗");
                Console.WriteLine("                     ║ SUMMARY:                                 ║");
                Console.WriteLine($"                     ║    Total Distance: {totalDistance:0000.00} km            ║");
                Console.WriteLine($"                     ║    Average Fuel Efficiency: {totalFuelEfficiency / tripCount:00.00} L/km   ║");
                Console.WriteLine("                     ╚══════════════════════════════════════════╝");
            }
            else
            {
                Console.WriteLine($"{vehicle.VehicleLicence.ToUpper()}:");
                Console.WriteLine("  No Trips in selected timeframe\n");
            }
        }

        Console.WriteLine("Press Any Key To Continue:");
        Console.ReadKey();
    }
    private static void GenerasteDailyReportTrips(List<Driver> drivers, List<Trip> trips)
    {
        DateTime today = DateTime.Today;
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine($"║  DAILY REPORT FOR TRIPS ({today:dd/MM/yyyy}):  ║");
        Console.WriteLine("╚════════════════════════════════════════╝");

        Console.WriteLine("╔══════════╦══════════════╦═══════════════════════════╦═══════════╦══════════════════╗");
        Console.WriteLine("║ Trip ID  ║  Vehicle ID  ║           Driver          ║ Distance  ║  Fuel Efficiency ║");
        Console.WriteLine("╠══════════╬══════════════╬═══════════════════════════╬═══════════╬══════════════════╣");
        foreach (var trip in trips)
        {
            if (trip.Date == DateTime.Today)
            {
                string DriverFullName = "";
                foreach (var driver in drivers)
                {
                    if (driver.DriverNumber == trip.DriverNumber)
                    {
                        DriverFullName = driver.DisplayFullname();
                    }
                }


                Console.WriteLine($"║ {trip.TripId,6}   ║  {trip.VehicleId,5}       ║   {DriverFullName,20}    ║ {trip.Distance,5} km  ║    {trip.FuelEfficiency:00.00} L/km    ║");

            }
        }
        Console.WriteLine("╚══════════╩══════════════╩═══════════════════════════╩═══════════╩══════════════════╝");
        Console.WriteLine("Press Any Key To Continue:");
        Console.ReadKey();

    }


    private static void GenerateWeeklyReportTrips(List<Driver> drivers, List<Trip> trips)
    {
        DateTime oneWeekAgo = DateTime.Today.AddDays(-7);
        Console.Clear();
        Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
        Console.WriteLine($"║  WEEKLY REPORT FOR Trips ({oneWeekAgo:dd/MM/yyyy} - {DateTime.Today:dd/MM/yyyy}):   ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════════╝");

        Console.WriteLine("╔══════════╦══════════════╦═══════════════════════════╦═══════════╦══════════════════╗");
        Console.WriteLine("║ Trip ID  ║  Vehicle ID  ║           Driver          ║ Distance  ║  Fuel Efficiency ║");
        Console.WriteLine("╠══════════╬══════════════╬═══════════════════════════╬═══════════╬══════════════════╣");
        foreach (var trip in trips)
        {

            if (trip.Date > DateTime.Today.AddDays(-7))
            {
                string DriverFullName = "";
                foreach (var driver in drivers)
                {
                    if (driver.DriverNumber == trip.DriverNumber)
                    {
                        DriverFullName = driver.DisplayFullname();
                    }
                }


                Console.WriteLine($"║ {trip.TripId,6}   ║  {trip.VehicleId,5}       ║   {DriverFullName,20}    ║ {trip.Distance,5} km  ║    {trip.FuelEfficiency:00.00} L/km    ║");

            }
        }
        Console.WriteLine("╚══════════╩══════════════╩═══════════════════════════╩═══════════╩══════════════════╝");
        Console.WriteLine("Press Any Key To Continue:");
        Console.ReadKey();
    }

    async private static void GenerateMonthlyReportTrips(List<Driver> drivers, List<Trip> trips)

    {

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
        Console.WriteLine($"║  MONTHLY REPORT FOR DRIVERS ({monthName.ToUpper(),9}): ║");
        Console.WriteLine("╚══════════════════════════════════════════╝");

        Console.WriteLine("╔══════════╦══════════════╦═══════════════════════════╦═══════════╦══════════════════╗");
        Console.WriteLine("║ Trip ID  ║  Vehicle ID  ║           Driver          ║ Distance  ║  Fuel Efficiency ║");
        Console.WriteLine("╠══════════╬══════════════╬═══════════════════════════╬═══════════╬══════════════════╣");

        foreach (var trip in trips)
        {

            if (trip.Date.Month == chosenMonth)
            {
                string DriverFullName = "";
                foreach (var driver in drivers)
                {
                    if (driver.DriverNumber == trip.DriverNumber)
                    {
                        DriverFullName = driver.DisplayFullname();
                    }
                }


                Console.WriteLine($"║ {trip.TripId,6}   ║  {trip.VehicleId,5}       ║   {DriverFullName,20}    ║ {trip.Distance,5} km  ║    {trip.FuelEfficiency:00.00} L/km    ║");

            }
        }
        Console.WriteLine("╚══════════╩══════════════╩═══════════════════════════╩═══════════╩══════════════════╝");
        Console.WriteLine("Press Any Key To Continue:");
        Console.ReadKey();
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
                Console.WriteLine("╔══════════╦══════════════╦════════════╦══════════════════╗");
                Console.WriteLine("║ Trip ID  ║  Vehicle ID  ║  Distance  ║  Fuel Efficiency ║");
                Console.WriteLine("╠══════════╬══════════════╬════════════╬══════════════════╣");
                foreach (var trip in driverTrips)
                {
                    Console.WriteLine($"║ {trip.TripId,6}   ║  {trip.VehicleId,5}       ║  {trip.Distance,5} km  ║    {trip.FuelEfficiency:00.00} L/km    ║");
                    totalDistance += trip.Distance;
                    totalFuelEfficiency += trip.FuelEfficiency;
                    tripCount++;
                }
                Console.WriteLine("╚══════════╩══════════════╩════════════╩══════════════════╝");
                Console.WriteLine("        ╔══════════════════════════════════════════╗");
                Console.WriteLine("        ║ SUMMARY:                                 ║");
                Console.WriteLine($"        ║    Total Distance: {totalDistance:0000.00} km            ║");
                Console.WriteLine($"        ║    Average Fuel Efficiency: {totalFuelEfficiency / tripCount:00.00} L/km   ║");
                Console.WriteLine("        ╚══════════════════════════════════════════╝");
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
                Console.WriteLine("╔══════════╦══════════════╦════════════╦══════════════════╗");
                Console.WriteLine("║ Trip ID  ║  Vehicle ID  ║  Distance  ║  Fuel Efficiency ║");
                Console.WriteLine("╠══════════╬══════════════╬════════════╬══════════════════╣");
                foreach (var trip in driverTrips)
                {
                    Console.WriteLine($"║ {trip.TripId,6}   ║  {trip.VehicleId,5}       ║  {trip.Distance,5} km  ║    {trip.FuelEfficiency:00.00} L/km    ║");
                    totalDistance += trip.Distance;
                    totalFuelEfficiency += trip.FuelEfficiency;
                    tripCount++;
                }
                Console.WriteLine("╚══════════╩══════════════╩════════════╩══════════════════╝");

                Console.WriteLine("        ╔══════════════════════════════════════════╗");
                Console.WriteLine("        ║ SUMMARY:                                 ║");
                Console.WriteLine($"        ║    Total Distance: {totalDistance:0000.00} km            ║");
                Console.WriteLine($"        ║    Average Fuel Efficiency: {totalFuelEfficiency / tripCount:00.00} L/km   ║");
                Console.WriteLine("        ╚══════════════════════════════════════════╝");
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
        Console.WriteLine($"║  MONTHLY REPORT FOR DRIVERS ({monthName.ToUpper(),9}): ║");
        Console.WriteLine("╚══════════════════════════════════════════╝");

        bool isFirstDriver = true;

        foreach (var driver in selectedDrivers)
        {
            if (driver.Active = true)
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
                    Console.WriteLine("╔══════════╦══════════════╦════════════╦══════════════════╗");
                    Console.WriteLine("║ Trip ID  ║  Vehicle ID  ║  Distance  ║  Fuel Efficiency ║");
                    Console.WriteLine("╠══════════╬══════════════╬════════════╬══════════════════╣");

                    foreach (var trip in driverTrips)
                    {
                        Console.WriteLine($"║ {trip.TripId,6}   ║  {trip.VehicleId,5}       ║  {trip.Distance,5} km  ║    {trip.FuelEfficiency:00.00} L/km    ║");
                        totalDistance += trip.Distance;
                        totalFuelEfficiency += trip.FuelEfficiency;
                        tripCount++;
                    }
                    Console.WriteLine("╚══════════╩══════════════╩════════════╩══════════════════╝");
                    Console.WriteLine("        ╔══════════════════════════════════════════╗");
                    Console.WriteLine("        ║ SUMMARY:                                 ║");
                    Console.WriteLine($"        ║    Total Distance: {totalDistance:0000.00} km            ║");
                    Console.WriteLine($"        ║    Average Fuel Efficiency: {totalFuelEfficiency / tripCount:00.00} L/km   ║");
                    Console.WriteLine("        ╚══════════════════════════════════════════╝");
                }
                else
                {
                    Console.WriteLine($"{driver.DisplayFullname()}:");
                    Console.WriteLine("  No Trips in selected timeframe\n");
                }
            }
            else
            {
                continue;
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
            Console.WriteLine("Drivers:");
            for (int i = 0; i < drivers.Count; i++)
            {
                Console.WriteLine($"Driver Num: {drivers[i].DriverNumber}. {drivers[i].DisplayFullname()}");
            }
            Console.WriteLine("\nType Driver's num (eg. 2) to add to Report List.\n");
            Console.WriteLine("Type A for report on all Drivers.");
            Console.WriteLine("Type G to generate report for selected Drivers.");
            Console.Write("\nOption:");

            string input = Console.ReadLine();
            Console.WriteLine("");


            switch (input.ToUpper())
            {
                case "G":
                    if (selectedDrivers.Count != 0)
                    {
                        return selectedDrivers;
                    }
                    else
                    {
                        Console.WriteLine("No Drivers has been selected!");
                        Console.ReadKey();
                    }
                    break;

                case "A":
                    return drivers;

                default:
                    {
                        bool found = false;
                        if (int.TryParse(input, out int choice))
                        {
                            foreach (var driver in drivers)
                            {
                                if (driver.DriverNumber == choice)
                                {
                                    bool exist = false;
                                    foreach (var selectedDriver in selectedDrivers)
                                    {
                                        if (selectedDriver.DriverNumber == driver.DriverNumber)
                                        {
                                            Console.WriteLine("Driver Aready Added To The List!");
                                            Console.ReadKey();
                                            found = true;
                                            exist = true;
                                            break;
                                        }
                                    }
                                    if (!exist)
                                    {
                                        selectedDrivers.Add(driver);
                                        Console.WriteLine("Selected Driver Added To List!");
                                        Console.ReadKey();
                                        found = true;
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Please choose a valid option");
                            break;
                        }
                        if (!found)
                        {
                            Console.WriteLine("Please provide a valid Driver Num!");
                            Console.ReadKey();
                        }
                        break;
                    }
            }
        }

    }

    private static void GenerateDailyReportVehicles(List<Vehicle> vehicles, List<Driver> drivers, List<Trip> trips)
    {
        List<Vehicle> selected = SelectVehicles(vehicles);
        DateTime today = DateTime.Today;
        Console.Clear();
        Console.WriteLine("╔═══════════════════════════════════════════╗");
        Console.WriteLine($"║  DAILY REPORT FOR VEHICLES ({today:dd/MM/yyyy}):  ║");
        Console.WriteLine("╚═══════════════════════════════════════════╝");



        foreach (var vehicle in selected)
        {


            var vehicleTrips = trips.Where(t => t.VehicleId == vehicle.VehicleId && t.Date.Date == today).ToList();

            Console.WriteLine("======================================================================================");
            if (vehicleTrips.Count != 0)
            {
                double totalDistance = 0;
                double totalFuelEfficiency = 0;
                int tripCount = 0;

                Console.WriteLine($"{vehicle.VehicleLicence.ToUpper()}:\n");

                Console.WriteLine("╔══════════╦══════════════╦═══════════════════════════╦═══════════╦══════════════════╗");
                Console.WriteLine("║ Trip ID  ║  Vehicle ID  ║           Driver          ║ Distance  ║  Fuel Efficiency ║");
                Console.WriteLine("╠══════════╬══════════════╬═══════════════════════════╬═══════════╬══════════════════╣");
                foreach (var trip in trips)
                {
                    if (trip.VehicleId == vehicle.VehicleId)
                    {
                        string DriverFullName = "";
                        foreach (var driver in drivers)
                        {
                            if (driver.DriverNumber == trip.DriverNumber)
                            {
                                DriverFullName = driver.DisplayFullname();
                            }
                        }


                        Console.WriteLine($"║ {trip.TripId,6}   ║  {trip.VehicleId,5}       ║   {DriverFullName,20}    ║ {trip.Distance,5} km  ║    {trip.FuelEfficiency:00.00} L/km    ║");
                        totalDistance += trip.Distance;
                        totalFuelEfficiency += trip.FuelEfficiency;
                        tripCount++;
                    }
                }
                Console.WriteLine("╚══════════╩══════════════╩═══════════════════════════╩═══════════╩══════════════════╝");


                Console.WriteLine("                     ╔══════════════════════════════════════════╗");
                Console.WriteLine("                     ║ SUMMARY:                                 ║");
                Console.WriteLine($"                     ║    Total Distance: {totalDistance:0000.00} km            ║");
                Console.WriteLine($"                     ║    Average Fuel Efficiency: {totalFuelEfficiency / tripCount:00.00} L/km   ║");
                Console.WriteLine("                     ╚══════════════════════════════════════════╝");
            }
            else
            {
                Console.WriteLine($"{vehicle.VehicleLicence.ToUpper()}:");
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

        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════════════════════════╗");
        Console.WriteLine($"║  WEEKLY REPORT FOR Vehicels ({oneWeekAgo:dd/MM/yyyy} - {DateTime.Today:dd/MM/yyyy}):  ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════╝");


        foreach (var vehicle in selectedVehicles)
        {
            var vehicleTrips = trips.Where(t => t.VehicleId == vehicle.VehicleId && t.Date >= oneWeekAgo && t.Date <= DateTime.Today).ToList();
            Console.WriteLine("======================================================================================");
            if (vehicleTrips.Count != 0)
            {
                double totalDistance = 0;
                double totalFuelEfficiency = 0;
                int tripCount = 0;

                Console.WriteLine($"{vehicle.VehicleLicence.ToUpper()}:\n");

                Console.WriteLine("╔══════════╦══════════════╦═══════════════════════════╦═══════════╦══════════════════╗");
                Console.WriteLine("║ Trip ID  ║  Vehicle ID  ║           Driver          ║ Distance  ║  Fuel Efficiency ║");
                Console.WriteLine("╠══════════╬══════════════╬═══════════════════════════╬═══════════╬══════════════════╣");
                foreach (var trip in trips)
                {
                    if (trip.VehicleId == vehicle.VehicleId)
                    {
                        string DriverFullName = "";
                        foreach (var driver in drivers)
                        {
                            if (driver.DriverNumber == trip.DriverNumber)
                            {
                                DriverFullName = driver.DisplayFullname();
                            }
                        }


                        Console.WriteLine($"║ {trip.TripId,6}   ║  {trip.VehicleId,5}       ║   {DriverFullName,20}    ║ {trip.Distance,5} km  ║    {trip.FuelEfficiency:00.00} L/km    ║");
                        totalDistance += trip.Distance;
                        totalFuelEfficiency += trip.FuelEfficiency;
                        tripCount++;
                    }
                }
                Console.WriteLine("╚══════════╩══════════════╩═══════════════════════════╩═══════════╩══════════════════╝");


                Console.WriteLine("                     ╔══════════════════════════════════════════╗");
                Console.WriteLine("                     ║ SUMMARY:                                 ║");
                Console.WriteLine($"                     ║    Total Distance: {totalDistance:0000.00} km            ║");
                Console.WriteLine($"                     ║    Average Fuel Efficiency: {totalFuelEfficiency / tripCount:00.00} L/km   ║");
                Console.WriteLine("                     ╚══════════════════════════════════════════╝");
            }
            else
            {
                Console.WriteLine($"{vehicle.VehicleLicence.ToUpper()}:");
                Console.WriteLine("  No Trips in selected timeframe\n");
            }
        }
        Console.WriteLine("Press Any Key To Continue:");
        Console.ReadKey();
    }

    private static void GenerateMonthlyReportVehicles(List<Vehicle> vehicles, List<Driver> drivers, List<Trip> trips)
    {
        List<Vehicle> selectedVehicles = SelectVehicles(vehicles);

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
        Console.WriteLine("╔═══════════════════════════════════════════╗");
        Console.WriteLine($"║  MONTHLY REPORT FOR VEHICLES ({monthName.ToUpper(),9}): ║");
        Console.WriteLine("╚═══════════════════════════════════════════╝");


        foreach (var vehicle in selectedVehicles)
        {
            var vehicleTrips = trips.Where(t => t.VehicleId == vehicle.VehicleId && t.Date.Month == chosenMonth && t.Date.Year == DateTime.Today.Year).ToList();
            Console.WriteLine("======================================================================================");
            if (vehicleTrips.Count != 0)
            {
                double totalDistance = 0;
                double totalFuelEfficiency = 0;
                int tripCount = 0;

                Console.WriteLine($"{vehicle.VehicleLicence.ToUpper()}:\n");

                Console.WriteLine("╔══════════╦══════════════╦═══════════════════════════╦═══════════╦══════════════════╗");
                Console.WriteLine("║ Trip ID  ║  Vehicle ID  ║           Driver          ║ Distance  ║  Fuel Efficiency ║");
                Console.WriteLine("╠══════════╬══════════════╬═══════════════════════════╬═══════════╬══════════════════╣");
                foreach (var trip in trips)
                {
                    if (trip.VehicleId == vehicle.VehicleId)
                    {
                        string DriverFullName = "";
                        foreach (var driver in drivers)
                        {
                            if (driver.DriverNumber == trip.DriverNumber)
                            {
                                DriverFullName = driver.DisplayFullname();
                            }
                        }


                        Console.WriteLine($"║ {trip.TripId,6}   ║  {trip.VehicleId,5}       ║   {DriverFullName,20}    ║ {trip.Distance,5} km  ║    {trip.FuelEfficiency:00.00} L/km    ║");
                        totalDistance += trip.Distance;
                        totalFuelEfficiency += trip.FuelEfficiency;
                        tripCount++;
                    }
                }
                Console.WriteLine("╚══════════╩══════════════╩═══════════════════════════╩═══════════╩══════════════════╝");


                Console.WriteLine("                     ╔══════════════════════════════════════════╗");
                Console.WriteLine("                     ║ SUMMARY:                                 ║");
                Console.WriteLine($"                     ║    Total Distance: {totalDistance:0000.00} km            ║");
                Console.WriteLine($"                     ║    Average Fuel Efficiency: {totalFuelEfficiency / tripCount:00.00} L/km   ║");
                Console.WriteLine("                     ╚══════════════════════════════════════════╝");
            }
            else
            {
                Console.WriteLine($"{vehicle.VehicleLicence.ToUpper()}:");
                Console.WriteLine("  No Trips in selected timeframe\n");
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
            Console.WriteLine("Vehicles:");
            for (int i = 0; i < vehicles.Count; i++)
            {
                Console.WriteLine($"Vehicle ID: {vehicles[i].VehicleId}. {vehicles[i].Make} {vehicles[i].Model}");
            }
            Console.WriteLine("\nType Vehicle's ID (eg. 2) to add to Report List.\n");
            Console.WriteLine("Type A for report on all Vehicles.");
            Console.WriteLine("Type G to generate report for selected Vehicles.");
            Console.Write("\nOption:");

            string input = Console.ReadLine();
            Console.WriteLine("");


            switch (input.ToUpper())
            {
                case "G":
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

                case "A":
                    return vehicles;

                default:
                    {
                        bool found = false;
                        if (int.TryParse(input, out int choice))
                        {
                            foreach (var vehicle in vehicles)
                            {
                                if (vehicle.VehicleId == choice)
                                {
                                    bool exist = false;
                                    foreach (var selectedVehicle in selectedVehicles)
                                    {
                                        if (selectedVehicle.VehicleId == vehicle.VehicleId)
                                        {
                                            Console.WriteLine("Vehicel aready added to the list!");
                                            Console.ReadKey();
                                            found = true;
                                            exist = true;
                                            break;
                                        }
                                    }
                                    if (!exist)
                                    {
                                        selectedVehicles.Add(vehicle);
                                        Console.WriteLine("Selected Vehicle Added To List!");
                                        Console.ReadKey();
                                        found = true;
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Please choose a valid option");
                            break;
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
    }
}







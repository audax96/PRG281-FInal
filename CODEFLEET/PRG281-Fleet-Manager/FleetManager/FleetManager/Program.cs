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

        void DriverLanding()
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
                    break;
                case "0":
                    loggedin = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }

        

      

        void DisplayReports()
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

       

        void ViewVehicles(List<Vehicle> vehicles)
        {
            Console.WriteLine("Vehicles in the system:");
            foreach (var vehicle in vehicles)
            {
                Console.WriteLine($"ID: {vehicle.VehicleId}, Make: {vehicle.Make}, Model: {vehicle.Model}, Year: {vehicle.Year}, Fuel: {vehicle.FuelType}, Odometer: {vehicle.OdometerReading}");
            }
            Console.ReadKey();
        }

        void LogTrip(List<Vehicle> vehicles, List<Trip> trips, DataManager dataManager)
        {
            Console.Clear();
            Console.WriteLine("Select a Vehicle by Number:");

            // Display all vehicles with a numeric index
            for (int i = 0; i < vehicles.Count; i++)
            {
                Console.WriteLine($"{i + 1}. ID: {vehicles[i].VehicleId} - {vehicles[i].Make} {vehicles[i].Model} (Odometer: {vehicles[i].OdometerReading} km)");
            }

            // Prompt user to select a vehicle by number
            Console.WriteLine("Enter the number corresponding to the vehicle you want to select:");
            int selectedIndex;
            bool isValid = int.TryParse(Console.ReadLine(), out selectedIndex) && selectedIndex > 0 && selectedIndex <= vehicles.Count;

            if (!isValid)
            {
                Console.WriteLine("Invalid selection. Please try again.");
                Console.ReadKey();
                return;
            }

            // Get the selected vehicle
            var selectedVehicle = vehicles[selectedIndex - 1];

            //Get Driver ID
            Console.WriteLine("Select Driver by Number:");
            for (int i = 0; i < drivers.Count; i++)
            {
                Console.WriteLine($"Driver: {drivers[i].DriverNumber}, {drivers[i].DisplayFullname()}");
            }
            var driverNumber = Int32.Parse(Console.ReadLine());

            // Use the selected vehicle's current odometer as the start odometer
            double startOdometer = selectedVehicle.OdometerReading;
            Console.WriteLine($"Start Odometer Reading: {startOdometer} km");

            // Prompt user to enter the end odometer reading
            Console.WriteLine("Enter End Odometer Reading:");
            double endOdometer = double.Parse(Console.ReadLine());

            // Ensure that the end odometer is greater than the start odometer
            if (endOdometer < startOdometer)
            {
                Console.WriteLine("End odometer must be greater than start odometer. Please try again.");
                Console.ReadKey();
                return;
            }

            // Calculate the distance traveled
            double distanceTraveled = endOdometer - startOdometer;

            // Prompt user to enter the fuel used
            Console.WriteLine("Enter Fuel Used (in liters):");
            double fuelUsed = double.Parse(Console.ReadLine());

            // Calculate fuel efficiency (km per liter)
            double fuelEfficiency = distanceTraveled / fuelUsed;
            Console.WriteLine($"Fuel Efficiency: {fuelEfficiency} km/l");

            // Prompt user to enter the date of the trip
            Console.WriteLine("Enter the Date of the Trip (dd/mm/yyyy):");
            DateTime date;
            while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out date))
            {
                Console.WriteLine("Invalid date format. Please enter the date in dd/mm/yyyy format:");
            }

            // Remove the time component by resetting it to midnight
            date = date.Date;

            // Prompt user to enter the price per liter
            Console.WriteLine("Enter the Price per Liter (in your currency):");
            double pricePerLiter = double.Parse(Console.ReadLine());

            int lastTripId = 0;
            if (trips.Count > 0)
            {
                Trip lastTrip = trips.Last();
                lastTripId = lastTrip.VehicleId;
            }
            // Create a new Trip object
            Trip trip = new Trip
            {
                TripId = lastTripId + 1,
                VehicleId = selectedVehicle.VehicleId,
                DriverNumber = driverNumber,
                StartOdometer = startOdometer,
                EndOdometer = endOdometer,
                FuelUsed = fuelUsed,
                FuelEfficiency = fuelEfficiency,
                Date = date,
                PricePerLiter = pricePerLiter,
                Distance = endOdometer - startOdometer,
            };

            // Add the trip to the list and save to the JSON file
            trips.Add(trip);
            dataManager.SaveTrips(trips);

            // Update the vehicle's odometer to the end odometer of the trip
            selectedVehicle.OdometerReading = endOdometer;
            dataManager.SaveVehicles(vehicles);

            Console.WriteLine("Trip logged successfully, with fuel efficiency calculated and vehicle odometer updated!");
            Console.ReadKey();
        }

        void ViewTrips(List<Trip> trips)
        {
            Console.WriteLine("Trips logged:");
            foreach (var trip in trips)
            {
                Console.WriteLine($"Trip ID: {trip.TripId}, Vehicle ID: {trip.VehicleId}, Distance: {trip.CalculateDistance()} km, Fuel Used: {trip.FuelUsed} L, Date: {trip.Date}");
            }
            Console.ReadKey();
        }


    }
}
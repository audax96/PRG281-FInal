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
                AdminDisplay();
            }
            else if (userRole == 3)
            {
                DriverLanding();
            }
            else if (userRole == 4)
            {
                FinanceLanding();
            }
        }

        void FinanceLanding()
        {
            Console.Clear();
            Console.WriteLine("╔═════════════════════════════════════════╗");
            Console.WriteLine("║           Finance Home Page             ║");
            Console.WriteLine("╚═════════════════════════════════════════╝");
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

        void AdminDisplay()
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
                Console.WriteLine("6. Remove Driver");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                string option = Console.ReadLine();
                switch (option)
                {
                    case "1": AddVehicle(vehicles, dataManager); break;

                    case "2": AddDriver(drivers, dataManager); break;

                    case "3": UserManagement.CreateUser(users); break;

                    case "4": vehicles = Admin.RemoveVehicle(vehicles); break;

                    case "5": drivers = Admin.RemoveDriver(drivers); break;
                    case "6": break;

                    case "0": return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
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

        void AddVehicle(List<Vehicle> vehicles, DataManager dataManager)
        {
            Console.WriteLine("Enter Vehicle ID:");
            int vehicleId = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Enter Make:");
            string make = Console.ReadLine();

            Console.WriteLine("Enter Model:");
            string model = Console.ReadLine();

            Console.WriteLine("Enter Year:");
            int year = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Fuel Type:");
            string fuelType = Console.ReadLine();

            Console.WriteLine("Enter Odometer Reading:");
            double odometer = double.Parse(Console.ReadLine());

            Vehicle vehicle = new Vehicle
            {
                VehicleId = vehicleId,
                Make = make,
                Model = model,
                Year = year,
                FuelType = fuelType,
                OdometerReading = odometer
            };

            vehicles.Add(vehicle);
            dataManager.SaveVehicles(vehicles);
            Console.WriteLine("Vehicle added successfully!");
            Console.ReadKey();
        }

        void AddDriver(List<Driver> drivers, DataManager dataManager)
        {
            string driverGender = "";
            bool valid = false;
            Console.Write("Enter Driver's ID Number: ");
            string driverid = Console.ReadLine();
            Console.Write("Enter Driver's Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Driver's Surname: ");
            string surname = Console.ReadLine();
            Console.Write("Enter Driver's Date of Birth(dd/mm/yyyy): ");
            string dob = Console.ReadLine();
            while (!valid)
            {
                Console.WriteLine("Enter Driver's Gender:");
                Console.WriteLine("1: Male");
                Console.WriteLine("2: Female");
                string ans = Console.ReadLine();
                switch (ans)
                {
                    case "1":
                        driverGender = "Male";
                        valid = true;
                        break;
                    case "2":
                        driverGender = "Female";
                        valid = true;
                        break;
                    default:
                        Console.WriteLine("Please Enter A Valid Option! Press Anykey To Proceed.");
                        Console.ReadKey();
                        break;
                }
            }

            int lastDriverNo = 0;
            if (drivers.Count > 0)
            {
                Driver LastDriver = drivers.Last();
                lastDriverNo = LastDriver.DriverNumber;
            }
            Driver driver = new Driver
            {
                DriverNumber = lastDriverNo + 1,
                DriverID = driverid,
                Name = name,
                Surname = surname,
                DOB = DateTime.Parse(dob),
                Gender = driverGender,
            };
            drivers.Add(driver);
            dataManager.SaveDrivers(drivers);
            Console.WriteLine("Driver added successfully!");
            Console.ReadKey();


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

            // Create a new Trip object
            Trip trip = new Trip
            {
                TripId = Guid.NewGuid().ToString(),
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
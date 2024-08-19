using System.Data;
using System.Globalization;
public class Admin
{

    public static List<Driver> RemoveDriver(List<Driver> drivers)
    {
        DataManager dataManager = new();
        while (true)
        {
            Console.Clear();
            Console.WriteLine("List Of Drivers:");
            for (int i = 0; i < drivers.Count; i++)
            {
                if (drivers[i].Active == true)
                {
                 Console.WriteLine($"Driver: {drivers[i].DriverNumber}, {drivers[i].DisplayFullname()}");   
                }
                
            }
            Console.WriteLine("Select Driver To Remove By Driver Number:");

            string input = Console.ReadLine();
            if (!int.TryParse(input, out int driverNumber))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                Console.ReadKey();
            }
            else
            {
                foreach (var driver in drivers)
                {
                    if (driverNumber == driver.DriverNumber)
                    {
                        driver.Active = false;
                        dataManager.SaveDrivers(drivers);
                        Console.WriteLine("===============================================");
                        Console.WriteLine($"{driver.Name} {driver.Surname} Deactived Successfully");
                        Console.WriteLine("===============================================");
                        Console.ReadKey();
                        return drivers;
                    }
                }
                Console.WriteLine("Invalid Driver Number!");
                Console.ReadKey();
            }

        }
    }

    public static List<Vehicle> RemoveVehicle(List<Vehicle> vehicles)
    {
        DataManager dataManager = new();
        while (true)
        {
            Console.Clear();
            Console.WriteLine("List Of vehicles:");
            for (int i = 0; i < vehicles.Count; i++)
            {
                if (vehicles[i].Active == true)
                {
                Console.WriteLine($"Vehicel: {vehicles[i].VehicleId}, {vehicles[i].Make}, {vehicles[i].Model}");
                }
                
            }
            Console.WriteLine("Select Vehicle To Remove By Vehicel ID:");
            string input = Console.ReadLine();
            if (!int.TryParse(input, out int vehicleId))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                Console.ReadKey();

            }
            else
            {


                foreach (var vehicle in vehicles)
                {

                    if (vehicleId == vehicle.VehicleId)
                    {
                        vehicle.Active = false;
                        dataManager.SaveVehicles(vehicles);
                        Console.WriteLine("===============================================");
                        Console.WriteLine($"{vehicle.Make} {vehicle.Model} Deactivated Successfully");
                        Console.WriteLine("===============================================");
                        Console.ReadKey();
                        return vehicles;
                    }
                }

                Console.WriteLine("Invalid Vehicle Number!");
                Console.ReadKey();
            }
        }
    }

    public static List<Trip> RemoveTrip(List<Trip> trips)
    {
        DataManager dataManager = new DataManager();
        List<Vehicle> vehicles = dataManager.LoadVehicles();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("╔═════════════════════════════════════════╗");
            Console.WriteLine("║               Remove Trip               ║");
            Console.WriteLine("╚═════════════════════════════════════════╝");

            Console.Write("Enter Trip date");
            DateTime dateToRemove = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("List Of Trips:");

            Console.WriteLine("Select Trip To Remove By Trip ID:");
            int tripNumber = int.Parse(Console.ReadLine());
            foreach (var trip in trips)
            {
                if (dateToRemove == trip.Date)
                {
                    Console.WriteLine($"Trip ID: {trip.TripId}, Vehicle ID: {trip.VehicleId}, Distance: {trip.CalculateDistance()} km, Fuel Used: {trip.FuelUsed} L, Date: {trip.Date}");
                }
                if (tripNumber == trip.TripId)
                {
                    foreach (var vehicle in vehicles)
                    {
                        if (vehicle.VehicleId == trip.VehicleId)
                        {
                            vehicle.OdometerReading -= trip.Distance;
                            dataManager.SaveVehicles(vehicles);
                        }
                    }
                    trips.Remove(trip);
                    dataManager.SaveTrips(trips);
                    Console.WriteLine("=========================");
                    Console.WriteLine("Trip Removed Successfully");
                    Console.WriteLine("=========================");
                    Console.ReadKey();
                    return trips;
                }
                else
                {
                    Console.WriteLine("Invalid driver number!");
                    Console.ReadKey();
                    break;
                }
            }
        }
    }

    public static void AddVehicle(List<Vehicle> vehicles, DataManager dataManager)
    {
        Console.Clear();
        Console.WriteLine("╔═════════════════════════════════════════╗");
        Console.WriteLine("║               ADD VEHICLE               ║");
        Console.WriteLine("╚═════════════════════════════════════════╝");
        Console.Write("License No:");
        string vehicleLicenceNo = Console.ReadLine();

        Console.Write("Make:");
        string make = Console.ReadLine();

        Console.Write("Model:");
        string model = Console.ReadLine();

        Console.Write("Year:");
        int year = int.Parse(Console.ReadLine());

        Console.Write("Fuel Type:");
        string fuelType = Console.ReadLine();

        Console.Write("Odometer Reading:");
        double odometer = double.Parse(Console.ReadLine());

        int lastVehicleId = 0;
        if (vehicles.Count > 0)
        {
            Vehicle lastVehicle = vehicles.Last();
            lastVehicleId = lastVehicle.VehicleId;
        }

        Vehicle vehicle = new()
        {
            VehicleId = lastVehicleId + 1,
            VehicleLicence = vehicleLicenceNo,
            Make = make,
            Model = model,
            Year = year,
            FuelType = fuelType,
            OdometerReading = odometer,
            Active = true
        };

        vehicles.Add(vehicle);
        dataManager.SaveVehicles(vehicles);
        Console.WriteLine( "============================================================================");
        Console.WriteLine($"{vehicle.Make} {vehicle.Model}, {vehicle.VehicleLicence} added successfully!");
        Console.WriteLine( "============================================================================");
        Console.ReadKey();
    }

    public static void AddDriver(List<Driver> drivers, DataManager dataManager)
    {
        Console.Clear();
        Console.WriteLine("╔═════════════════════════════════════════╗");
        Console.WriteLine("║                ADD DRIVER               ║");
        Console.WriteLine("╚═════════════════════════════════════════╝");
        string driverGender = "";
        bool valid = false;
        Console.Write("ID Number: ");
        string driverid = Console.ReadLine();
        Console.Write("Name: ");
        string name = Console.ReadLine();
        Console.Write("Surname: ");
        string surname = Console.ReadLine();
        Console.Write("Date of Birth(dd/mm/yyyy): ");
        string dob = Console.ReadLine();
        while (!valid)
        {
            Console.WriteLine("Gender:");
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
            Active = true,
        };
        drivers.Add(driver);
        dataManager.SaveDrivers(drivers);
        Console.WriteLine( "==============================================");
        Console.WriteLine($"{driver.DisplayFullname()} added successfully!");
        Console.WriteLine( "==============================================");
        Console.ReadKey();

    }

    public static void AddFinance(List<Finance> finances, List<Trip> trips, DataManager dataManager)
    {
        foreach (var trip in trips)
        {
            bool found = false;
            foreach (var record in finances)
            {
                if (trip.TripId == record.TripNumber)
                {
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                Finance finance = new Finance()
                {
                    TripNumber = trip.TripId,
                    KMsLogged = trip.Distance,
                    Handled = false,
                    CostOfTrip = trip.CalculateDistance(),
                    DateOfTrip = trip.Date
                };
                finances.Add(finance);
                dataManager.SaveFinance(finances);
            }
        }
    }
}
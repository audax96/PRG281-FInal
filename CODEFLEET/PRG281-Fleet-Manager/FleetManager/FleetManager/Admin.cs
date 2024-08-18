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
                Console.WriteLine($"Driver: {drivers[i].DriverNumber}, {drivers[i].DisplayFullname()}");
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
                        drivers.Remove(driver);
                        dataManager.SaveDrivers(drivers);
                        Console.WriteLine("Driver Removed Successfully");
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
                Console.WriteLine($"Vehicel: {vehicles[i].VehicleId}, {vehicles[i].Make}, {vehicles[i].Model}");
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
                        vehicles.Remove(vehicle);
                        dataManager.SaveVehicles(vehicles);
                        Console.WriteLine("Vehicel Removed Successfully");
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

            Console.Write("Enter Trip date");
            DateTime dateToRemove = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("List Of Trips:");

            Console.WriteLine("Select Trip To Remove By Trip ID:");
            var tripNumber = Console.ReadLine();
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
                    Console.WriteLine("Trip Removed Successfully");
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

}
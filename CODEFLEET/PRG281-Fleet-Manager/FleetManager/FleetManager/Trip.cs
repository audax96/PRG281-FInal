using System.Globalization;

public class Trip : Entity
{
    public required int TripId { get; set; }
    public required int VehicleId { get; set; }
    public required int DriverNumber { get; set; }
    public double StartOdometer { get; set; }
    public double EndOdometer { get; set; }
    public double FuelUsed { get; set; }
    public double FuelEfficiency { get; set; }  // New property for fuel efficiency
    public DateTime Date { get; set; }
    public double PricePerLiter { get; set; }

    public double Distance { get; set; }

    static DataManager dataManager = new DataManager();
    static List<Driver> drivers = dataManager.LoadDrivers();

    public double CalculateDistance()
    {
        return EndOdometer - StartOdometer;
    }

    public static void AddEntity(List<Vehicle> vehicles, List<Trip> trips, DataManager dataManager)
    {
        Console.Clear();
        Console.WriteLine("Select a Vehicle by Number:");

        // Display all vehicles with a numeric index
        for (int i = 0; i < vehicles.Count; i++)
        {
            Console.WriteLine($"{i + 1}. ID: {vehicles[i].VehicleId} - {vehicles[i].Make} {vehicles[i].Model} (Odometer: {vehicles[i].OdometerReading} km)");
        }

        // Prompt user to select a vehicle by number
        Console.WriteLine("Enter the number corresponding to the vehicle you want to select (Enter '0' to exit):");
        int selectedIndex;
        bool isValid = int.TryParse(Console.ReadLine(), out selectedIndex) && selectedIndex >= 0 && selectedIndex <= vehicles.Count;
        if (selectedIndex == 0)
        {
            return;
        }
        if (!isValid)
        {
            Console.WriteLine("Invalid selection. Please try again.");
            Console.ReadKey();
            return;
        }

        // Get the selected vehicle
        var selectedVehicle = vehicles[selectedIndex - 1];

        //Get Driver ID
        Console.WriteLine("Select Driver by Number (Enter '0' to exit):");
        for (int i = 0; i < drivers.Count; i++)
        {
            Console.WriteLine($"Driver: {drivers[i].DriverNumber}, {drivers[i].DisplayFullname()}");
        }
        var driverNumber = Int32.Parse(Console.ReadLine());
        if (driverNumber == 0)
        {
            return;
        }

        // Use the selected vehicle's current odometer as the start odometer
        double startOdometer = selectedVehicle.OdometerReading;
        Console.WriteLine($"Start Odometer Reading: {startOdometer} km");

        // Prompt user to enter the end odometer reading
        Console.WriteLine("Enter End Odometer Reading (Enter '0' to exit):");
        double endOdometer = double.Parse(Console.ReadLine());
        if (endOdometer == 0)
        {
            return;
        }

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
        Console.WriteLine("Enter Fuel Used (in liters), (Enter '0' to exit):");
        double fuelUsed = double.Parse(Console.ReadLine());

        if (fuelUsed == 0)
        {
            return;
        }

        // Calculate fuel efficiency (km per liter)
        double fuelEfficiency = distanceTraveled / fuelUsed;
        Console.WriteLine($"Fuel Efficiency: {fuelEfficiency} km/l ");

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
        Console.WriteLine("Enter the Price per Liter (in your currency), (Enter '0' to exit):");
        double pricePerLiter = double.Parse(Console.ReadLine());
        if (pricePerLiter == 0)
        {
            return;
        }

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
        Console.WriteLine("=======================================================================================");
        Console.WriteLine("Trip logged successfully, with fuel efficiency calculated and vehicle odometer updated!");
        Console.WriteLine("=======================================================================================");
        Console.ReadKey();
    }


}



public class Vehicle
{
    public required int VehicleId { get; set; }
    public required string VehicleLicence { get; set; }
    public required string Make { get; set; }
    public required string Model { get; set; }
    public required int Year { get; set; }
    public required string FuelType { get; set; }
    public required double OdometerReading { get; set; }
    public required bool Active {get; set;}
}

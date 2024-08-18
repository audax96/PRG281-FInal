using System.Globalization;

public class Trip
{
    public required string TripId { get; set; }
    public required int VehicleId { get; set; }
    public required int DriverNumber{ get; set; }
    public double StartOdometer { get; set; }
    public double EndOdometer { get; set; }
    public double FuelUsed { get; set; }
    public double FuelEfficiency { get; set; }  // New property for fuel efficiency
    public DateTime Date { get; set; }
    public double PricePerLiter { get; set; }

    public double Distance { get; set; }


    public double CalculateDistance()
    {
        return EndOdometer - StartOdometer;
    }

    
}



public class Driver
{
    public required int DriverNumber { get; set; }
    public required string DriverID { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required DateTime DOB { get; set; }
    public required string Gender { get; set; }

    public required bool Active {get;set;}

    public string DisplayFullname()
    {
        return Name + " " + Surname;
    }

   

}


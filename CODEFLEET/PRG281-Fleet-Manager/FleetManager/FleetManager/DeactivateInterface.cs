interface IDisplay
{
 bool AdminLanding(List<Trip> trips,List<Vehicle> vehicles, List<Driver> drivers, List<User>users, List<Finance>finance, string loggedInName);

 bool DriverLanding(List<Driver> drivers, List<Trip> trips, List<Vehicle> vehicles, string loggedInName);

 void FinanceLanding(List<Finance> finances, List<Trip> trips, string loggedInName);
}
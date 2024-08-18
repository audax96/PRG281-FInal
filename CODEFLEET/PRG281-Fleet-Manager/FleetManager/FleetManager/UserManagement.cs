using System.Numerics;

public class UserManagement
{
    public static DataManager dataManager = new();

    public static int Login(List<User> users)
    {
        bool valid = false;
        while (!valid)
        {
            Console.Write("Enter Email: ");
            string email = Console.ReadLine();
            Console.Write("Password: ");
            string password = GetPassword();
            bool active = true;
            foreach (var user in users)
            {
                if (email == user.Email)
                {
                    if (password == user.Password)
                    {
                        if (user.Active == true)
                        {
                            Console.WriteLine("\n=============================================\nSuccessful Login!! Press Any Key To Continue: ");
                            Console.ReadKey();
                            valid = true;
                            return user.Role;
                        }
                        else
                        {
                            active = false;
                        }
                    }
                }
            }
            if (active == false)
            {
                Console.WriteLine("\n=============================\nUser Is Deactivated!");
                Console.ReadKey();
            }
            else if (valid == false)
            {
                Console.WriteLine("\n=============================\nInvalid Email or Password. \n=============================\nPlease try agian:");
                Console.ReadKey();
            }
        }
        return 0;
    }

    public static void CreateUser(List<User> users)
    {
        Console.Write("Enter Your National ID: ");
        string id = Console.ReadLine();
        Console.Write("Name: ");
        string name = Console.ReadLine();
        Console.Write("Surname: ");
        string surname = Console.ReadLine();
        Console.Write("\nRole: \n1. Owner, \n2. Admin, \n3. Driver, \n4. Finance\n");
        Console.Write("Choose Users Role:");
        int role = Int32.Parse(Console.ReadLine());
        Console.Write("Email: ");
        string email = Console.ReadLine();
        Console.Write("Password: ");
        string password = GetPassword();
        if (!id.Any() || !name.Any() || !surname.Any() || role > 4 || !email.Any() || !password.Any())
        {
            Console.WriteLine("not all fields are filled in!");
            Console.ReadKey();
        }
        int lastUserNo = 0;
        if (users.Count > 0)
        {
            User lastUser = users.Last();
            lastUserNo = lastUser.UserNo;
        }

        User user = new User
        {
            UserNo = lastUserNo + 1,
            UserID = id,
            Name = name,
            Surname = surname,
            Role = role,
            Email = email,
            Password = password,
            Active = true,
        };
        users.Add(user);
        dataManager.SaveUser(users);
        Console.WriteLine("\n=============================\n User added successfully!");
        Console.ReadKey();


    }

    public static List<User> DeactivateUser(List<User> users)
    {
        DataManager dataManager = new DataManager();
        List<Vehicle> vehicles = dataManager.LoadVehicles();
        Console.Clear();

        Console.WriteLine("╔═════════════════════════════════════════╗");
        Console.WriteLine("║               Activate Users            ║");
        Console.WriteLine("╚═════════════════════════════════════════╝");
        foreach (var user in users)
        {
            if (user.Active == true)
            {
               Console.WriteLine($"|| User Number: {user.UserNo} || User Name: {user.Name}  {user.Surname}||");  
            }
        }
        Console.WriteLine("╔═════════════════════════════════════════╗");
        Console.WriteLine("║             Inactivate Users            ║");
        Console.WriteLine("╚═════════════════════════════════════════╝");
        foreach (var user in users)
        {
            if (user.Active == false)
            {
               Console.WriteLine($"|| User Number: {user.UserNo} || User Name: {user.Name}  {user.Surname}||"); 
            }
        }

        Console.Write("Choose A user by ID: ");
        string input = Console.ReadLine();
        if (!int.TryParse(input, out int id))
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
            Console.ReadKey();
        }
        else
        {

            foreach (var user in users)
            {
                if (user.UserNo == id)
                {
                    if (user.Active == true)
                    {
                        user.Active = false;
                        dataManager.SaveUser(users);
                        Console.WriteLine("User Successfully Deactivated");
                        Console.ReadKey();
                        break;
                    }
                    else
                    {
                        user.Active = true;
                        dataManager.SaveUser(users);
                        Console.WriteLine("User Successfully Deactivated");
                        Console.ReadKey();
                        break;
                    }
                }
            }
        }
        return users;

    }

    static string GetPassword()
    {
        string password = string.Empty;
        ConsoleKeyInfo key;

        do
        {
            key = Console.ReadKey(true);

            // Check if the key is not Backspace or Enter
            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                password += key.KeyChar;
                Console.Write("*");
            }
            else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password = password[0..^1];
                Console.Write("\b \b");
            }
        } while (key.Key != ConsoleKey.Enter);

        return password;
    }

}
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
        Console.Write("Role (\n1. Owner, \n2. Admin, \n3. Driver, \n4. Finance): ");
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

        User user = new User
        {
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
        Console.WriteLine("║              Deactivate User            ║");
        Console.WriteLine("╚═════════════════════════════════════════╝");
        foreach (var user in users)
        {
            Console.WriteLine($"|| User ID: {user.UserID} || User First Name: {user.Name} || User Surname: {user.Surname}");
        }

        Console.Write("Choose A user by ID: ");
        string id = Console.ReadLine();
        foreach (var user in users)
        {
            if (user.UserID == id)
            {
                user.Active = false;
                dataManager.SaveUser(users);
                Console.WriteLine("User Successfully Deactivated");
                Console.ReadKey();
                break;
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
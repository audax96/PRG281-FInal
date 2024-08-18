public class UserManagement
{
    public static DataManager dataManager = new();

    public static void Login(List<User> users)
    {
        bool valid = false;
        while (!valid)
        {
            Console.Write("Enter Email: ");
            string email = Console.ReadLine();
            Console.Write("Password: ");
            string password = GetPassword();
            foreach (var user in users)
            {
                if (email == user.Email)
                {
                    if (password == user.Password)
                    {
                        Console.WriteLine("\n=============================================\nSuccessful Login!! Press Any Key To Continue: ");
                        Console.ReadKey();
                        valid = true;
                        break;

                    }
                }
            }
            if (valid == false)
            {
                Console.WriteLine("\n=============================\nInvalid Email or Password. \n=============================\nPlease try agian:");
            }
        }
    }

    public static void CreateUser(List<User> users)
    {
        Console.Write("Enter Your National ID: ");
        string id = Console.ReadLine();
        Console.Write("Name: ");
        string name = Console.ReadLine();
        Console.Write("Surname: ");
        string surname = Console.ReadLine();
        Console.Write("Role (Admin, Owner, Driver, Finance): ");
        string role = Console.ReadLine();
        Console.Write("Email: ");
        string email = Console.ReadLine();
        Console.Write("Password: ");
        string password = GetPassword();
        if (!id.Any() || !name.Any() || !surname.Any() || !role.Any() || !email.Any() || !password.Any())
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
        };
        users.Add(user);
        dataManager.SaveUser(users);
        Console.WriteLine("\n=============================\n User added successfully!");
        Console.ReadKey();


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
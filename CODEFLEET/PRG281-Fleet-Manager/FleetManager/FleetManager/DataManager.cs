using System.Text;
using System.Text.Json;
using System.Security.Cryptography;

public class DataManager
{
    private readonly string vehicleFilePath = "vehicles.json";
    private readonly string tripFilePath = "trips.json";
    private readonly string driverFilePath = "drivers.json";
    private readonly string userFilePath = "users.json";
    private readonly string financeFilePath = "finance.json";

    private static readonly byte[] Key = Encoding.UTF8.GetBytes("0123456789abcdef"); // 16 bytes for AES-128
    private static readonly byte[] IV = Encoding.UTF8.GetBytes("abcdef9876543210"); // 16 bytes

    public List<Vehicle> LoadVehicles() => LoadJsonFile<List<Vehicle>>(vehicleFilePath);
    public List<Trip> LoadTrips() => LoadJsonFile<List<Trip>>(tripFilePath);
    public List<Driver> LoadDrivers() => LoadJsonFile<List<Driver>>(driverFilePath);
    public List<User> LoadUser() => LoadJsonFile<List<User>>(userFilePath, true);

    public List<Finance> LoadFinance() => LoadJsonFile<List<Finance>>(financeFilePath, true);

    public void SaveVehicles(List<Vehicle> vehicles) => SaveJsonFile(vehicleFilePath, vehicles);
    public void SaveTrips(List<Trip> trips) => SaveJsonFile(tripFilePath, trips);
    public void SaveDrivers(List<Driver> drivers) => SaveJsonFile(driverFilePath, drivers);
    public void SaveUser(List<User> users) => SaveJsonFile(userFilePath, users, true);

    public void SaveFinance(List<Finance> finance) => SaveJsonFile(financeFilePath, finance);

    private static T LoadJsonFile<T>(string path, bool decryptPasswords = false)
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            var data = JsonSerializer.Deserialize<T>(json);
            if (decryptPasswords && data is List<User> userList)
            {
                foreach (var user in userList)
                    user.Password = Decrypt(user.Password);
            }
            return data;
        }
        return default;
    }

    private static void SaveJsonFile<T>(string path, T data, bool encryptPasswords = false)
    {
        string json;
        if (encryptPasswords && data is List<User> userList)
        {
            foreach (var user in userList)
                user.Password = Encrypt(user.Password);
            json = JsonSerializer.Serialize(data);
            foreach (var user in userList) // Decrypt back to avoid altering in-memory data
                user.Password = Decrypt(user.Password);
        }
        else
        {
            json = JsonSerializer.Serialize(data);
        }
        File.WriteAllText(path, json);
    }

    private static string Encrypt(string plainText)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Key;
            aes.IV = IV;
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using MemoryStream ms = new MemoryStream();
            using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (StreamWriter sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
            }
            string encrypted = Convert.ToBase64String(ms.ToArray());
            return encrypted;
        }
    }

    private static string Decrypt(string cipherText)
    {

        using Aes aes = Aes.Create();
        aes.Key = Key;
        aes.IV = IV;
        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(cipherText)))
        using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
        using (StreamReader sr = new StreamReader(cs))
        {
            return sr.ReadToEnd();
        }
    }

}

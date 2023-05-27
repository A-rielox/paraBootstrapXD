using API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text;
using Newtonsoft.Json;

namespace API.Data;

public class Seed
{
    public static async Task SeedUsers(DataContext context)
    {
        if (await context.Users.AnyAsync()) return;

        var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

        // var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var users = JsonConvert.DeserializeObject<List<AppUser>>(userData);
        // The solution was to change from System.Text.Json to Newtonsoft Json with this line

        foreach (var user in users)
        {
            using var hmac = new HMACSHA512(); // X IDENTITY

            user.UserName = user.UserName.ToLower();

            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("P@ssword1"));
            user.PasswordSalt = hmac.Key;

            context.Users.Add(user);

        }

        await context.SaveChangesAsync();
    }
}

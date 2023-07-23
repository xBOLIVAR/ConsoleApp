using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Enter user details:");
            Console.Write("Username: ");
            string userName = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            var newUser = new UserModel
            {
                UserName = userName,
                Email = email,
                Password = password
            };

            var controller = new UserController();

            try
            {
                var createdUser = await controller.CreateUser(newUser);
                Console.WriteLine($"User created successfully. Username: {createdUser.UserName}");

                var allUsers = await controller.GetAllUsers();
                Console.WriteLine("Existing users:");
                foreach (var user in allUsers)
                {
                    Console.WriteLine($"Username: {user.UserName}, Email: {user.Email}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }
    }
}

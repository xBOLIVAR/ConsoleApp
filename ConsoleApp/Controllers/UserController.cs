using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class UserController
    {
        private const string BaseUrl = "https://api.ejemplo.com";

        private HttpClient httpClient;

        public UserController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(BaseUrl);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<UserModel> CreateUser(UserModel newUser)
        {
            try
            {
                var json = JsonSerializer.Serialize(newUser);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("/users", content);
                
                if (response.IsSuccessStatusCode)
                {
                    var createdUserJson = await response.Content.ReadAsStringAsync();
                    var createdUser = JsonSerializer.Deserialize<UserModel>(createdUserJson);
                    return createdUser;
                }
                else
                {
                    throw new Exception($"Failed to create user. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating user: {ex.Message}");
            }
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            try
            {
                var response = await httpClient.GetAsync("/users");

                if (response.IsSuccessStatusCode)
                {
                    var userJson = await response.Content.ReadAsStringAsync();
                    var users = JsonSerializer.Deserialize<List<UserModel>>(userJson);
                    return users;
                }
                else
                {
                    throw new Exception($"Failed to get users. Status code: {response.StatusCode}");
                }
                /*var users = new List<UserModel>
                    {
                       new UserModel {  UserName = "user1", Email = "user1@example.com", Password = "password1" },
                       new UserModel {  UserName = "user2", Email = "user2@example.com", Password = "password2" },
                       new UserModel {  UserName = "user3", Email = "user3@example.com", Password = "password3" }
                    };
                return users;*/
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting users: {ex.Message}");
            }
        }
    }
}
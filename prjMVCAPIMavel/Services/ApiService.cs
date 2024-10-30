using prjMVCAPIMavel.Models;
using System.Text.Json;

namespace prjMVCAPIMavel.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        public ApiService(HttpClient httpClient)
        { 
            _httpClient = httpClient;
        }
        public async Task<List<TblAvenger>> GetAvengersAsync()
        {
            var response = await _httpClient.GetAsync("/users");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<TblAvenger>>();
        }
        public async Task<List<TblContact>> GetContactsAsync()
        {
            var response = await _httpClient.GetAsync("/contacts");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<TblContact>>();
        }
        public async Task CreateAvengerAsync(TblAvenger newAvenger)
        {
            var response = await _httpClient.PostAsJsonAsync("/users", newAvenger);
            response.EnsureSuccessStatusCode();
        }

        public async Task CreateContactAsync(TblContact newContact)
        {
            var response = await _httpClient.PostAsJsonAsync("/contacts", newContact);
            response.EnsureSuccessStatusCode();
        }

        // Delete method for TblAvenger
        public async Task DeleteAvengerAsync(string username)
        {
            var response = await _httpClient.DeleteAsync($"/users/{username}");
            response.EnsureSuccessStatusCode();
        }
        // Method to log in a user
        public async Task<string> LoginAsync(LoginViewModel userLoginDto)
        {
            var response = await _httpClient.PostAsJsonAsync("/login", userLoginDto);

            // Ensure the response is successful
            response.EnsureSuccessStatusCode();

            // Read the response as a string
            var jsonString = await response.Content.ReadAsStringAsync();

            // Deserialize the response to a dictionary
            var result = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);

            // Check if the token key exists (note the lowercase 'token')
            if (result != null && result.TryGetValue("token", out var token)) 
            {
                return token; // Return the token from the response
            }

            throw new Exception("Token not found in response"); // Throw an exception if the token is not found
        }


    }
}

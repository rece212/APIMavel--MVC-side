using prjMVCAPIMavel.Models;

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
    }
}

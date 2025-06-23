using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUI.Models.Employee;

namespace WebUI.Services
{
    public class EmployeeService
    {
        private readonly HttpClient _httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<EmployeeViewModel>> GetEmployeesAsync()
        {
            var response = await _httpClient.GetAsync("api/Employee");
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Status: {response.StatusCode}, Content: {content}");

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                return new List<EmployeeViewModel>(); // 204 ise boş liste döndür

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<List<EmployeeViewModel>>();
            return result;
        }

        public async Task<EmployeeViewModel> GetEmployeeByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<EmployeeViewModel>($"api/employee/{id}");
        }

        public async Task AddEmployeeAsync(EmployeeCreateViewModel employee)
        {
            await _httpClient.PostAsJsonAsync("api/employee", employee);
        }

        public async Task UpdateEmployeeAsync(EmployeeUpdateViewModel employee)
        {
            await _httpClient.PutAsJsonAsync("api/employee", employee);
        }

        public async Task DeleteEmployeeAsync(Guid id)
        {
            await _httpClient.DeleteAsync($"api/employee/{id}");
        }
    }
}

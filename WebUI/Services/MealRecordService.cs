using System.Net.Http.Json;
using WebUI.Models.MealRecord;

namespace WebUI.Services
{
    public class MealRecordService
    {
        private readonly HttpClient _httpClient;
        public MealRecordService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<MealRecordViewModel>> GetMealRecordsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<MealRecordViewModel>>("api/mealrecord");
        }
        public async Task<MealRecordViewModel> GetMealRecordByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<MealRecordViewModel>($"api/mealrecord/{id}");
        }

        public async Task<List<MealRecordViewModel>> GetMealRecordsByDateAsync(DateOnly date)
        {
            return await _httpClient.GetFromJsonAsync<List<MealRecordViewModel>>($"api/mealrecord/employee/{date}");

        }

        public async Task AddMealRecordAsync(MealRecordCreateViewModel mealRecord)
        {
            await _httpClient.PostAsJsonAsync("api/Employee", mealRecord);
        }
        public async Task UpdateMealRecordAsync(MealRecordUpdateViewModel mealRecord)
        {
            await _httpClient.PutAsJsonAsync("api/mealrecord", mealRecord);
        }
        public async Task DeleteMealRecordAsync(Guid id)
        {
            await _httpClient.DeleteAsync($"api/mealrecord/{id}");
        }
    }
}

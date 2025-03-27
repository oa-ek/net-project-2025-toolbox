using System.Net.Http.Json;
using Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ConditionService
{
    private readonly HttpClient _httpClient;

    public ConditionService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ConditionDto>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<ConditionDto>>("api/condition");
    }

    public async Task<ConditionDto> GetByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<ConditionDto>($"api/condition/{id}");
    }

    public async Task AddAsync(ConditionDto condition)
    {
        await _httpClient.PostAsJsonAsync("api/condition", condition);
    }

    public async Task UpdateAsync(int id, ConditionDto condition)
    {
        await _httpClient.PutAsJsonAsync($"api/condition/{id}", condition);
    }

    public async Task DeleteAsync(int id)
    {
        await _httpClient.DeleteAsync($"api/condition/{id}");
    }
}


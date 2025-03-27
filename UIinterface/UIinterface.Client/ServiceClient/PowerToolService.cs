using System.Net.Http.Json;
using Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PowerToolService
{
    private readonly HttpClient _httpClient;

    public PowerToolService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<PowerToolDto>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<PowerToolDto>>("api/powertool");
    }

    public async Task<PowerToolDto> GetByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<PowerToolDto>($"api/powertool/{id}");
    }

    public async Task AddAsync(PowerToolDto powerTool)
    {
        await _httpClient.PostAsJsonAsync("api/powertool", powerTool);
    }

    public async Task UpdateAsync(int id, PowerToolDto powerTool)
    {
        await _httpClient.PutAsJsonAsync($"api/powertool/{id}", powerTool);
    }

    public async Task DeleteAsync(int id)
    {
        await _httpClient.DeleteAsync($"api/powertool/{id}");
    }
}


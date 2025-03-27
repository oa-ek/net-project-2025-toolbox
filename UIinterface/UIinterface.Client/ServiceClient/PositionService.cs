using System.Net.Http.Json;
using Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PositionService
{
    private readonly HttpClient _httpClient;

    public PositionService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<PositionDto>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<PositionDto>>("api/position");
    }

    public async Task<PositionDto> GetByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<PositionDto>($"api/position/{id}");
    }

    public async Task AddAsync(PositionDto position)
    {
        await _httpClient.PostAsJsonAsync("api/position", position);
    }

    public async Task UpdateAsync(int id, PositionDto position)
    {
        await _httpClient.PutAsJsonAsync($"api/position/{id}", position);
    }

    public async Task DeleteAsync(int id)
    {
        await _httpClient.DeleteAsync($"api/position/{id}");
    }
}


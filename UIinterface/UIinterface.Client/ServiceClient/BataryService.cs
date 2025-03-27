using System.Net.Http.Json;
using Core.DTOs;


public class BataryService
{
    private readonly HttpClient _httpClient;

    public BataryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<BataryDto>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<BataryDto>>("api/batary");
    }

    public async Task<BataryDto> GetByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<BataryDto>($"api/batary/{id}");
    }

    public async Task AddAsync(BataryDto batary)
    {
        await _httpClient.PostAsJsonAsync("api/batary", batary);
    }

    public async Task UpdateAsync(int id, BataryDto batary)
    {
        await _httpClient.PutAsJsonAsync($"api/batary/{id}", batary);
    }

    public async Task DeleteAsync(int id)
    {
        await _httpClient.DeleteAsync($"api/batary/{id}");
    }
}



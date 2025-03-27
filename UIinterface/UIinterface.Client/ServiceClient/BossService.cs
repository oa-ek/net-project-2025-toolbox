using System.Net.Http.Json;
using Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

public class BossService
{
    private readonly HttpClient _httpClient;

    public BossService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<BossDto>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<BossDto>>("api/boss");
    }

    public async Task<BossDto> GetByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<BossDto>($"api/boss/{id}");
    }

    public async Task AddAsync(BossDto boss)
    {
        await _httpClient.PostAsJsonAsync("api/boss", boss);
    }

    public async Task UpdateAsync(int id, BossDto boss)
    {
        await _httpClient.PutAsJsonAsync($"api/boss/{id}", boss);
    }

    public async Task DeleteAsync(int id)
    {
        await _httpClient.DeleteAsync($"api/boss/{id}");
    }
}


using System.Net.Http.Json;
using Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

public class BataryModelService
{
    private readonly HttpClient _httpClient;

    public BataryModelService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<BataryModelDto>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<BataryModelDto>>("api/batarymodel");
    }

    public async Task<BataryModelDto> GetByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<BataryModelDto>($"api/batarymodel/{id}");
    }

    public async Task AddAsync(BataryModelDto bataryModel)
    {
        await _httpClient.PostAsJsonAsync("api/batarymodel", bataryModel);
    }

    public async Task UpdateAsync(int id, BataryModelDto bataryModel)
    {
        await _httpClient.PutAsJsonAsync($"api/batarymodel/{id}", bataryModel);
    }

    public async Task DeleteAsync(int id)
    {
        await _httpClient.DeleteAsync($"api/batarymodel/{id}");
    }
}


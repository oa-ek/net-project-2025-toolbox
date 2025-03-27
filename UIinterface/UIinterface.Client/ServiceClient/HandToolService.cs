using System.Net.Http.Json;
using Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

public class HandToolService
{
    private readonly HttpClient _httpClient;

    public HandToolService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<HandToolDto>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<HandToolDto>>("api/handtool");
    }

    public async Task<HandToolDto> GetByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<HandToolDto>($"api/handtool/{id}");
    }

    public async Task AddAsync(HandToolDto handTool)
    {
        await _httpClient.PostAsJsonAsync("api/handtool", handTool);
    }

    public async Task UpdateAsync(int id, HandToolDto handTool)
    {
        await _httpClient.PutAsJsonAsync($"api/handtool/{id}", handTool);
    }

    public async Task DeleteAsync(int id)
    {
        await _httpClient.DeleteAsync($"api/handtool/{id}");
    }
}


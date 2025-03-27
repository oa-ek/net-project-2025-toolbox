using System.Net.Http.Json;
using Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ToolTypeService
{
    private readonly HttpClient _httpClient;

    public ToolTypeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ToolTypeDto>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<ToolTypeDto>>("api/tooltype");
    }

    public async Task<ToolTypeDto> GetByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<ToolTypeDto>($"api/tooltype/{id}");
    }

    public async Task AddAsync(ToolTypeDto toolType)
    {
        await _httpClient.PostAsJsonAsync("api/tooltype", toolType);
    }

    public async Task UpdateAsync(int id, ToolTypeDto toolType)
    {
        await _httpClient.PutAsJsonAsync($"api/tooltype/{id}", toolType);
    }

    public async Task DeleteAsync(int id)
    {
        await _httpClient.DeleteAsync($"api/tooltype/{id}");
    }
}


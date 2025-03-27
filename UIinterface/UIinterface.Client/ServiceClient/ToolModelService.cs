using System.Net.Http.Json;
using Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ToolModelService
{
    private readonly HttpClient _httpClient;

    public ToolModelService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ToolModelDto>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<ToolModelDto>>("api/toolmodel");
    }

    public async Task<ToolModelDto> GetByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<ToolModelDto>($"api/toolmodel/{id}");
    }

    public async Task AddAsync(ToolModelDto toolModel)
    {
        await _httpClient.PostAsJsonAsync("api/toolmodel", toolModel);
    }

    public async Task UpdateAsync(int id, ToolModelDto toolModel)
    {
        await _httpClient.PutAsJsonAsync($"api/toolmodel/{id}", toolModel);
    }

    public async Task DeleteAsync(int id)
    {
        await _httpClient.DeleteAsync($"api/toolmodel/{id}");
    }
}


using System.Net.Http.Json;
using Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SystemAdminService
{
    private readonly HttpClient _httpClient;

    public SystemAdminService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<SystemAdminDto>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<SystemAdminDto>>("api/systemadmin");
    }

    public async Task<SystemAdminDto> GetByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<SystemAdminDto>($"api/systemadmin/{id}");
    }

    public async Task AddAsync(SystemAdminDto systemAdmin)
    {
        await _httpClient.PostAsJsonAsync("api/systemadmin", systemAdmin);
    }

    public async Task UpdateAsync(int id, SystemAdminDto systemAdmin)
    {
        await _httpClient.PutAsJsonAsync($"api/systemadmin/{id}", systemAdmin);
    }

    public async Task DeleteAsync(int id)
    {
        await _httpClient.DeleteAsync($"api/systemadmin/{id}");
    }
}


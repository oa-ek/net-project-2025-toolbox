using System.Net.Http.Json;
using Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

public class WorkerService
{
    private readonly HttpClient _httpClient;

    public WorkerService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<WorkerDto>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<WorkerDto>>("api/worker");
    }

    public async Task<WorkerDto> GetByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<WorkerDto>($"api/worker/{id}");
    }

    public async Task AddAsync(WorkerDto worker)
    {
        await _httpClient.PostAsJsonAsync("api/worker", worker);
    }

    public async Task UpdateAsync(int id, WorkerDto worker)
    {
        await _httpClient.PutAsJsonAsync($"api/worker/{id}", worker);
    }

    public async Task DeleteAsync(int id)
    {
        await _httpClient.DeleteAsync($"api/worker/{id}");
    }
}


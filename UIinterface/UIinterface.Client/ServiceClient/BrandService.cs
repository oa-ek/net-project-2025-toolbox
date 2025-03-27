using System.Net.Http.Json;
using Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

public class BrandService
{
    private readonly HttpClient _httpClient;

    public BrandService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<BrandDto>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<BrandDto>>("api/brand");
    }

    public async Task<BrandDto> GetByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<BrandDto>($"api/brand/{id}");
    }

    public async Task AddAsync(BrandDto brand)
    {
        await _httpClient.PostAsJsonAsync("api/brand", brand);
    }

    public async Task UpdateAsync(int id, BrandDto brand)
    {
        await _httpClient.PutAsJsonAsync($"api/brand/{id}", brand);
    }

    public async Task DeleteAsync(int id)
    {
        await _httpClient.DeleteAsync($"api/brand/{id}");
    }
}


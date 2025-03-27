using System.Net.Http.Json;
using Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

public class LocationService
{
    private readonly HttpClient _httpClient;

    public LocationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<LocationDto>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<LocationDto>>("api/location");
    }

    public async Task<LocationDto> GetByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<LocationDto>($"api/location/{id}");
    }

    public async Task AddAsync(LocationDto location)
    {
        await _httpClient.PostAsJsonAsync("api/location", location);
    }

    public async Task UpdateAsync(int id, LocationDto location)
    {
        await _httpClient.PutAsJsonAsync($"api/location/{id}", location);
    }

    public async Task DeleteAsync(int id)
    {
        await _httpClient.DeleteAsync($"api/location/{id}");
    }
}


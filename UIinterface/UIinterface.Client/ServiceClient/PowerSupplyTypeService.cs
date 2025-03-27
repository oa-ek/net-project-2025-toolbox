using System.Net.Http.Json;
using Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PowerSupplyTypeService
{
    private readonly HttpClient _httpClient;

    public PowerSupplyTypeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<PowerSupplyTypeDto>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<PowerSupplyTypeDto>>("api/powersupplytype");
    }

    public async Task<PowerSupplyTypeDto> GetByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<PowerSupplyTypeDto>($"api/powersupplytype/{id}");
    }

    public async Task AddAsync(PowerSupplyTypeDto powerSupplyType)
    {
        await _httpClient.PostAsJsonAsync("api/powersupplytype", powerSupplyType);
    }

    public async Task UpdateAsync(int id, PowerSupplyTypeDto powerSupplyType)
    {
        await _httpClient.PutAsJsonAsync($"api/powersupplytype/{id}", powerSupplyType);
    }

    public async Task DeleteAsync(int id)
    {
        await _httpClient.DeleteAsync($"api/powersupplytype/{id}");
    }
}


using Newtonsoft.Json;
using app_ventas1.Models;

namespace app_ventas1.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("http://192.168.1.87:5297/");
    }

    public async Task<List<Producto>> GetProductosAsync()
    {
        var response = await _httpClient.GetAsync("api/productos");

        if (!response.IsSuccessStatusCode)
            return new List<Producto>();

        var json = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<List<Producto>>(json);
    }

    public async Task<bool> RegistrarVentaAsync(VentaRequest venta)
    {
        var json = JsonConvert.SerializeObject(venta);

        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("api/ventas", content);

        return response.IsSuccessStatusCode;
    }
}
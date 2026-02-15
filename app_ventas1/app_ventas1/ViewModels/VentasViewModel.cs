using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using app_ventas1.Models;
using System.Collections.ObjectModel;
using app_ventas1.Services;

namespace app_ventas1.ViewModels
{
    public partial class VentasViewModel : ObservableObject
    {
        private readonly ApiService _apiService = new();
        [ObservableProperty]
        private ObservableCollection<Producto> listaProductos;

        [ObservableProperty]
        private decimal totalPagar;

        public VentasViewModel()
        {
            ListaProductos = new ObservableCollection<Producto>();
            _ = CargarProductos();
        }

        private async Task CargarProductos()
        {
            try
            {
                var productos = await _apiService.GetProductosAsync();

                ListaProductos.Clear();

                foreach (var p in productos)
                {
                    p.PropertyChanged += (_, __) => RecalcularTotal();
                    ListaProductos.Add(p);
                }

                RecalcularTotal();
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        [RelayCommand]
        private void Incrementar(Producto producto)
        {
            if (producto.CantidadSolicitada < producto.Stock)
                producto.CantidadSolicitada++;
        }

        [RelayCommand]
        private void Decrementar(Producto producto)
        {
            if (producto.CantidadSolicitada > 0)
                producto.CantidadSolicitada--;
        }

        private void RecalcularTotal()
        {
            decimal suma = 0;

            foreach (var prod in ListaProductos)
            {
                if (prod.CantidadSolicitada > prod.Stock)
                    prod.CantidadSolicitada = prod.Stock;

                if (prod.CantidadSolicitada < 0)
                    prod.CantidadSolicitada = 0;

                suma += prod.Precio * prod.CantidadSolicitada;
            }

            TotalPagar = suma;
        }

        [RelayCommand]
        public void CalcularTotal()
        {
            decimal suma = 0;
            foreach (var prod in ListaProductos)
            {
                if (prod.CantidadSolicitada > prod.Stock)
                    prod.CantidadSolicitada = prod.Stock;

                suma += (prod.Precio * prod.CantidadSolicitada);
            }
            TotalPagar = suma;
        }

        [RelayCommand]
        public async Task Pagar()
        {
            var venta = new VentaRequest
            {
                MetodoPago = "Efectivo",
                Estado = "PAGADA"
            };

            foreach (var prod in ListaProductos)
            {
                if (prod.CantidadSolicitada > 0)
                {
                    venta.VentaDetalles.Add(new VentaDetalleRequest
                    {
                        ProductoId = prod.Id,
                        Cantidad = prod.CantidadSolicitada
                    });
                }
            }

            if (!venta.VentaDetalles.Any())
            {
                await App.Current.MainPage.DisplayAlert("Error", "Selecciona productos primero", "OK");
                return;
            }

            var resultado = await _apiService.RegistrarVentaAsync(venta);

            if (resultado)
            {
                await App.Current.MainPage.DisplayAlert("Éxito", "Venta registrada correctamente", "OK");

                foreach (var prod in ListaProductos)
                    prod.CantidadSolicitada = 0;

                TotalPagar = 0;

                ListaProductos.Clear();
                CargarProductos();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se pudo registrar la venta", "OK");
            }
        }
    }
}
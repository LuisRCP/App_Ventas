using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input; // Esto arregla el RelayCommand
using app_ventas1.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace app_ventas1.ViewModels
{
    // OJO: Debe decir "partial class" para que funcione el Toolkit
    public partial class VentasViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Producto> listaProductos;

        [ObservableProperty]
        private decimal totalPagar;

        public VentasViewModel()
        {
            ListaProductos = new ObservableCollection<Producto>();
            CargarDatosDePrueba();
        }

        private void CargarDatosDePrueba()
        {
            // Datos simulados
            ListaProductos.Add(new Producto { Id = 1, Nombre = "iPhone 15", Marca = "Apple", Precio = 20000, Stock = 5, Descripcion = "256GB" });
            ListaProductos.Add(new Producto { Id = 2, Nombre = "Galaxy S24", Marca = "Samsung", Precio = 18000, Stock = 8, Descripcion = "Ultra HD" });
            ListaProductos.Add(new Producto { Id = 3, Nombre = "Laptop Asus", Marca = "Asus", Precio = 15000, Stock = 3, Descripcion = "i7 16GB RAM" });
            ListaProductos.Add(new Producto { Id = 3, Nombre = "Laptop Victus", Marca = "HP", Precio = 12000, Stock = 10, Descripcion = "i9 32GB RAM" });
            ListaProductos.Add(new Producto { Id = 3, Nombre = "Google Pixel", Marca = "Google", Precio = 23000, Stock = 2, Descripcion = "120GB 12 RAM" });
            ListaProductos.Add(new Producto { Id = 3, Nombre = "Pc Gaming", Marca = "Xtrem", Precio = 22000, Stock = 4, Descripcion = "i9-13 1TB 64 RAM" });
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
            if (TotalPagar <= 0)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Selecciona productos primero", "OK");
                return;
            }

            // Crear el objeto de venta
            var nuevaVenta = new VentaRequest
            {
                Total = TotalPagar,
                MetodoPago = "Efectivo",
                Detalles = new List<VentaDetalle>()
            };

            foreach (var prod in ListaProductos)
            {
                if (prod.CantidadSolicitada > 0)
                {
                    nuevaVenta.Detalles.Add(new VentaDetalle
                    {
                        ProductoId = prod.Id,
                        Cantidad = prod.CantidadSolicitada,
                        PrecioUnitario = prod.Precio
                    });
                }
            }

            // Simular envío
            string json = JsonConvert.SerializeObject(nuevaVenta);
            await App.Current.MainPage.DisplayAlert("Éxito", $"JSON Generado:\n{json}", "OK");
        }
    }
}
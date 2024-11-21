using TP6MVC.Models;

namespace TP6MVC.ViewModels
{
    public class UpdatePresupuestoViewModel
    {
        private Presupuesto presupuesto;
        public Presupuesto Presupuesto { get => presupuesto; set => presupuesto = value; }
        private List<Producto> productos;
        public List<Producto> Productos { get => productos; set => productos = value; }

        public ProductoSeleccionado ProductoSeleccionado { get; set; }
    }

    public class ProductoSeleccionado
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
    }
}
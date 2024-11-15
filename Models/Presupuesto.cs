
namespace TP6MVC.Models
{
    public class Presupuesto
    {
        private int idPresupuesto;
        private string nombreDestinario;
        private List<PresupuestoDetalle> detalle = new List<PresupuestoDetalle>(); // Inicialización de la lista
        private const double IVA = 0.21;

        public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
        public string NombreDestinario { get => nombreDestinario; set => nombreDestinario = value; }
        public List<PresupuestoDetalle> Detalle { get => detalle; set => detalle = value; }

        public Presupuesto(int idPresupuesto, string nombreDestinario)
        {
            this.IdPresupuesto = idPresupuesto;
            this.NombreDestinario = nombreDestinario;
        }

        public Presupuesto() {}

        // Método para obtener el monto sin IVA
        public decimal CalcularMontoSinIva()
        {
            decimal monto = 0;

            foreach (var presupuesto in Detalle)
            {
                monto += presupuesto.Producto.Precio * presupuesto.Cantidad;
            }

            return monto;
        }

        // Método para obtener el monto con IVA
        public decimal CalcularMontoConIva()
        {
            decimal montoConIva = 0;

            foreach (var presupuestoDetalle in Detalle)
            {
                decimal precio = presupuestoDetalle.Producto.Precio * presupuestoDetalle.Cantidad;
                montoConIva += precio * (1 + (decimal)IVA);
            }

            return montoConIva;
        }

        // Método para obtener la cantidad total de productos
        public int CalcularCantidadProductos()
        {
            int cantidad = 0;
            foreach (var presupuesto in Detalle)
            {
                cantidad += presupuesto.Cantidad;
            }

            return cantidad;
        }
    }
}
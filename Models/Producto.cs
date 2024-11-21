using System.ComponentModel.DataAnnotations;

namespace TP6MVC.Models
{
    public class Producto
    {
        private int idProducto;
        private string descripcion;
        private int precio;

        public int IdProducto { get => idProducto; set => idProducto = value; }

        [Required(ErrorMessage = "La descripcion es obligatoria")]
        [StringLength(250, ErrorMessage = "La descripcion no puede tener más de 250 caracteres.")]
        public string Descripcion { get => descripcion; set => descripcion = value; }
        
        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El precio debe ser un valor positivo")]
        public int Precio { get => precio; set => precio = value; }

        public Producto(int idProducto, string descripcion, int precio)
        {
            this.IdProducto = idProducto;
            this.Descripcion = descripcion;
            this.Precio = precio;
        }

        public Producto()
        {

        }
    }
}
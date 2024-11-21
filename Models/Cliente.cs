using System.ComponentModel.DataAnnotations;

namespace TP6MVC.Models
{
    public class Cliente
    {
        private int idCliente;
        private string nombre;
        private string email;
        private string telefono;

        public int IdCliente { get => idCliente; set => idCliente = value; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get => nombre; set => nombre = value; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido.")]
        public string Email { get => email; set => email = value; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [Phone(ErrorMessage = "El teléfono no tiene un formato válido.")]
        public string Telefono { get => telefono; set => telefono = value; }


        public Cliente(int idCliente, string nombre, string email, string telefono)
        {
            this.IdCliente = idCliente;
            this.Nombre = nombre;
            this.Email = email;
            this.Telefono = telefono;
        }

        public Cliente()
        {
        }



    }
}
using TP6MVC.Models;

namespace TP6MVC.ViewModels
{
    public class CreatePresupuestoViewModel
    {
        int idCliente;
        public int IdCliente { get => idCliente; set => idCliente = value; }
        private List<Cliente> clientes;
        public List<Cliente> Clientes { get => clientes; set => clientes = value; }
    }
}
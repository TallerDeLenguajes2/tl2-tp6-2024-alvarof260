using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using TP6MVC.Models;

namespace TP6MVC.Repositories
{
    public interface IClienteRepository
    {
        public List<Cliente> GetAll();
        public Cliente GetById(int id);
        public void Create(Cliente cliente);
/*         public void Update(); */
        public void Delete(int id);
    }

    public class ClienteRepository : IClienteRepository
    {

        private readonly string _stringConnection;

        public ClienteRepository(string stringConnection)
        {
            _stringConnection = stringConnection;
        }

        public List<Cliente> GetAll()
        {
            List<Cliente> Clientes = new List<Cliente>();
            string query = @"SELECT * FROM Clientes";
            using (SqliteConnection connection = new SqliteConnection(_stringConnection))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idCliente = reader.GetInt32(0);
                        string nombre = reader.GetString(1);
                        string email = reader.GetString(2);
                        string telefono = reader.GetString(3);

                        Cliente nuevoCliente = new Cliente(idCliente, nombre, email, telefono);
                        Clientes.Add(nuevoCliente);
                    }
                }
                connection.Close();
            }

            return Clientes;
        }

        public Cliente GetById(int id)
        {
            Cliente cliente = new Cliente();
            string query = @"SELECT * FROM Clientes WHERE idCliente = @id";
            using (SqliteConnection connection = new SqliteConnection(_stringConnection))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                command.Parameters.Add(new SqliteParameter("@id", id));
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        cliente.IdCliente = reader.GetInt32(0);
                        cliente.Nombre = reader.GetString(1);
                        cliente.Email = reader.GetString(2);
                        cliente.Telefono = reader.GetString(3);
                    }
                }
                connection.Close();
            }
            return cliente;
        }

        public void Create(Cliente cliente)
        {
            string query = @"INSERT INTO Clientes (Nombre, Email, Telefono) VALUES (@nombre, @email, @telefono)";
            using (SqliteConnection connection = new SqliteConnection(_stringConnection))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(query, connection);
                command.Parameters.Add(new SqliteParameter("@nombre", cliente.Nombre));
                command.Parameters.Add(new SqliteParameter("@email", cliente.Email));
                command.Parameters.Add(new SqliteParameter("@telefono", cliente.Telefono));

                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void Delete(int id)
        {
            string query = @"DELETE FROM Clientes WHERE idCliente = @id;";
            using (SqliteConnection connection = new SqliteConnection(_stringConnection))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                command.Parameters.Add(new SqliteParameter("@id", id));
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

    }
}
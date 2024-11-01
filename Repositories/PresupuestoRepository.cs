using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

public interface IPresupuestoRepository
{
    List<Presupuesto> GetAll();
    Presupuesto GetById(int id);
    void Create(Presupuesto presupuesto);
    void Update(int id, Producto producto, int cantidad);
    void Delete(int id);
}

public class PresupuestoRepository : IPresupuestoRepository
{
    private readonly string _stringConnection;

    public PresupuestoRepository(string stringConnection)
    {
        _stringConnection = stringConnection;
    }

    public List<Presupuesto> GetAll()
    {
        List<Presupuesto> presupuestos = new List<Presupuesto>();
        string query = @"SELECT idPresupuesto, NombreDestinatario FROM Presupuestos;";
        using (SqliteConnection connection = new SqliteConnection(_stringConnection))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int idPresupuesto = reader.GetInt32(0);
                    string nombreDestinatario = reader.GetString(1);

                    Presupuesto presupuesto = new Presupuesto(idPresupuesto, nombreDestinatario);
                    presupuesto.Detalle = GetPresupuestoDetalles(idPresupuesto);
                    presupuestos.Add(presupuesto);
                }
            }
            connection.Close();
        }
        return presupuestos;
    }

    public Presupuesto GetById(int id)
    {
        Presupuesto presupuesto = null;
        string query = @"SELECT idPresupuesto, NombreDestinatario 
                         FROM Presupuestos 
                         WHERE idPresupuesto = @id";

        using (SqliteConnection connection = new SqliteConnection(_stringConnection))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@id", id));

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    int idPresupuesto = reader.GetInt32(0);
                    string nombreDestinatario = reader.GetString(1);
                    presupuesto = new Presupuesto(idPresupuesto, nombreDestinatario);
                    presupuesto.Detalle = GetPresupuestoDetalles(idPresupuesto);

                }
            }
            connection.Close();
        }
        return presupuesto;
    }

    public void Create(Presupuesto presupuesto)
    {
        string query = @"INSERT INTO Presupuestos (NombreDestinatario, FechaCreacion) 
                     VALUES (@NombreDestinatario, @FechaCreacion);";

        using (SqliteConnection connection = new SqliteConnection(_stringConnection))
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@NombreDestinatario", presupuesto.NombreDestinario));
            command.Parameters.Add(new SqliteParameter("@FechaCreacion", "2024-10-27"));

            command.ExecuteNonQuery();

            connection.Close();
        }
    }

    public void Update(int id, Producto producto, int cantidad)
    {
        string query = @"INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto, Cantidad)
                         VALUES (@idPresupuesto, @idProducto, @Cantidad);";

        using (SqliteConnection connection = new SqliteConnection(_stringConnection))
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@idPresupuesto", id));
            command.Parameters.Add(new SqliteParameter("@idProducto", producto.IdProducto));
            command.Parameters.Add(new SqliteParameter("@Cantidad", cantidad));
            command.ExecuteNonQuery();

            connection.Close();
        }
    }

    public void Delete(int id)
    {
        string query = @"DELETE FROM Presupuestos WHERE idPresupuesto = @id;";
        using (SqliteConnection connection = new SqliteConnection(_stringConnection))
        {
            connection.Open();

            SqliteCommand commandDetalles = new SqliteCommand(@"DELETE FROM PresupuestoDetalles 
                                                                WHERE idPresupuesto = @idPresupuesto;", connection);
            commandDetalles.Parameters.Add(new SqliteParameter("@idPresupuesto", id));
            commandDetalles.ExecuteNonQuery();

            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@id", id));
            command.ExecuteNonQuery();

            connection.Close();
        }
    }
    private List<PresupuestoDetalle> GetPresupuestoDetalles(int id)
    {
        List<PresupuestoDetalle> presupuestosDetalles = new List<PresupuestoDetalle>();
        string query = @"SELECT p.idProducto, p.Descripcion, p.Precio, d.Cantidad
                     FROM PresupuestosDetalle d
                     JOIN Productos p ON d.idProducto = p.idProducto
                     WHERE d.idPresupuesto = @idPresupuesto;";
        using (SqliteConnection connection = new SqliteConnection(_stringConnection))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@idPresupuesto", id));

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Producto producto = new Producto
                    {
                        IdProducto = reader.GetInt32(0),
                        Descripcion = reader.GetString(1),
                        Precio = reader.GetInt32(2)
                    };

                    int cantidad = reader.GetInt32(3);
                    presupuestosDetalles.Add(new PresupuestoDetalle(producto, cantidad));
                }
            }
            connection.Close();
        }
        return presupuestosDetalles;
    }

}
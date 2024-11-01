using Microsoft.Data.Sqlite;

public interface IProductoRepository
{
    List<Producto> GetAll();
    Producto GetById(int id);
    void Create(Producto producto);
    void Update(int id, Producto producto);
    void Delete(int id);
}

public class ProductoRepository : IProductoRepository
{
    private readonly string _stringConnection;

    public ProductoRepository(string stringConnection)
    {
        _stringConnection = stringConnection;
    }

    public List<Producto> GetAll()
    {
        List<Producto> productos = new List<Producto>();
        string query = @"SELECT * FROM Productos;";

        using (SqliteConnection connection = new SqliteConnection(_stringConnection))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    productos.Add(new Producto
                    {
                        IdProducto = reader.GetInt32(0),
                        Descripcion = reader.GetString(1),
                        Precio = reader.GetInt32(2)
                    });
                }
            }
            connection.Close();
        }
        return productos;
    }

    public Producto GetById(int id)
    {
        Producto producto = new Producto();
        string query = @"SELECT * FROM Productos WHERE idProducto = @id;";

        using (SqliteConnection connection = new SqliteConnection(_stringConnection))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@id", id));
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    producto.IdProducto = reader.GetInt32(0);
                    producto.Descripcion = reader.GetString(1);
                    producto.Precio = reader.GetInt32(2);
                }
            }
            connection.Close();
        }
        return producto;
    }

    public void Create(Producto producto)
    {
        string query = @"INSERT INTO Productos (Descripcion, Precio) VALUES (@Descripcion, @Precio);";

        using (SqliteConnection connection = new SqliteConnection(_stringConnection))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);

            command.Parameters.Add(new SqliteParameter("@Descripcion", producto.Descripcion));
            command.Parameters.Add(new SqliteParameter("@Precio", producto.Precio));
            command.ExecuteNonQuery();

            connection.Close();
        }
    }

    public void Update(int id, Producto producto)
    {
        string query = @"UPDATE Productos SET Descripcion = @Descripcion, Precio = @Precio WHERE idProducto = @id;";
        using (SqliteConnection connection = new SqliteConnection(_stringConnection))
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@Descripcion", producto.Descripcion));
            command.Parameters.Add(new SqliteParameter("@Precio", producto.Precio));
            command.Parameters.Add(new SqliteParameter("@id", id));
            command.ExecuteNonQuery();

            connection.Close();
        }
    }

    public void Delete(int id)
    {
        string query = @"DELETE FROM Productos WHERE idProducto = @id;";
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
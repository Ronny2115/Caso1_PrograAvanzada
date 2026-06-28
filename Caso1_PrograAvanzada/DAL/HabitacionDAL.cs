using Caso1_PrograAvanzada.Entities;
using Caso1_PrograAvanzada.Utilities;
using Microsoft.Data.SqlClient;

namespace Caso1_PrograAvanzada.DAL
{
    public class HabitacionDAL
    {
        private readonly Conexion _conexion;

        public HabitacionDAL(Conexion conexion)
        {
            _conexion = conexion;
        }

        //==========================
        // LISTAR
        //==========================

        public List<Habitacion> ListarHabitaciones()
        {
            List<Habitacion> lista = new();

            using (SqlConnection con = _conexion.ObtenerConexion())
            {
                SqlCommand cmd = new("SELECT * FROM HABITACIONES", con);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Habitacion
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        CodigoDeHabitacion = reader["CodigoDeHabitacion"].ToString()!,
                        NombreDeHabitacion = reader["NombreDeHabitacion"].ToString()!,
                        CantidadDeHuespedesPermitidos = Convert.ToInt32(reader["CantidadDeHuespedesPermitidos"]),
                        CantidadDeCamas = Convert.ToInt32(reader["CantidadDeCamas"]),
                        CantidadDeBanos = Convert.ToInt32(reader["CantidadDeBanos"]),
                        Ubicacion = reader["Ubicacion"].ToString()!,
                        EncargadoDeLimpieza = reader["EncargadoDeLimpieza"].ToString()!,
                        TipoDeHabitacion = Convert.ToInt32(reader["TipoDeHabitacion"]),
                        CostoDeLimpieza = Convert.ToDecimal(reader["CostoDeLimpieza"]),
                        CostoDeReserva = Convert.ToDecimal(reader["CostoDeReserva"]),
                        FechaDeRegistro = Convert.ToDateTime(reader["FechaDeRegistro"]),
                        FechaDeModificacion = reader["FechaDeModificacion"] == DBNull.Value
                            ? null
                            : Convert.ToDateTime(reader["FechaDeModificacion"]),
                        Estado = Convert.ToBoolean(reader["Estado"])
                    });
                }
            }

            return lista;
        }

        //==========================
        // OBTENER POR ID
        //==========================

        public Habitacion ObtenerHabitacion(int id)
        {
            Habitacion habitacion = new();

            using (SqlConnection con = _conexion.ObtenerConexion())
            {
                SqlCommand cmd = new("SELECT * FROM HABITACIONES WHERE Id=@Id", con);

                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    habitacion.Id = Convert.ToInt32(reader["Id"]);
                    habitacion.CodigoDeHabitacion = reader["CodigoDeHabitacion"].ToString()!;
                    habitacion.NombreDeHabitacion = reader["NombreDeHabitacion"].ToString()!;
                    habitacion.CantidadDeHuespedesPermitidos = Convert.ToInt32(reader["CantidadDeHuespedesPermitidos"]);
                    habitacion.CantidadDeCamas = Convert.ToInt32(reader["CantidadDeCamas"]);
                    habitacion.CantidadDeBanos = Convert.ToInt32(reader["CantidadDeBanos"]);
                    habitacion.Ubicacion = reader["Ubicacion"].ToString()!;
                    habitacion.EncargadoDeLimpieza = reader["EncargadoDeLimpieza"].ToString()!;
                    habitacion.TipoDeHabitacion = Convert.ToInt32(reader["TipoDeHabitacion"]);
                    habitacion.CostoDeLimpieza = Convert.ToDecimal(reader["CostoDeLimpieza"]);
                    habitacion.CostoDeReserva = Convert.ToDecimal(reader["CostoDeReserva"]);
                    habitacion.Estado = Convert.ToBoolean(reader["Estado"]);
                }
            }

            return habitacion;
        }

        //==========================
        // REGISTRAR
        //==========================

        public void RegistrarHabitacion(Habitacion habitacion)
        {
            using (SqlConnection con = _conexion.ObtenerConexion())
            {
                string sql = @"INSERT INTO HABITACIONES
                (
                    CodigoDeHabitacion,
                    NombreDeHabitacion,
                    CantidadDeHuespedesPermitidos,
                    CantidadDeCamas,
                    CantidadDeBanos,
                    Ubicacion,
                    EncargadoDeLimpieza,
                    TipoDeHabitacion,
                    CostoDeLimpieza,
                    CostoDeReserva,
                    FechaDeRegistro,
                    Estado
                )
                VALUES
                (
                    @Codigo,
                    @Nombre,
                    @Huespedes,
                    @Camas,
                    @Banos,
                    @Ubicacion,
                    @Encargado,
                    @Tipo,
                    @CostoLimpieza,
                    @CostoReserva,
                    GETDATE(),
                    @Estado
                )";

                SqlCommand cmd = new(sql, con);

                cmd.Parameters.AddWithValue("@Codigo", habitacion.CodigoDeHabitacion);
                cmd.Parameters.AddWithValue("@Nombre", habitacion.NombreDeHabitacion);
                cmd.Parameters.AddWithValue("@Huespedes", habitacion.CantidadDeHuespedesPermitidos);
                cmd.Parameters.AddWithValue("@Camas", habitacion.CantidadDeCamas);
                cmd.Parameters.AddWithValue("@Banos", habitacion.CantidadDeBanos);
                cmd.Parameters.AddWithValue("@Ubicacion", habitacion.Ubicacion);
                cmd.Parameters.AddWithValue("@Encargado", habitacion.EncargadoDeLimpieza);
                cmd.Parameters.AddWithValue("@Tipo", habitacion.TipoDeHabitacion);
                cmd.Parameters.AddWithValue("@CostoLimpieza", habitacion.CostoDeLimpieza);
                cmd.Parameters.AddWithValue("@CostoReserva", habitacion.CostoDeReserva);
                cmd.Parameters.AddWithValue("@Estado", habitacion.Estado);

                con.Open();

                cmd.ExecuteNonQuery();
            }
        }

        //==========================
        // EDITAR
        //==========================

        public void EditarHabitacion(Habitacion habitacion)
        {
            using (SqlConnection con = _conexion.ObtenerConexion())
            {
                string sql = @"UPDATE HABITACIONES SET

CantidadDeHuespedesPermitidos=@Huespedes,
CantidadDeCamas=@Camas,
EncargadoDeLimpieza=@Encargado,
TipoDeHabitacion=@Tipo,
CostoDeLimpieza=@CostoLimpieza,
CostoDeReserva=@CostoReserva,
FechaDeModificacion=GETDATE(),
Estado=@Estado

WHERE Id=@Id";

                SqlCommand cmd = new(sql, con);

                cmd.Parameters.AddWithValue("@Id", habitacion.Id);
                cmd.Parameters.AddWithValue("@Huespedes", habitacion.CantidadDeHuespedesPermitidos);
                cmd.Parameters.AddWithValue("@Camas", habitacion.CantidadDeCamas);
                cmd.Parameters.AddWithValue("@Encargado", habitacion.EncargadoDeLimpieza);
                cmd.Parameters.AddWithValue("@Tipo", habitacion.TipoDeHabitacion);
                cmd.Parameters.AddWithValue("@CostoLimpieza", habitacion.CostoDeLimpieza);
                cmd.Parameters.AddWithValue("@CostoReserva", habitacion.CostoDeReserva);
                cmd.Parameters.AddWithValue("@Estado", habitacion.Estado);

                con.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}
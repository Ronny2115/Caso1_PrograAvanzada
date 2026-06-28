using Caso1_PrograAvanzada.Entities;
using Caso1_PrograAvanzada.Utilities;
using Microsoft.Data.SqlClient;

namespace Caso1_PrograAvanzada.DAL
{
    public class ReservacionDAL
    {
        private readonly Conexion _conexion;

        public ReservacionDAL(Conexion conexion)
        {
            _conexion = conexion;
        }

        //=========================================
        // LISTAR RESERVACIONES POR HABITACION
        //=========================================

        public List<Reservacion> ListarReservaciones(int idHabitacion)
        {
            List<Reservacion> lista = new();

            using (SqlConnection con = _conexion.ObtenerConexion())
            {
                string sql = @"SELECT *
                               FROM RESERVACIONES
                               WHERE IdHabitacion=@IdHabitacion
                               ORDER BY FechaInicioReserva";

                SqlCommand cmd = new(sql, con);
                cmd.Parameters.AddWithValue("@IdHabitacion", idHabitacion);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Reservacion
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        NombreDeLaPersona = reader["NombreDeLaPersona"].ToString()!,
                        Identificacion = reader["Identificacion"].ToString()!,
                        Telefono = reader["Telefono"].ToString()!,
                        Correo = reader["Correo"].ToString()!,
                        FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]),
                        Direccion = reader["Direccion"].ToString()!,
                        MontoTotal = Convert.ToDecimal(reader["MontoTotal"]),
                        FechaInicioReserva = Convert.ToDateTime(reader["FechaInicioReserva"]),
                        FechaFinReserva = Convert.ToDateTime(reader["FechaFinReserva"]),
                        FechaDeRegistro = Convert.ToDateTime(reader["FechaDeRegistro"]),
                        IdHabitacion = Convert.ToInt32(reader["IdHabitacion"])
                    });
                }
            }

            return lista;
        }

        //=========================================
        // REGISTRAR RESERVACION
        //=========================================

        public int RegistrarReservacion(Reservacion reservacion)
        {
            using (SqlConnection con = _conexion.ObtenerConexion())
            {
                string sql = @"
        INSERT INTO RESERVACIONES
        (
            NombreDeLaPersona,
            Identificacion,
            Telefono,
            Correo,
            FechaNacimiento,
            Direccion,
            MontoTotal,
            FechaInicioReserva,
            FechaFinReserva,
            FechaDeRegistro,
            IdHabitacion
        )

        VALUES
        (
            @Nombre,
            @Identificacion,
            @Telefono,
            @Correo,
            @FechaNacimiento,
            @Direccion,
            @MontoTotal,
            @FechaInicio,
            @FechaFin,
            GETDATE(),
            @IdHabitacion
        );

        SELECT CAST(SCOPE_IDENTITY() AS INT);";

                SqlCommand cmd = new(sql, con);

                cmd.Parameters.AddWithValue("@Nombre", reservacion.NombreDeLaPersona);
                cmd.Parameters.AddWithValue("@Identificacion", reservacion.Identificacion);
                cmd.Parameters.AddWithValue("@Telefono", reservacion.Telefono);
                cmd.Parameters.AddWithValue("@Correo", reservacion.Correo);
                cmd.Parameters.AddWithValue("@FechaNacimiento", reservacion.FechaNacimiento);
                cmd.Parameters.AddWithValue("@Direccion", reservacion.Direccion);
                cmd.Parameters.AddWithValue("@MontoTotal", reservacion.MontoTotal);
                cmd.Parameters.AddWithValue("@FechaInicio", reservacion.FechaInicioReserva);
                cmd.Parameters.AddWithValue("@FechaFin", reservacion.FechaFinReserva);
                cmd.Parameters.AddWithValue("@IdHabitacion", reservacion.IdHabitacion);

                con.Open();

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        //=========================================
        // VALIDAR DISPONIBILIDAD
        //=========================================

        public bool HabitacionDisponible(int idHabitacion,
                                         DateTime fechaInicio,
                                         DateTime fechaFin)
        {
            using (SqlConnection con = _conexion.ObtenerConexion())
            {
                string sql = @"
                SELECT COUNT(*)
                FROM RESERVACIONES
                WHERE IdHabitacion=@IdHabitacion
                AND
                (
                    @FechaInicio <= FechaFinReserva
                    AND
                    @FechaFin >= FechaInicioReserva
                )";

                SqlCommand cmd = new(sql, con);

                cmd.Parameters.AddWithValue("@IdHabitacion", idHabitacion);
                cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", fechaFin);

                con.Open();

                int cantidad = Convert.ToInt32(cmd.ExecuteScalar());

                return cantidad == 0;
            }
        }

        //=========================================
        // OBTENER RESERVACION POR ID
        //=========================================

        public Reservacion? ObtenerReservacion(int id)
        {
            using (SqlConnection con = _conexion.ObtenerConexion())
            {
                string sql = "SELECT * FROM RESERVACIONES WHERE Id=@Id";

                SqlCommand cmd = new(sql, con);

                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Reservacion
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        NombreDeLaPersona = reader["NombreDeLaPersona"].ToString()!,
                        Identificacion = reader["Identificacion"].ToString()!,
                        Telefono = reader["Telefono"].ToString()!,
                        Correo = reader["Correo"].ToString()!,
                        FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]),
                        Direccion = reader["Direccion"].ToString()!,
                        MontoTotal = Convert.ToDecimal(reader["MontoTotal"]),
                        FechaInicioReserva = Convert.ToDateTime(reader["FechaInicioReserva"]),
                        FechaFinReserva = Convert.ToDateTime(reader["FechaFinReserva"]),
                        FechaDeRegistro = Convert.ToDateTime(reader["FechaDeRegistro"]),
                        IdHabitacion = Convert.ToInt32(reader["IdHabitacion"])
                    };
                }
            }

            return null;
        }
    }
}
using Caso1_PrograAvanzada.DAL;
using Caso1_PrograAvanzada.Entities;

namespace Caso1_PrograAvanzada.BLL
{
    public class ReservacionBLL
    {
        private readonly ReservacionDAL _reservacionDAL;
        private readonly HabitacionDAL _habitacionDAL;

        public ReservacionBLL(
            ReservacionDAL reservacionDAL,
            HabitacionDAL habitacionDAL)
        {
            _reservacionDAL = reservacionDAL;
            _habitacionDAL = habitacionDAL;
        }

        //=====================================
        // LISTAR RESERVACIONES
        //=====================================

        public List<Reservacion> ListarReservaciones(int idHabitacion)
        {
            return _reservacionDAL.ListarReservaciones(idHabitacion);
        }

        //=====================================
        // CALCULAR MONTO
        //=====================================

        public decimal CalcularMonto(Reservacion reservacion)
        {
            Habitacion habitacion =
                _habitacionDAL.ObtenerHabitacion(reservacion.IdHabitacion);

            int dias = (reservacion.FechaFinReserva -
                        reservacion.FechaInicioReserva).Days;

            if (dias <= 0)
                throw new Exception("La fecha final debe ser mayor que la fecha inicial.");

            return (dias * habitacion.CostoDeReserva)
                    + habitacion.CostoDeLimpieza;
        }

        //=====================================
        // REGISTRAR RESERVACION
        //=====================================

        public int RegistrarReservacion(Reservacion reservacion)
        {
            if (string.IsNullOrWhiteSpace(reservacion.NombreDeLaPersona))
                throw new Exception("Debe ingresar el nombre de la persona.");

            if (string.IsNullOrWhiteSpace(reservacion.Identificacion))
                throw new Exception("Debe ingresar la identificación.");

            if (string.IsNullOrWhiteSpace(reservacion.Telefono))
                throw new Exception("Debe ingresar el teléfono.");

            if (string.IsNullOrWhiteSpace(reservacion.Correo))
                throw new Exception("Debe ingresar el correo.");

            if (reservacion.FechaInicioReserva.Date < DateTime.Today)
                throw new Exception("La fecha de inicio no puede ser anterior al día de hoy.");

            if (reservacion.FechaFinReserva <= reservacion.FechaInicioReserva)
                throw new Exception("La fecha final debe ser mayor que la fecha inicial.");

            bool disponible = _reservacionDAL.HabitacionDisponible(
                reservacion.IdHabitacion,
                reservacion.FechaInicioReserva,
                reservacion.FechaFinReserva);

            if (!disponible)
                throw new Exception("La habitación ya se encuentra reservada para ese rango de fechas.");

            reservacion.MontoTotal = CalcularMonto(reservacion);

            return _reservacionDAL.RegistrarReservacion(reservacion);
        }

        //=====================================
        // BUSCAR RESERVACION
        //=====================================

        public Reservacion? ObtenerReservacion(int id)
        {
            return _reservacionDAL.ObtenerReservacion(id);
        }
    }
}
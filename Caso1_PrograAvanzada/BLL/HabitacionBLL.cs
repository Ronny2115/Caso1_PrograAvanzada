using Caso1_PrograAvanzada.DAL;
using Caso1_PrograAvanzada.Entities;

namespace Caso1_PrograAvanzada.BLL
{
    public class HabitacionBLL
    {
        private readonly HabitacionDAL _habitacionDAL;

        public HabitacionBLL(HabitacionDAL habitacionDAL)
        {
            _habitacionDAL = habitacionDAL;
        }

        //==========================
        // LISTAR
        //==========================

        public List<Habitacion> ListarHabitaciones()
        {
            return _habitacionDAL.ListarHabitaciones();
        }

        //==========================
        // OBTENER
        //==========================

        public Habitacion ObtenerHabitacion(int id)
        {
            return _habitacionDAL.ObtenerHabitacion(id);
        }

        //==========================
        // REGISTRAR
        //==========================

        public void RegistrarHabitacion(Habitacion habitacion)
        {
            if (habitacion.CantidadDeHuespedesPermitidos <= 0)
                throw new Exception("La cantidad de huéspedes debe ser mayor que cero.");

            if (habitacion.CostoDeLimpieza <= 0)
                throw new Exception("El costo de limpieza debe ser mayor que cero.");

            if (habitacion.CostoDeReserva <= 0)
                throw new Exception("El costo de reserva debe ser mayor que cero.");

            _habitacionDAL.RegistrarHabitacion(habitacion);
        }

        //==========================
        // EDITAR
        //==========================

        public void EditarHabitacion(Habitacion habitacion)
        {
            if (habitacion.CantidadDeHuespedesPermitidos <= 0)
                throw new Exception("La cantidad de huéspedes debe ser mayor que cero.");

            if (habitacion.CostoDeLimpieza <= 0)
                throw new Exception("El costo de limpieza debe ser mayor que cero.");

            if (habitacion.CostoDeReserva <= 0)
                throw new Exception("El costo de reserva debe ser mayor que cero.");

            _habitacionDAL.EditarHabitacion(habitacion);
        }
    }
}
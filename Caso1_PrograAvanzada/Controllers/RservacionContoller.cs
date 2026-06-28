using Caso1_PrograAvanzada.BLL;
using Caso1_PrograAvanzada.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Caso1_PrograAvanzada.Controllers
{
    public class ReservacionController : Controller
    {
        private readonly HabitacionBLL _habitacionBLL;
        private readonly ReservacionBLL _reservacionBLL;

        public ReservacionController(
            HabitacionBLL habitacionBLL,
            ReservacionBLL reservacionBLL)
        {
            _habitacionBLL = habitacionBLL;
            _reservacionBLL = reservacionBLL;
        }

        //==========================================
        // HABITACIONES DISPONIBLES
        //==========================================

        public IActionResult Index()
        {
            var habitaciones = _habitacionBLL
                .ListarHabitaciones()
                .Where(h => h.Estado)
                .ToList();

            return View(habitaciones);
        }

        //==========================================
        // GET CREATE
        //==========================================

        [HttpGet]
        public IActionResult Create(int idHabitacion)
        {
            Reservacion reservacion = new();

            reservacion.IdHabitacion = idHabitacion;

            return View(reservacion);
        }

        //==========================================
        // POST CREATE
        //==========================================

        [HttpPost]
        public IActionResult Create(Reservacion reservacion)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(reservacion);

                int idGenerado = _reservacionBLL.RegistrarReservacion(reservacion);

                TempData["Mensaje"] = "Reservación registrada correctamente.";

                return RedirectToAction(nameof(Detalle), new
                {
                    id = idGenerado
                });
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;

                return View(reservacion);
            }
        }

        //==========================================
        // BUSCAR
        //==========================================

        [HttpGet]
        public IActionResult Buscar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Buscar(int id)
        {
            return RedirectToAction(nameof(Detalle), new { id });
        }

        //==========================================
        // DETALLE
        //==========================================

        public IActionResult Detalle(int id)
        {
            Reservacion? reservacion =
                _reservacionBLL.ObtenerReservacion(id);

            if (reservacion == null)
            {
                TempData["Mensaje"] =
                    "No existe la reservación.";

                return RedirectToAction(nameof(Buscar));
            }

            return View(reservacion);
        }
    }
}
using Caso1_PrograAvanzada.BLL;
using Caso1_PrograAvanzada.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Caso1_PrograAvanzada.Controllers
{
    public class HabitacionController : Controller
    {
        private readonly HabitacionBLL _habitacionBLL;
        private readonly ReservacionBLL _reservacionBLL;

        public HabitacionController(
            HabitacionBLL habitacionBLL,
            ReservacionBLL reservacionBLL)
        {
            _habitacionBLL = habitacionBLL;
            _reservacionBLL = reservacionBLL;
        }

        //==========================
        // LISTAR HABITACIONES
        //==========================

        public IActionResult Index()
        {
            var lista = _habitacionBLL.ListarHabitaciones();

            return View(lista);
        }

        //==========================
        // REGISTRAR HABITACION
        //==========================

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Habitacion habitacion)
        {
            try
            {
                habitacion.Estado = true;

                _habitacionBLL.RegistrarHabitacion(habitacion);

                TempData["Mensaje"] = "Habitación registrada correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View(habitacion);
            }
        }

        //==========================
        // EDITAR HABITACION
        //==========================

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Habitacion habitacion = _habitacionBLL.ObtenerHabitacion(id);

            return View(habitacion);
        }

        [HttpPost]
        public IActionResult Edit(Habitacion habitacion)
        {
            try
            {
                _habitacionBLL.EditarHabitacion(habitacion);

                TempData["Mensaje"] = "Habitación actualizada correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View(habitacion);
            }
        }

        //==========================
        // VER RESERVACIONES
        //==========================

        [HttpGet]
        public IActionResult Reservas(int id)
        {
            var lista = _reservacionBLL.ListarReservaciones(id);

            ViewBag.IdHabitacion = id;

            return View(lista);
        }
    }
}
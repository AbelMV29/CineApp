using AppCine.Controllers;
using AppCine.Models;
using AppCine.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AppCine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CineBdContext>();

            // Crear una instancia del contexto de la base de datos
            using var context = new CineBdContext(optionsBuilder.Options);
            var clienteController = new ClienteController(context);
            var adminController = new AdminController(context);
            var ui = new GeneralVista(clienteController,adminController);

            if (EsDiaDeApertura())
            {
                ui.MenuPrincipal();
            }
            else
            {
                ui.CineCerrado();
            }
        }
        private static bool EsDiaDeApertura()
        {
            DayOfWeek today = DateTime.Today.DayOfWeek;
            return true;
            //return today >= DayOfWeek.Wednesday && today <= DayOfWeek.Saturday;
        }
    }

}
using AppCine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCine.Controllers
{
    public class ClienteController
    {

        private static Random random = new Random();

        private readonly CineBdContext _context;

        // Inyectar el contexto de la base de datos en el controlador
        public ClienteController(CineBdContext context)
        {
            _context = context;
        }
        // Método para obtener todas las funciones
        public List<Funcione> ObtenerTodasLasFunciones()
        {
            DateTime fechaActual = DateTime.Now;
            // Utiliza LINQ para filtrar las funciones que tienen una fecha mayor o igual a la actual.
            var funcionesFuturas = _context.Funciones
                                           .Where(f => f.FechaHora >= fechaActual)
                                           .OrderBy(f => f.FechaHora)
                                           .ToList();
            return funcionesFuturas;
        }
        public List<Asiento> ObtenerAsientosPorFuncionId(int funcionId)
        {
            // Filtra los asientos que tengan el FuncionId proporcionado.
            var asientos = _context.Asientos
                                   .Where(a => a.FuncionId == funcionId)
                                   .ToList();
            return asientos;
        }
        public bool ReservarAsiento(int asientoId, int funcionId)
        {
            var asiento = _context.Asientos.FirstOrDefault(a => a.AsientoId == asientoId && a.FuncionId == funcionId);
            if (asiento != null && asiento.Disponible)
            {
                asiento.Disponible = false;
                var boleto = new Boleto { FuncionId = funcionId, AsientoId = asientoId, BoletoId = GenerateRandomString(6) };
                _context.Boletos.Add(boleto);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public Boleto ObtenerBoletoPorId(string boletoId)
        {
            // Asumiendo que BoletoID es un string. Si es otro tipo, asegúrate de cambiar el parámetro del método.
            var boleto = _context.Boletos.FirstOrDefault(b => b.BoletoId == boletoId);
            return boleto;
        }
        public Funcione ObtenerFuncionPorId(int FuncionId)
        {
            var funcion = _context.Funciones.FirstOrDefault(f => f.FuncionId == FuncionId);
            return funcion;
        }
        public Asiento ObtenerAsientoPorId(int asientoId)
        {
            var asiento = _context.Asientos.FirstOrDefault(a => a.AsientoId == asientoId);
            return asiento;
        }
        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] stringChars = new char[length];

            for (int i = 0; i < length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new String(stringChars);
        }
    }
}

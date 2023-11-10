using AppCine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCine.Controllers
{
    public class AdminController
    {
        private static Random random = new Random();

        private readonly CineBdContext _context;

        // Inyectar el contexto de la base de datos en el controlador
        public AdminController(CineBdContext context)
        {
            _context = context;
        }

        public void AgregarFuncion(Funcione nuevaFuncion)
        {
            // Agregar la nueva función al contexto
            _context.Funciones.Add(nuevaFuncion);
            // Guardar los cambios para obtener el ID de la función recién agregada si es generado por la base de datos
            _context.SaveChanges();

            // Crear 50 asientos asociados con la nueva función
            char[] filas = new char[] { 'A', 'B', 'C', 'D', 'E' };
            for (int i=0; i<filas.Length; i++)
            {
                for (int columna = 1; columna <= 10; columna++)
                {
                    var nuevoAsiento = new Asiento
                    {
                        FuncionId = nuevaFuncion.FuncionId, // Asignar el ID de la función recién creada
                        Fila = filas[i].ToString(),
                        Columna = columna,
                        Disponible = true // Todos los asientos inician como disponibles
                    };
                    _context.Asientos.Add(nuevoAsiento);
                }
            }
            // Guardar los cambios de los asientos en la base de datos
            _context.SaveChanges();
        }
        public List<Funcione> ObtenerTodasLasFunciones()
        {
            return _context.Funciones.OrderBy(f=>f.FechaHora).ToList();
        }
        public List<Boleto> ObtenerTodosLosBoletos()
        {
            return _context.Boletos.ToList();
        }
        public bool EliminarFuncionYRelacionados(int funcionId)
        {
            // Encontrar la función específica
            var funcion = _context.Funciones.Find(funcionId);

            if (funcion != null)
            {
                // Encontrar todos los boletos relacionados con la función
                var boletosRelacionados = _context.Boletos.Where(b => b.FuncionId == funcionId).ToList();
                // Encontrar todos los asientos relacionados con la función
                var asientosRelacionados = _context.Asientos.Where(a => a.FuncionId == funcionId).ToList();

                // Eliminar todos los boletos relacionados
                foreach (var boleto in boletosRelacionados)
                {
                    _context.Boletos.Remove(boleto);
                }

                // Eliminar todos los asientos relacionados
                foreach (var asiento in asientosRelacionados)
                {
                    _context.Asientos.Remove(asiento);
                }

                // Eliminar la función
                _context.Funciones.Remove(funcion);

                // Guardar todos los cambios en la base de datos
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}

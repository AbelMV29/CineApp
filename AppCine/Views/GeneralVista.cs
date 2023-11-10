using AppCine.Controllers;
using AppCine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCine.Views;

namespace AppCine.Views
{
    public class GeneralVista
    {
        private ClienteController _clienteController;
        private AdminController _adminController;

        public GeneralVista(ClienteController clienteController, AdminController adminController)
        {
            _clienteController = clienteController;
            _adminController = adminController;
        }
        
        public void Bienvenido()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------");
            Console.WriteLine("        NOMBRE DEL CINE ");
            Console.WriteLine("--------------------------------");
            Console.WriteLine();
        }
        public void CineCerrado()
        {
            Console.WriteLine();
            Console.WriteLine("-----EL ESTABLECIMIENTO SE ENCUENTRA CERRADO-----");
            Console.WriteLine();
            Console.WriteLine("       Cine abierto de Miercoles a Sabados");
        }
        public void MenuPrincipal()
        {
            bool inputValido = false;
            int opcionSeleccionada;

            while (!inputValido)
            {
                Bienvenido();
                Console.WriteLine("1. Ver Cartelera");
                Console.WriteLine("2. Comprar una Entrada");
                Console.WriteLine("3. Administrar");
                Console.WriteLine("4. Salir");
                Console.WriteLine();
                Console.WriteLine("Elija una opción (1, 2, 3, 4):");

                string input = Console.ReadLine();

                // Validación de la entrada: asegúrate de que es un número y está dentro del rango de opciones
                if (int.TryParse(input, out opcionSeleccionada) && opcionSeleccionada >= 1 && opcionSeleccionada <= 4)
                {
                    inputValido = true;
                    switch (opcionSeleccionada)
                    {
                        case 1:
                            MostrarInformacionFunciones(_clienteController.ObtenerTodasLasFunciones());
                            Console.WriteLine();
                            Console.WriteLine("Presione una tecla para volver al menu Principal");
                            Console.ReadKey();
                            MenuPrincipal();
                            break;
                        case 2:
                            ComprarBoleto(_clienteController.ObtenerTodasLasFunciones());
                            break;
                        case 3:
                            Login();
                            break;
                        case 4:
                            // Llamada al método para "Salir"
                            Environment.Exit(0);
                            break;
                        default:
                            // Esto no debería ocurrir debido a la validación de entrada
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Opción no válida, por favor intente de nuevo.");
                    Console.ReadKey();
                }
            }
        }
        public void MostrarInformacionFunciones(List<Funcione> funciones)
        {
            // Encabezado de la tabla
            Bienvenido();
            Console.WriteLine($"{"FuncionId",-10} {"Titulo",-25} {"FechaHora",-20} {"Duracion",-10} {"Genero",-15}");
            Console.WriteLine(new string('-', 75));

            // Datos de la tabla
            foreach (var funcion in funciones)
            {
                Console.WriteLine($"{funcion.FuncionId,-10} {funcion.Titulo,-25} {funcion.FechaHora,-20:dd/MM/yyyy HH:mm} {funcion.Duracion,-10:g} {funcion.Genero,-15}");
            }
            
        }
        public void MenuAdmin()
        {

            bool inputValido = false;
            int opcionSeleccionada;

            while (!inputValido)
            {
                Bienvenido();
                Console.WriteLine("1. Agregar Función");
                Console.WriteLine("2. Eliminar Función");
                Console.WriteLine("3. Ver Boletos");
                Console.WriteLine("4. Ver Todas las Funciones");
                Console.WriteLine("5. Volver al Menu Principal");
                Console.WriteLine("Elija una opción (1, 2, 3, 4, 5):");

                string input = Console.ReadLine();

                // Validación de la entrada: asegúrate de que es un número y está dentro del rango de opciones
                if (int.TryParse(input, out opcionSeleccionada) && opcionSeleccionada >= 1 && opcionSeleccionada <= 5)
                {
                    inputValido = true;
                    switch (opcionSeleccionada)
                    {
                        case 1:
                            LeerDatosFuncion();
                            break;
                        case 2:
                            MenuEliminarFuncion();
                            break;
                        case 3:
                            MostrarInformacionBoletos(_adminController.ObtenerTodosLosBoletos());
                            break;
                        case 4:
                            MostrarInformacionFunciones(_adminController.ObtenerTodasLasFunciones());
                            Console.WriteLine();
                            Console.WriteLine("Presione una tecla para volver al menu Principal");
                            Console.ReadKey();
                            MenuAdmin();
                            break;
                        case 5:
                            MenuPrincipal();
                            break;
                        default:
                            // Esto no debería ocurrir debido a la validación de entrada
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Opción no válida, por favor intente de nuevo.");
                    Console.ReadKey();
                }
            }
        }
        public void LeerDatosFuncion()
        {

            Bienvenido();
            Funcione funcionAux = new Funcione();

            Console.WriteLine("Ingrese los detalles de la nueva función:");

            Console.Write("Título: ");
            funcionAux.Titulo = Console.ReadLine();

            DateTime fechaActual = DateTime.Now;

            Console.WriteLine("Seleccione el día de la función:");
            Console.WriteLine("1. Miércoles");
            Console.WriteLine("2. Jueves");
            Console.WriteLine("3. Viernes");
            Console.WriteLine("4. Sábado");

            DateTime fechaHora;
            int diaElegido;
            while (!int.TryParse(Console.ReadLine(), out diaElegido) || diaElegido < 1 || diaElegido > 4)
            {
                Console.Write("Opción inválida. Seleccione el día de la función (1-4): ");
            }

            Console.WriteLine("Seleccione el horario de la función:");
            Console.WriteLine("1. 19:00");
            Console.WriteLine("2. 21:00");

            int horarioElegido;
            while (!int.TryParse(Console.ReadLine(), out horarioElegido) || horarioElegido < 1 || horarioElegido > 2)
            {
                Console.Write("Opción inválida. Seleccione el horario de la función (1-2): ");
            }

            // Calcular la próxima fecha del día de la semana elegido
            int diasParaAgregar = ((int)DayOfWeek.Wednesday - (int)fechaActual.DayOfWeek + 7 + (diaElegido - 1) * 1) % 7;
            if (diasParaAgregar == 0 || fechaActual.DayOfWeek > (DayOfWeek)(diaElegido + 2))
            {
                diasParaAgregar += 7;
            }
            DateTime proximaFecha = fechaActual.AddDays(diasParaAgregar).Date;

            // Establecer la hora según la opción elegida
            fechaHora = proximaFecha.AddHours(horarioElegido == 1 ? 19 : 21);

            // Si la fecha calculada es anterior a la actual, agregar una semana
            if (fechaHora < fechaActual)
            {
                fechaHora = fechaHora.AddDays(7);
            }

            funcionAux.FechaHora = fechaHora;

            int duracion;
            Console.Write("Duración (en minutos): ");
            while (!int.TryParse(Console.ReadLine(), out duracion))
            {
                Console.Write("Entrada inválida. Ingrese la duración en minutos: ");
            }
            funcionAux.Duracion = duracion;

            Console.Write("Género: ");
            funcionAux.Genero = Console.ReadLine();
            _adminController.AgregarFuncion(funcionAux);
            MenuAdmin();
        }
        public void MostrarInformacionBoletos(List<Boleto> boletos)
        {
            
            Bienvenido();
            Console.WriteLine($"{"BoletoId",-15} {"FuncionId",-10} {"AsientoId",-10}");
            Console.WriteLine(new string('-', 35));

            foreach (var boleto in boletos)
            {
                Console.WriteLine($"{boleto.BoletoId,-15} {boleto.FuncionId,-10} {boleto.AsientoId,-10}");
            }
            Console.WriteLine();
            Console.WriteLine("Presione una tecla para volver al menu Principal");
            Console.ReadKey();
            MenuAdmin();
        }
        public void MenuEliminarFuncion()
        {
            Bienvenido();
            Console.WriteLine("Ingrese el ID de la función que desea eliminar:");
            int funcionId;
            string input = Console.ReadLine();

            // Validación de la entrada: asegúrate de que es un número
            if (int.TryParse(input, out funcionId))
            {
                // Confirmación antes de la eliminación
                Console.WriteLine($"¿Está seguro de que desea eliminar la función con ID {funcionId}? (s/n)");
                if (Console.ReadLine().ToLower() == "s")
                {
                    bool eliminado = _adminController.EliminarFuncionYRelacionados(funcionId);
                    if (eliminado)
                    {
                        Console.WriteLine("Función eliminada correctamente.");
                    }
                    else
                    {
                        Console.WriteLine("No se encontró la función con el ID proporcionado.");
                    }
                }
                else
                {
                    Console.WriteLine("Eliminación cancelada.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido, por favor intente de nuevo.");
            }
            Console.WriteLine("Presione una tecla para volver al menú de administración.");
            Console.ReadKey();
            MenuAdmin();
        }
        public void ComprarBoleto(List<Funcione> funciones)
        {
            bool continuarCompra = true;

            while (continuarCompra)
            {
                MostrarInformacionFunciones(funciones);

                Console.WriteLine("Ingrese el ID de la función para la cual desea comprar un boleto:");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int funcionId))
                {
                    Funcione funcionSeleccionada = funciones.FirstOrDefault(f => f.FuncionId == funcionId);
                    if (funcionSeleccionada != null)
                    {
                        List<Asiento> asientos = _clienteController.ObtenerAsientosPorFuncionId(funcionSeleccionada.FuncionId);
                        Asiento asientoSeleccionado = SeleccionarAsiento(asientos);
                        if (asientoSeleccionado != null)
                        {
                            bool reservaExitosa = _clienteController.ReservarAsiento(asientoSeleccionado.AsientoId, funcionSeleccionada.FuncionId);
                            if (reservaExitosa)
                            {
                                Console.WriteLine("Su asiento ha sido reservado exitosamente.");
                                
                            }
                            else
                            {
                                Console.WriteLine("Hubo un problema al reservar su asiento. Presione una tecla para volver al menu.");
                                Console.ReadKey();
                                MenuPrincipal();
                            }
                        }
                        // Preguntar al usuario si quiere realizar otra acción
                        Console.WriteLine("¿Desea comprar otra entrada? (s/n)");
                        string respuesta = Console.ReadLine().Trim().ToLower();

                        if (respuesta == "n")
                        {
                            continuarCompra = false;
                        }
                        // No es necesario hacer nada si la respuesta es "s", ya que el bucle continuará.
                    }
                    else
                    {
                        Console.WriteLine("No se encontró una función con ese ID. Presione una tecla para volver al menu.");
                        Console.ReadKey();
                        MenuPrincipal();
                    }
                }
                else
                {
                    Console.WriteLine("ID inválido, por favor intente de nuevo.");
                }
            }

            Console.WriteLine("Gracias por su compra. Presione una tecla para volver al menú principal...");
            Console.ReadKey();
            MenuPrincipal();
        }
        public void MostrarAsientos(List<Asiento> asientos)
        {
            Bienvenido();
            int numColumnas = 10;
            char[] filas = { 'A', 'B', 'C', 'D', 'E' };

            Console.WriteLine("           Pantalla".PadLeft(numColumnas / 2 + "Pantalla".Length / 2).PadRight(numColumnas));
            Console.WriteLine();

            foreach (char fila in filas)
            {
                List<Asiento> asientosFila = asientos.Where(a => a.Fila == fila.ToString()).ToList();
                Console.Write(fila + " ");
                for (int col = 1; col <= numColumnas; col++)
                {
                    Asiento asiento = asientosFila.FirstOrDefault(a => a.Columna == col);
                    Console.Write(asiento != null && asiento.Disponible ? "[ ]" : "[X]");
                }
                Console.WriteLine(); // Nueva línea para la siguiente fila de asientos
            }
            Console.WriteLine("\n[ ] Disponible\n[X] Ocupado");
        }
        public Asiento SeleccionarAsiento(List<Asiento> asientos)
        {
            int numColumnas = 10;
            char[] filas = { 'A', 'B', 'C', 'D', 'E' };
            MostrarAsientos(asientos);

            Asiento asientoSeleccionado = null;
            while (asientoSeleccionado == null)
            {
                Console.WriteLine("Seleccione un asiento por fila y número (ejemplo: B5):");
                string eleccion = Console.ReadLine().ToUpper();

                if (!string.IsNullOrWhiteSpace(eleccion) &&
                    eleccion.Length >= 2 &&
                    filas.Contains(eleccion[0]) &&
                    int.TryParse(eleccion.Substring(1), out int numeroAsiento) &&
                    numeroAsiento >= 1 && numeroAsiento <= numColumnas)
                {
                    asientoSeleccionado = asientos.FirstOrDefault(a => a.Fila == eleccion[0].ToString() && a.Columna == numeroAsiento);

                    if (asientoSeleccionado != null && asientoSeleccionado.Disponible)
                    {
                        Console.WriteLine($"Ha seleccionado el asiento {eleccion}. Procesando su reserva...");
                        return asientoSeleccionado;
                    }
                    else
                    {
                        Console.WriteLine("Asiento no disponible o inexistente. Intente con otro asiento.");
                        asientoSeleccionado = null; // Permitir que el usuario intente de nuevo
                    }
                }
                else
                {
                    Console.WriteLine("Entrada inválida. Por favor, siga el formato de ejemplo 'B5'.");
                }
            }
            return asientoSeleccionado; // Este código nunca debe alcanzarse debido al bucle, pero es necesario para la compilación
        }
        public void Login()
        {
            Bienvenido();
            const string usuarioValido = "admin";
            const string contraseñaValida = "1234";

            Console.WriteLine("Ingrese su nombre de usuario:");
            string usuario = Console.ReadLine();

            Console.WriteLine("Ingrese su contraseña:");
            string contraseña = Console.ReadLine();

            if (usuario == usuarioValido && contraseña == contraseñaValida)
            {
                MenuAdmin();
            }
            else
            {
                Console.WriteLine("Usuario o contraseña incorrecta.Presione una tecla para volver al menu principal");
                Console.ReadKey();
                MenuPrincipal();
            }
        }
    }
}

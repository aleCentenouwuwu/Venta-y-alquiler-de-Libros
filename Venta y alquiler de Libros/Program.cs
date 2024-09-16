using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices.WindowsRuntime;
using static Venta_y_alquiler_de_Libros.Program;

// by Alexania J. Centeno Pauyh, Lia B. Matamoros Martínez and Victor A. Martinez Castellón

namespace Venta_y_alquiler_de_Libros
{
    public class Program
    {
        // LISTAS DE LIBROS

        public static List<Libro> LibrosVenta = new List<Libro>()
            {
                new Libro("Cien años de soledad", "Gabriel Garcia Marquez", "123456", 10, 200),
                new Libro("Pepito", "Pepe", "12ASF23", 0, 5),
                new Libro("La mano arriba", "cintura sola", "ERD3442" , 20, 300),
                new Libro("Harry Potter y la piedra filosofal o algo así", "J.K. Rowling","FF4234A",  10, 500),
                new Libro("El diario de Ana Frank", "Ana Frank", "2322SDDD", 30, 400),
                new Libro("El Principito", "Antoine de Saint-Exupéry", "200939FDD", 12, 350),
                new Libro("El Alquimista", "Paulo Coelho", "FRFG3222", 30, 460),
                new Libro("El viejo y el mar", "Hernest Hemingway","344ERRR3", 20, 200),
                new Libro("Farenheit 451", "Ray Bradbury", "34SFDDDF", 30 , 200),
                new Libro("Romeo y Julieta", "William Shakespeare", "ASDDDF23", 15 , 180),
            };

        public static List<Libro> LibrosAlquiler = new List<Libro>()
            {
                new Libro("Azul", "Ruben Dario", "22003RD", 50, 150),
                new Libro("Prosas Profanas", "Ruben Dario", "34DDFES", 20, 150),
                new Libro("El origen de las especies", "Charles Darwin", "21100GH", 5 , 800),
                new Libro("El capital", "Karl Marx", "34DDFFFFF", 10 , 500),
                new Libro("Don Quijote de la Mancha", "Miguel de Cervantes", "FGGG900", 12 , 300),
                new Libro("Metamorfosis", "Franz Kafka", "1200HHHH", 20 , 400),
                new Libro("El hombre en busca del sentido", "Viktor Frank", "52656JK4", 10, 350),
                new Libro("Los Miserables", "Victor Hugo", "0000FDD", 20 , 200),
                new Libro("Padre rico, padre pobre", "Robert Kiyosaki", "2312DRRT", 30, 250),
                new Libro("Aerodinamica", "Juan Zitnik", "121DFF000", 20 , 500),
            };

        public static Dictionary<string, Usuario> Usuarios = new Dictionary<string, Usuario>()
            {
                {
                    "id 3839", new Usuario("Alexania", "Profesor")
                },
                {
                    "id 4659", new Usuario("Victor", "Estudiante")
                },
                {
                    "id 1256", new Usuario("Lia", "Estudiante")
                },
                {
                    "id 8596", new Usuario("PinkyPie", "Cliente")
                },
                {
                    "id aqui", new Usuario("Freddy", "Estudiante")
                }
            };

        // PROGRAMA PRINCIPAL 
        static void Main(string[] args)
        {
            bool Continuar = false;

            do
            {
                try
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Clear();
                    Menu1();
                    byte opc = byte.Parse(Console.ReadLine());

                    switch (opc)
                    {
                        case 0:
                            Continuar = true;
                            break;    // Salida
                        case 1:
                            Venta(LibrosVenta);
                            break;    // Comprar
                        case 2:
                            Alquiler(LibrosAlquiler);
                            break;    // Alquilar
                        case 3:
                            Devolucion(LibrosAlquiler);
                            break;  //metodo devolucion
                        case 4:
                            Administrador();
                            break;
                        default:
                            MensajeError("Opcion no disponible, intentelo de nuevo.");
                            break;
                    }
                }
                catch (FormatException) { MensajeError("Error, ingrese informacion correctamente."); }
                catch (ArgumentException) { MensajeError("Error, ingrese informacion correctamente."); }
                catch (Exception) { MensajeError("Error, ingrese informacion correctamente."); }

            } while (!Continuar);
        }

        // METODOS

        public static void Menu1()
        {
            Console.Clear();
            SonidoMenu();
            string Bienvenida = "--------------------¡Bienvenido/a a Libreria Pompompurin!--------------------\n";
            Console.SetCursorPosition((Console.WindowWidth - Bienvenida.Length) / 2, Console.CursorTop);
            Console.WriteLine(Bienvenida);
            Console.WriteLine("¿Qué desea hacer?");
            Console.WriteLine("1. Comprar \n2. Alquilar \n3. Devolución \n4. Ver Inventario (solo administradores) \n0. Salir");
        }
        public static void MensajeError(string msg)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ReadKey();
        } // mensaje de error
        public static void MostrarLibros(List<Libro> libros, string tipo)
        {
            SonidoMenu();
            Console.WriteLine($"\nLos libros disponibles en apartado de {tipo} son: \n\n");
            foreach (var Libro in libros)
            {
                Console.WriteLine($"{Libro.Titulo}, de {Libro.Autor}, ISBN: {Libro.ISBN}, Cantidad: {Libro.Cantidad}, \tPrecio: C${Libro.Precio}\n");
            }
        } //imprime y muestra todos los datos de cada libro en una matriz dada
        public static void Venta(List<Libro> libros)
        {
            Console.Clear();
            SonidoMenu();
            MostrarLibros(LibrosVenta, "Venta");
            int i = BuscaLibros(LibrosVenta, "comprar");

            if (libros[i].Cantidad == 0)
            {
                Console.WriteLine("El libro actualmente no se encuentra en el sistema.");
            }
            else
            {
                SonidoMenu();
                int cant = Int32.Parse(DeclararVariable("¿Cuántos desea comprar?"));

                if (cant > libros[i].Cantidad)
                {
                    MensajeError("Lo sentimos, no contamos con tal cantidad.");
                }
                else
                {
                    libros[i].Cantidad = -cant;
                    Console.ForegroundColor = ConsoleColor.Green;
                    SonidoMenu();
                    Console.WriteLine($"Su total es de: C${libros[i].Precio * cant}, pagar en caja por favor.");
                    Console.WriteLine("¡Su compra fue exitosa! :D");
                }
            }; Console.ReadKey();
        }
        public static void Alquiler(List<Libro> libros)
        {
            SonidoMenu();
            string Cliente = VerificacionID();

            if (Cliente == "Cliente")
            {
                MensajeError("Lo siento, no puede alquilar libros");
                return;
            }

            MostrarLibros(libros, "Alquiler");
            int i = BuscaLibros(libros, "alquilar");

            SonidoMenu();
            int cant = Int32.Parse(DeclararVariable("¿Cuántos desea alquilar?"));

            if (cant > libros[i].Cantidad)
            {
                MensajeError("Límite excedido, no hay suficientes en inventario.");
            }
            else if (Cliente == "Estudiante" && cant > 3)
            {
                MensajeError("Límite excedido (max. 3).");
            }
            else
            {
                SonidoMenu();
                // Actualizamos la cantidad disponible
                libros[i].Cantidad -= cant;
                double Costo = libros[i].Precio * cant;

                Console.WriteLine($"Su total es de: C${Costo}, pagar en caja por favor.");
                DateTime fechaAlquiler = DateTime.Now;
                DateTime fechaDevolucion;
                if (Cliente == "Estudiante")
                {
                    fechaDevolucion = fechaAlquiler.AddDays(3);
                }
                else
                {
                    fechaDevolucion = fechaAlquiler.AddDays(7);
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Su alquiler fue hecho el {fechaAlquiler:dd/MM/yyyy}, deberá devolverlo el {fechaDevolucion:dd/MM/yyyy}.");
                SonidoMenu();

                if (Cliente == "Profesor")
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("\n¿Desea alquilarlo por más tiempo? \n1.Sí \n2. No");
                    byte opcion = byte.Parse(Console.ReadLine());
                    if (opcion == 1)
                    {
                        SonidoMenu();
                        Console.WriteLine("¿Cuántos días extra lo alquilará?");
                        int diasExtra = Int16.Parse(Console.ReadLine());

                        DateTime fechaDevolucionExtra = fechaDevolucion.AddDays(diasExtra);
                        int tarifa = diasExtra * 25;

                        Console.ForegroundColor = ConsoleColor.Green;
                        SonidoMenu();
                        Console.WriteLine($"Con la nueva fecha limite, su tarifa extra es de {tarifa}, en total: {Costo + tarifa} \nDebera de devolver el libro para la fecha {fechaDevolucionExtra} \nAlquiler realizado con exito.");
                    }
                    else if (opcion == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        SonidoMenu();
                        Console.WriteLine("Alquiler realizado con exito");
                    }
                    else
                    {
                        MensajeError("Opcion invalida");
                    }
                }
                Console.ReadKey();
            }
        }
        public static int BuscaLibros(List<Libro> libros, string cosa)
        {
            string titulo = DeclararVariable($"\nIngrese título del libro a {cosa}.");

            for (int i = 0; i < libros.Count; i++)
            {
                if (titulo == libros[i].Titulo)
                {
                    if (libros[i].Cantidad == 0)
                    {
                        MensajeError("El libro actualmente no se encuentra en el sistema.");
                        break;
                    }
                    return i;
                }
            }
            return -1;
        }
        public static void Devolucion(List<Libro> libros)
        {
            SonidoMenu();
            VerificacionID();
            int i = BuscaLibros(LibrosAlquiler, "devolver");

            SonidoMenu();
            Console.WriteLine("\n¿Cuántos libros va a devolver?");
            int cant = Int32.Parse(Console.ReadLine());
            libros[i].Cantidad += cant;
            Console.ReadKey();
        }
        public static string VerificacionID()
        {
            SonidoMenu();
            string id = DeclararVariable("Ingrese su identificacion");

            for (int i = 0; i < Usuarios.Count; i++)
            {
                Usuarios.TryGetValue(id, out var entidad);

                if (entidad.Tipo == "Cliente" || entidad.Tipo == "Estudiante" || entidad.Tipo == "Profesor")
                {
                    return entidad.Tipo;
                }
            }
            MensajeError("Usuario no encontrado.");
            return default;
        } // pide un id, lo busca en el diccionario y si lo encuentra devuelve el tipo de cliente con esa id
        public static void Administrador()
        {
            Console.Clear();
            SonidoMenu();
            string nombre = DeclararVariable("Ingrese su nombre");
            string contraseña = DeclararVariable("Ingrese contrasena para poder acceder (1234)"); //contrasena unica
            if (contraseña == "1234")
            {
                Console.Clear();
                SonidoMenu();
                Console.WriteLine($"Bienvenido, administrador/a {nombre}");
                Console.WriteLine("1. Inventario");
                Console.WriteLine("2. Historial de Movimientos");
                Console.WriteLine("0. Volver");
                byte opc1 = Convert.ToByte(Console.ReadLine());
                switch (opc1)
                {
                    case 1:
                        Console.Clear();
                        SonidoMenu();
                        MostrarLibros(LibrosVenta, "Venta");
                        MostrarLibros(LibrosAlquiler, "Alquiler");

                        Console.WriteLine("\n1. Administrar libros del sistema. \n0. Volver a menu principal");
                        byte opc = Convert.ToByte(Console.ReadLine());
                        switch (opc)
                        {
                            case 1:
                                AdministrarLibros();
                                break;//agregar una lista libro
                            case 0:
                                Menu1();
                                break;//Salida
                            default:
                                MensajeError("Error, intentelo de nuevo");
                                break;
                        }
                        Console.ReadKey();
                        break;
                    case 0:
                        Menu1();
                        break;
                    default:
                        MensajeError("Error, ingrese informacion correctamente");
                        break;
                }
            }
            else
            {
                MensajeError("Incorrecto :(");
            }
        }
        public static void AdministrarLibros()
        {
            Console.Clear();
            SonidoMenu();
            Console.WriteLine("1. Agregar libro a Venta \n2. Agregar libro a Alquiler \n3. Eliminar libro de Venta \n4. Eliminar libro de Alquiler \n0. Volver al menú principal");
            int opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    AñadirLibro(LibrosVenta, "venta"); //agregar a venta
                    break;
                case 2:
                    AñadirLibro(LibrosAlquiler, "alquiler"); //agregar a alquiler
                    break;
                case 3:
                    BorrarLibro(LibrosVenta, "Venta"); //borrar de venta
                    break;
                case 4:
                    BorrarLibro(LibrosAlquiler, "Alquiler"); //borrar de alquiler
                    break;
                case 0:
                    Menu1();
                    return;
                default:
                    MensajeError("Opción inválida, intentelo de nuevo.");
                    break;
            }
        }
        public static void BorrarLibro(List<Libro> libro, string tipo)
        {
            int i = BuscaLibros(libro, "borrar");
            
            libro.RemoveAt(i);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"El libro ha sido eliminado de la lista de {tipo}.");
            return;
        }
        public static void AñadirLibro(List<Libro> libro, string tipo) 
        {
            SonidoMenu();
            Console.WriteLine("Ingrese informacion del libro.");
            string Titulo = DeclararVariable("Titulo:");
            string Autor = DeclararVariable("Autor:");
            string ISBN = DeclararVariable("ISBN");
            int cantidad = Int32.Parse(DeclararVariable("Cantidad:"));
            int precio = Int32.Parse(DeclararVariable("Precio:"));

            libro.Add(new Libro(Titulo, Autor, ISBN, cantidad, precio));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"El libro se ha agregado a la lista de {tipo}.");
        }
        public static string DeclararVariable(string Peticion)
        {
            Console.WriteLine(Peticion);
            return Console.ReadLine();
        } // Imprime un pensaje y permite declarar la repuesta cpomo variable mas compacto
        static public void SonidoMenu()
        {
            string rutaSonido = @"menusound.wav";
            SoundPlayer player = new SoundPlayer(rutaSonido);
            player.Play();
        }
    }

    // CLASES
    public class Usuario
    {
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public Usuario(string nombre, string tipo)
        {
            Nombre = nombre;
            Tipo = tipo;
        }
    }
    public class Libro
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int Cantidad { get; set; }
        public int Precio { get; set; }
        public string ISBN { get; set; }

        public Libro(string titulo, string autor, string iSBN, int cantidad, int precio)
        {
            Titulo = titulo;
            Autor = autor;
            Cantidad = cantidad;
            Precio = precio;
            ISBN = iSBN;
        }
    }
}
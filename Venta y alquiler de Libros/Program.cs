using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;

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
                new Libro("Harry Potter y la piedra filosofal o algo así", "J.K. Rowling","FF4234A",  10, 500)
            };

        public static List<Libro> LibrosAlquiler = new List<Libro>()
            {
                new Libro("Azul", "Ruben Dario", "22003RD", 50, 150),
                new Libro("Prosas Profanas", "Ruben Dario", "34DDFES", 20, 150)
            };

        public static Dictionary<string, Usuario> Usuarios = new Dictionary<string, Usuario>()
            {
                {
                    "id 3839", new Usuario("aleale@gmail.com", "contrasena123","Tipo de cliente", "nombre")
                },
                {
                    "id aqui", new Usuario("uwu@gmail.com", "omg12345", "Tipo de cliente", "nombre")
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
                    Menu1();
                    byte opc = byte.Parse(Console.ReadLine());

                    switch (opc)
                    {
                        case 0:
                            Continuar = true;
                            break;    // Salida
                        case 1:
                            Compra();
                            break;    // Comprar
                        case 2:
                            Alquiler();
                            break;    // Alquilar
                        case 3:  //metodo devolucion
                            break;
                        case 4:
                            Administrador();
                            break;
                        default:
                            MensajeError("Opcion no disponible, intentelo de nuevo.");
                            break;
                    }
                }
                catch (FormatException) { MensajeError("no"); }
                catch (ArgumentException) { MensajeError("no"); }
                catch (Exception) { MensajeError("no"); }

            } while (!Continuar);
        }

        // METODOS

        public static void Menu1()
        {
            Console.Clear();
            Console.WriteLine($"Bienvenido, a libreria Pompompurin\n");
            Console.WriteLine("Que desea hacer?");
            Console.WriteLine("1. Comprar \n2. Alquilar \n3. Devolucion \n4. Inventario (solo administradores) \n0. Salir");
        }
        public static void MensajeError(string msg)
        {
            Console.Clear();
            Console.WriteLine(msg);
            Console.ReadKey();
        } // mensaje de error
        public static void MostrarLibrosCompra(List<Libro> libros)
        {
            Console.Clear();
            Console.WriteLine($"Los libros disponibles en apartado de ventas son: ");
            foreach (var Libro in libros)
            {
                Console.WriteLine($"Título: {Libro.Titulo}, Autor: {Libro.Autor}, ISBN: {Libro.ISBN}, Cantidad: {Libro.Cantidad}, Precio: C${Libro.Precio}");
            }
        }
        public static void MostrarLibrosAlquiler()
        {
            Console.Clear();
            Console.WriteLine($"Los libros disponibles en apartado de alquiler son: ");

            foreach (var Libro in LibrosAlquiler)
            {
                Console.WriteLine($"Título:{Libro.Titulo}, Autor:{Libro.Autor}, ISBN:{Libro.ISBN}, Cantidad:{Libro.Cantidad}, Precio:C${Libro.Precio}");
            }
        }
        public static void Compra()
        {
            MostrarLibrosCompra(LibrosVenta);
            Console.WriteLine("\nIngrese titulo del libro que desea comprar.");
            string nombre = Console.ReadLine();

            // tuve que ayudarme con la IA, perdon mi cabeza ya no da, son las 3am... lo siento :(

            for (int i = 0; i < LibrosVenta.Count; i++)
            {
                if (nombre == LibrosVenta[i].Titulo)
                {
                    if (LibrosVenta[i].Cantidad == 0)
                    {
                        Console.WriteLine("El libro actualmente no se encuentra en el sistema.");
                    }
                    else
                    {
                        Console.WriteLine("¿Cuántos desea comprar?");
                        int cant = Int32.Parse(Console.ReadLine());

                        if (cant > LibrosVenta[i].Cantidad)
                        {
                            Console.WriteLine("No seas consumista.");
                        }
                        else
                        {
                            LibrosVenta[i] = new Libro(LibrosVenta[i].Titulo, LibrosVenta[i].Autor, LibrosVenta[i].ISBN, LibrosVenta[i].Cantidad - cant, LibrosVenta[i].Cantidad);
                            Console.WriteLine($"Su total es de: C${LibrosVenta[i].Precio * cant}, pagar en caja por favor.");
                            Console.WriteLine("Su compra fue exitosa :D");
                        }

                    }
                }
            }; Console.ReadKey();
        }
        public static void Alquiler()
        {
            Console.WriteLine("Ingrese su rol \n1. Estudiante \n2. Profesor");
            byte opc = byte.Parse(Console.ReadLine());

            if (opc == 1)
            {
                bool Id = BoolIdEstudiante(Console.ReadLine());

                if (Id == false)
                {
                    MensajeError("Identificación no válida.");
                }
                else
                {
                    MostrarLibrosAlquiler();
                    Console.WriteLine("\nIngrese título del libro que desea alquilar.");
                    string nombre = Console.ReadLine();

                    for (int i = 0; i < LibrosAlquiler.Count; i++)
                    {
                        if (nombre == LibrosAlquiler[i].Titulo)
                        {
                            if (LibrosAlquiler[i].Cantidad == 0)
                            {
                                Console.WriteLine("El libro actualmente no se encuentra en el sistema.");
                            }
                            else
                            {
                                Console.WriteLine("¿Cuántos desea alquilar?");
                                int cant = Int32.Parse(Console.ReadLine());
                                if (cant > LibrosAlquiler[i].Cantidad || cant > 3)
                                {
                                    Console.WriteLine("Límite excedido (Libro no disponible o se intentó alquilar más de 3).");
                                }
                                else
                                {
                                    LibrosAlquiler[i] = new Libro(LibrosAlquiler[i].Titulo, LibrosAlquiler[i].Autor, LibrosAlquiler[i].ISBN, LibrosAlquiler[i].Cantidad - cant, LibrosAlquiler[i].Cantidad);
                                    Console.WriteLine($"Su total es de: C${LibrosAlquiler[i].Precio * cant}, pagar en caja por favor.");
                                    DateTime fechaAlquiler = DateTime.Now;
                                    DateTime fechaDevolucion = fechaAlquiler.AddDays(3);
                                    Console.WriteLine($"Su alquiler fue hecho el {fechaAlquiler:dd/MM/yyyy}, deberá devolverlo el {fechaDevolucion:dd/MM/yyyy}.");
                                }
                            }
                        }
                    }
                    Console.ReadKey();
                }
            }
            else if (opc == 2)
            {
                bool Id = BoolIdProfesor(Console.ReadLine());
                {
                    MostrarLibrosAlquiler();
                    Console.WriteLine("");
                    Console.WriteLine("Ingrese título del libro que desea alquilar.");
                    string nombre = Console.ReadLine();

                    for (int i = 0; i < LibrosAlquiler.Count; i++)
                    {
                        if (nombre == LibrosAlquiler[i].Titulo)
                        {
                            if (LibrosAlquiler[i].Cantidad == 0)
                            {
                                Console.WriteLine("El libro actualmente no se encuentra en el sistema.");
                            }
                            else
                            {
                                Console.WriteLine("¿Cuántos desea alquilar?");
                                int cant = Int32.Parse(Console.ReadLine());
                                if (cant > LibrosAlquiler[i].Cantidad || cant > 3)
                                {
                                    Console.WriteLine("Límite excedido (Libro no disponible o se intentó alquilar más de 3).");
                                }
                                else
                                {
                                    // Actualizamos la cantidad disponible
                                    LibrosAlquiler[i] = new Libro(LibrosAlquiler[i].Titulo, LibrosAlquiler[i].Autor, LibrosAlquiler[i].ISBN, LibrosAlquiler[i].Cantidad - cant, LibrosAlquiler[i].Cantidad);
                                    Console.WriteLine($"Su total es de: C${LibrosAlquiler[i].Precio * cant}, pagar en caja por favor.");
                                    DateTime fechaAlquiler = DateTime.Now;
                                    DateTime fechaDevolucion = fechaAlquiler.AddDays(7);
                                    Console.WriteLine($"Su alquiler fue hecho el {fechaAlquiler:dd/MM/yyyy}, deberá devolverlo el {fechaDevolucion:dd/MM/yyyy}.");
                                    Console.WriteLine("\n desea alquilarlo por mas tiepo?");
                                    Console.WriteLine("1. Si, porfa \n 2. No, gracias");
                                    if (byte.Parse(Console.ReadLine()) == 1)
                                    {
                                        Console.WriteLine($"su fecha limite actual es {fechaDevolucion:dd/MM/yyyy},\n  ¿Cuantos dias extra quiere prestare el libro¡");
                                        int diasExtra = Int16.Parse(Console.ReadLine());
                                        fechaDevolucion = fechaAlquiler.AddDays(diasExtra);

                                        int tarifa = diasExtra * 10;

                                        Console.WriteLine($"Con la nueva fecha limite, su tarifa extra es de {tarifa}, en total: {LibrosAlquiler[i].Precio * cant + tarifa}");
                                    }
                                }
                            }
                        }
                    }
                    Console.ReadKey();
                }
            }
            else
            {
                MensajeError("Opcion invalida");
            }

        }
        public static bool BoolIdEstudiante(string id)
        {
            Console.WriteLine("Ingrese su identificacion");
            id = Console.ReadLine();
            bool tieneLetra = id.Any(char.IsLetter);
            bool tieneGuion = id.Contains("-");
            bool esUnico = BoolUnico(id);
            // tuve que cambiar lo de !10, porque implicaria que puede ser 9 u 11
            if (id.Length < 10 || id.Length > 10 || tieneGuion == false || tieneLetra == false || esUnico == false)
            {
                MensajeError("Su identificacion es invalida");
                return false;
            }
            return true;
        }
        public static bool BoolIdProfesor(string id)
        {
            Console.WriteLine("Ingrese su identificacion");
            id = Console.ReadLine();
            bool tieneLetra = id.Any(char.IsLetter);
            bool tieneGuion = id.Contains("-");
            bool tieneNumeral = id.Contains("#");
            bool esUnico = BoolUnico(id);
            // tuve que cambiar lo de !10, porque implicaria que puede ser 9 u 11
            if (id.Length < 10 || id.Length > 10 || tieneGuion == false || tieneLetra == false || tieneNumeral == false || esUnico == false)
            {
                MensajeError("Su identificacion es invalida");
                return false;
            }
            return true;
        }
        public static bool BoolUnico(string id)
        {
            return !Usuarios.ContainsKey(id);

        } //Comprueba si el Id es irrepetible, de ser asi, retorna true
        public static void Administrador()
        {
            Console.Clear();
            Console.WriteLine("Ingrese su nombre");
            string nombre = Console.ReadLine();
            Console.WriteLine("Ingrese contrasena para poder acceder");
            string contrasena = "1234"; //contrasena unica
            string contraconfirm = Console.ReadLine();
            if (contraconfirm == contrasena)
            {
                Console.Clear();
                Console.WriteLine($"Bienvenido, administrador/a {nombre}");
                Console.WriteLine("1. Inventario");
                Console.WriteLine("2. Historial de Movimientos");
                Console.WriteLine("0. Volver");
                byte opc1 = Convert.ToByte(Console.ReadLine());
                switch (opc1)
                {
                    case 1: 
                        Console.Clear();
                        Console.WriteLine($"Los libros disponibles en apartado de ventas son: ");
                        foreach (var libro in LibrosVenta)
                        {
                            Console.WriteLine($"Título: {libro.Titulo}, Autor: {libro.Autor}, Cantidad: {libro.ISBN}, Precio: {libro.Cantidad}");
                        }
                        Console.WriteLine("");
                        Console.WriteLine($"Los libros disponibles en apartado de alquiler son: ");
                        foreach (var libro in LibrosAlquiler)
                        {
                            Console.WriteLine($"Título: {libro.Titulo}, Autor: {libro.Autor}, Cantidad: {libro.ISBN}, Precio: {libro.Cantidad}");
                        }
                        Console.WriteLine("\n1. Administrar libros del sistema. \n0. Volver a menu principal");
                        byte opc = Convert.ToByte(Console.ReadLine());
                        switch (opc)
                        {
                            case 1:
                                AdministrarLibros();
                                break;
                            //agregar una lista libro
                            case 2:
                            //borrar libro
                            case 0:
                                Menu1();
                                break;
                            default:
                                MensajeError("Error, intentelo de nuevo");
                                break;
                        }
                        Console.ReadKey();
                        break;
                    case 2:
                     //   Historial();
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
            Console.WriteLine("1. Agregar libro \n2. Borrar libro \n0. Volver a menu principal");
            int opcion = int.Parse(Console.ReadLine());
            switch (opcion)
            {
                case 1: //AGREGAR LIBRO A LISTA}

                    Console.WriteLine("Ingrese informacion del libro.");
                    string  Titulo      = DeclararVariable("Titulo:");
                    string  Autor       = DeclararVariable("Autor:");
                    string  ISBN        = DeclararVariable("ISBN");
                    int     cantidad    = Int32.Parse(DeclararVariable("Cantidad:"));
                    int     precio      = Int32.Parse(DeclararVariable("Precio:"));

                    int opc = int.Parse(DeclararVariable("1. Agregar a Venta \n2. Agregar a Alquiler"));

                    if (opc == 1)
                    {
                        LibrosVenta.Add(new Libro(Titulo, Autor, ISBN, cantidad, precio));
                        Console.WriteLine("El libro se ha agregado a la lista de ventas.");
                        Console.ReadKey();
                    }
                    else if (opc == 2)
                    {
                        LibrosAlquiler.Add(new Libro(Titulo, Autor, ISBN, cantidad, precio));
                        Console.WriteLine("El libro se ha agregado a la lista de alquiler.");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Opción inválida. El libro no fue agregado.");
                        Console.ReadKey();
                    }
                    break;
                case 2: //BORRAR LIBRO DEL SISTEMA
                    Console.WriteLine("Ingrese el título del libro que desea eliminar:");
                    string titulo = Console.ReadLine();
                    Console.WriteLine("1. Eliminar de Venta \n2. Eliminar de Alquiler");
                    byte op = byte.Parse(Console.ReadLine());
                    if (op == 1)
                    {
                        for (int i = 0; i < LibrosVenta.Count; i++)
                        {
                            if (LibrosVenta[i].Titulo == titulo)
                            {
                                LibrosVenta.RemoveAt(i);
                                Console.WriteLine("El libro ha sido eliminado de la lista de ventas.");
                                break;
                            }
                        }
                    }
                    else if (opcion == 2)
                    {
                        for (int i = 0; i < LibrosAlquiler.Count; i++)
                        {
                            if (LibrosAlquiler[i].Titulo == titulo)
                            {
                                LibrosAlquiler.RemoveAt(i);
                                Console.WriteLine("El libro ha sido eliminado de la lista de alquiler.");
                                break;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Opción inválida.");
                    }
                    break;
                case 0:
                    Menu1();
                    break;
                default:
                    MensajeError("Error, intentelo de nuevo");
                    break;

            }
        }
        public static string DeclararVariable(string Peticion)
        {
            Console.WriteLine(Peticion);
            return Console.ReadLine();
        } // Imprime un pensaje y permite declarar la repuesta cpomo variable mas compacto
        public static void Historial(List<Libro> libros)
        {
            Console.WriteLine("Historial de Movimientos");
            Console.WriteLine($"Alquileres:\n\n");

            foreach (var libro in libros)
            {
                Console.WriteLine($"Título: {libro.Titulo}");
               // Console.WriteLine($"Fecha de Alquiler: {libro.FechaAlquiler.ToShortDateString()}");
                //Console.WriteLine($"Fecha de Devolución: {libro.FechaDevolucion.ToShortDateString()}");
                Console.WriteLine("--------------------");
            }
            Console.ReadKey();
        }
    }

    // CLASES
    public class Usuario
    {
        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public Usuario(string correo, string contraseña, string nombre, string tipo)
        {
            Correo = correo;
            Contraseña = contraseña;
            Nombre = nombre;
            Tipo = tipo;
        }
    }
    public class Fecha
    {
        public DateTime FechaAlquiler { get; set; }
        public DateTime FechaDevolucion { get; set; }
        public Fecha(DateTime fechaAlquiler, DateTime fechaDevolucion)
        {
            FechaAlquiler = fechaAlquiler;
            FechaDevolucion = fechaDevolucion;
        }
    }
    public class Libro
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int Cantidad { get; set; }
        public int Precio { get; set; }
        public string ISBN { get; set; }

        public Libro(string titulo, /*Fecha fechas,*/ string autor, string iSBN, int cantidad, int precio)
        {
            Titulo = titulo;
            //FechasAlquiler = fechas;
            Autor = autor;
            Cantidad = cantidad;
            Precio = precio;
            ISBN = iSBN;
        }
    }

}
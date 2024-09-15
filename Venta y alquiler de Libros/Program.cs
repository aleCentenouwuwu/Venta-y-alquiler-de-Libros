using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;

namespace Venta_y_alquiler_de_Libros
{
    public class Program //dejalo asi extenso, es lo mejor, practicamente cada linea importa a como lo hice :ccccc plsplsplsplpslsplsplsplsp
    {
        // LISTAS DE LIBROS
        public static List<(string, string, string, int, int)> LibrosVenta = new List<(string, string, string, int, int)>()
            {
                ("Cien años de soledad", "Gabriel Garcia Marquez", "123456", 10, 200),
                ("Pepito", "Pepe", "12ASF23", 0, 5),
                ("La mano arriba", "cintura sola", "ERD3442" , 20, 300),
                ("Harry Potter y la piedra filosofal o algo así", "J.K. Rowling","FF4234A",  10, 500)
            };

        public static List<(string, string, string, int, int)> LibrosAlquiler = new List<(string, string, string, int, int)>()
            {
                ("Azul", "Ruben Dario", "22003RD", 50, 150),
                ("Prosas Profanas", "Ruben Dario", "34DDFES", 20, 150)
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
            bool Continuar = false ;

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
                } catch (FormatException) { MensajeError("no"); }
                  catch (ArgumentException) { MensajeError("no"); }
                  catch (Exception) { MensajeError("no"); }

            } while (!Continuar);
        }


        // METODOS

        public static void MensajeError(string msg) // mensaje de error 
        {
            Console.Clear();
            Console.WriteLine(msg);
            Console.ReadKey();
        }

        public static void MostrarLibrosCompra()
        {
            Console.Clear();
            Console.WriteLine($"Los libros disponibles en apartado de ventas son: ");
            foreach (var Libro in LibrosVenta)
            {
                Console.WriteLine($"Título: {Libro.Item1}, Autor: {Libro.Item2}, ISBN: {Libro.Item3}, Cantidad: {Libro.Item4}, Precio: C${Libro.Item5}");
            }
        }

        public static void MostrarLibrosAlquiler()
        {
            Console.Clear();
            Console.WriteLine($"Los libros disponibles en apartado de alquiler son: ");

            foreach (var Libro in LibrosAlquiler)
            {
                Console.WriteLine($"Título:{Libro.Item1}, Autor:{Libro.Item2}, ISBN:{Libro.Item3}, Cantidad:{Libro.Item4}, Precio:C${Libro.Item5}");
            }
        }

        public static void Compra()
        {
            MostrarLibrosCompra();
            Console.WriteLine("");
            Console.WriteLine("Ingrese titulo del libro que desea comprar.");
            string nombre = Console.ReadLine();
            // tuve que ayudarme con la IA, perdon mi cabeza ya no da, son las 3am... lo siento :(
            for (int i = 0; i < LibrosVenta.Count; i++) 
            {
                if (nombre == LibrosVenta[i].Item1)
                {
                    if (LibrosVenta[i].Item4 == 0)
                    {
                        Console.WriteLine("El libro actualmente no se encuentra en el sistema.");
                    }
                    else
                    {
                        Console.WriteLine("¿Cuántos desea comprar?");
                        int cant = Int32.Parse(Console.ReadLine());

                        if (cant > LibrosVenta[i].Item4)
                        {
                            Console.WriteLine("No seas consumista.");
                        }
                        else
                        {
                            var libroActualizado = (LibrosVenta[i].Item1, LibrosVenta[i].Item2, LibrosVenta[i].Item3, LibrosVenta[i].Item4 - cant, LibrosVenta[i].Item4);
                            LibrosVenta[i] = libroActualizado;
                            Console.WriteLine($"Su total es de: C${LibrosVenta[i].Item5 * cant}, pagar en caja por favor.");
                            Console.WriteLine("Su compra fue exitosa :D");
                        }
                        
                    }
                }
            }; Console.ReadKey();
        }

        public static void Menu1()
        {
            Console.Clear();
            Console.WriteLine($"Bienvenido, a libreria Pompompurin\n");
            Console.WriteLine("Que desea hacer?");
            Console.WriteLine("1. Comprar \n2. Alquilar \n3. Devolucion \n4. Inventario (solo administradores) \n0. Salir");
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
                    Console.WriteLine("");
                    Console.WriteLine("Ingrese título del libro que desea alquilar.");
                    string nombre = Console.ReadLine();

                    for (int i = 0; i < LibrosAlquiler.Count; i++)
                    {
                        if (nombre == LibrosAlquiler[i].Item1)
                        {
                            if (LibrosAlquiler[i].Item4 == 0)
                            {
                                Console.WriteLine("El libro actualmente no se encuentra en el sistema.");
                            }
                            else
                            {
                                Console.WriteLine("¿Cuántos desea alquilar?");
                                int cant = Int32.Parse(Console.ReadLine());
                                if (cant > LibrosAlquiler[i].Item4 || cant > 3)
                                {
                                    Console.WriteLine("Límite excedido (Libro no disponible o se intentó alquilar más de 3).");
                                }
                                else
                                {
                                    var libroActualizado = (LibrosAlquiler[i].Item1, LibrosAlquiler[i].Item2, LibrosAlquiler[i].Item3, LibrosAlquiler[i].Item4 - cant, LibrosAlquiler[i].Item4);
                                    LibrosAlquiler[i] = libroActualizado;
                                    Console.WriteLine($"Su total es de: C${LibrosAlquiler[i].Item5 * cant}, pagar en caja por favor.");
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
                        if (nombre == LibrosAlquiler[i].Item1)
                        {
                            if (LibrosAlquiler[i].Item4 == 0)
                            {
                                Console.WriteLine("El libro actualmente no se encuentra en el sistema.");
                            }
                            else
                            {
                                Console.WriteLine("¿Cuántos desea alquilar?");
                                int cant = Int32.Parse(Console.ReadLine());
                                if (cant > LibrosAlquiler[i].Item4 || cant > 3)
                                {
                                    Console.WriteLine("Límite excedido (Libro no disponible o se intentó alquilar más de 3).");
                                }
                                else
                                {
                                    // Actualizamos la cantidad disponible
                                    var libroActualizado = (LibrosAlquiler[i].Item1, LibrosAlquiler[i].Item2, LibrosAlquiler[i].Item3, LibrosAlquiler[i].Item4 - cant, LibrosAlquiler[i].Item4);
                                    LibrosAlquiler[i] = libroActualizado;
                                    Console.WriteLine($"Su total es de: C${LibrosAlquiler[i].Item5 * cant}, pagar en caja por favor.");
                                    DateTime fechaAlquiler = DateTime.Now;
                                    DateTime fechaDevolucion = fechaAlquiler.AddDays(7);
                                    Console.WriteLine($"Su alquiler fue hecho el {fechaAlquiler:dd/MM/yyyy}, deberá devolverlo el {fechaDevolucion:dd/MM/yyyy}.");
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

        }

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
                Console.WriteLine("2. Informacion de usuarios");
                Console.WriteLine("0. Volver");
                byte opc1 = Convert.ToByte(Console.ReadLine());
                switch (opc1)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine($"Los libros disponibles en apartado de ventas son: ");
                        foreach (var libro in LibrosVenta)
                        {
                            Console.WriteLine($"Título: {libro.Item1}, Autor: {libro.Item2}, Cantidad: {libro.Item3}, Precio: {libro.Item4}");
                        }
                        Console.WriteLine("");
                        Console.WriteLine($"Los libros disponibles en apartado de alquiler son: ");
                        foreach (var libro in LibrosAlquiler)
                        {
                            Console.WriteLine($"Título: {libro.Item1}, Autor: {libro.Item2}, Cantidad: {libro.Item3}, Precio: {libro.Item4}");
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
                Console.Clear();
                Console.WriteLine("Incorrecto :(");
                Console.ReadKey();
            }
        }

        public static void AdministrarLibros()
        {
            Console.Clear();
            Console.WriteLine("1. Agregar libro \n2. Borrar libro \n0. Volver a menu principal");
            int opcion = int.Parse(Console.ReadLine());
            switch (opcion)
            {
                case 1: //AGREGAR LIBRO A LISTA
                    Console.WriteLine("Ingrese informacion del libro.");
                    Console.WriteLine("Titulo:");
                    string Titulo = Console.ReadLine();
                    Console.WriteLine("Autor:");
                    string Autor = Console.ReadLine();
                    Console.WriteLine("ISBN");
                    string ISBN = Console.ReadLine();
                    Console.WriteLine("Cantidad:");
                    int cantidad = byte.Parse(Console.ReadLine());
                    Console.WriteLine("Precio:");
                    int precio = byte.Parse(Console.ReadLine());
                    Console.WriteLine("1. Agregar a Venta \n2. Agregar a Alquiler");
                    int opc = int.Parse(Console.ReadLine());
                    var nuevoLibro = (Titulo, Autor, ISBN, cantidad, precio);

                    if (opc == 1)
                    {
                        LibrosVenta.Add(nuevoLibro);
                        Console.WriteLine("El libro se ha agregado a la lista de ventas.");
                        Console.ReadKey();
                    }
                    else if (opc == 2)
                    {
                        LibrosAlquiler.Add(nuevoLibro);
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
                            if (LibrosVenta[i].Item1 == titulo)
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
                            if (LibrosAlquiler[i].Item1 == titulo)
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
}
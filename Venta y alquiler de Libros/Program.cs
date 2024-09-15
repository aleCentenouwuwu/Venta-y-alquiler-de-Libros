using System;
using System.Collections.Generic;
using System.Linq;

namespace Venta_y_alquiler_de_Libros
{
    public class Program
    {
        public static List<(string, string, int, int)> LibrosVenta = new List<(string, string, int, int)>()
            {
                ("Cien años de soledad", "Gabriel Garcia Marquez", 10, 200),
                ("Pepito", "Pepe", 0, 5),
                ("La mano arriba", "cintura sola", 20, 300),
                ("Harry Potter y la piedra filosofal o algo así", "J.K. Rowling", 10, 500)
            };

        public static List<(string, string, int, int)> LibrosAlquiler = new List<(string, string, int, int)>()
            {
                ("Azul", "Ruben Dario", 50, 150),
                ("Prosas Profanas", "Ruben Dario", 20, 150)
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

        static void Main(string[] args)
        {
            bool Salir = false;

            while (!Salir) // programa principal
            {
                Console.Clear();
                Console.WriteLine($"Bienvenido, a libreria Pompompurin\n\n");
                Console.WriteLine("Que desea hacer?\n");
                Console.WriteLine("1. Comprar \n2. Alquilar \n0. Salir");

                byte opc = byte.Parse(Console.ReadLine());

                switch (opc)
                {
                    case 0: Salir = true;   break;    // Salida
                    case 1: Compra();       break;    // Comprar
                    case 2: Alquiler();     break;    // Alquilar
                    default: MensajeError("Opcion erronea"); break;
                }
            };
        }

        public static void MensajeError(string msg) // mensaje de error mejorado
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
                Console.WriteLine($"Título: {Libro.Item1}, Autor: {Libro.Item2}, Cantidad: {Libro.Item3}, Precio: {Libro.Item4}");
            }
        }

        public static void MostrarLibrosAlquiler()
        {
            Console.WriteLine($"\nLos libros disponibles en apartado de alquiler son: ");

            foreach (var Libro in LibrosAlquiler)
            {
                Console.WriteLine($"Título: {Libro.Item1}, Autor: {Libro.Item2}, Cantidad: {Libro.Item3}, Precio: {Libro.Item4}");
            }
        }

        public static void Compra()
        {
            Console.Clear();
            MostrarLibrosCompra();
            Console.WriteLine("\n Que libro quiere comprar");
            string nombre = Console.ReadLine();

            foreach (var Libro in LibrosVenta)
            {
                if (nombre == Libro.Item1)
                {
                    if (Libro.Item3 == 0)
                    {
                        Console.WriteLine("Jo, No Hay de ese libro");
                    }
                    else
                    {
                        Console.WriteLine("CUANTOS >:(");
                        int cant = Int32.Parse(Console.ReadLine());

                        if (cant > Libro.Item3)
                        {
                            Console.WriteLine("no seas consumista");
                        };

                        Console.WriteLine("Su compra fue exitosa :D");
                        Console.ReadKey();
                    }
                }
            };
        }

        public static void Alquiler()
        {
            Console.WriteLine("Ingrese su identificacion");
            bool Id = BoolId(Console.ReadLine());

            if (Id == false)
            {
                MensajeError("No ccooreto");
            }
            else
            {
                Console.Clear();
                MostrarLibrosAlquiler();
                Console.WriteLine("\n Que libro quiere comprar");
                string nombre = Console.ReadLine();

                foreach (var Libro in LibrosAlquiler)
                {
                    if (nombre == Libro.Item1)
                    {
                        if (Libro.Item3 == 0)
                        {
                            Console.WriteLine("Jo, No Hay de ese libro");
                        }
                        else
                        {
                            Console.WriteLine("CUANTOS >:(");
                            int cant = Int32.Parse(Console.ReadLine());

                            if (cant > Libro.Item3)
                            {
                                Console.WriteLine("no seas consumista");
                            };

                            Console.WriteLine("Su compra fue exitosa :D");
                            Console.ReadKey();
                        }
                    }
                };
            }
        }

        public static bool BoolId(string id)
        {
            bool tieneLetra = id.Any(char.IsLetter);
            bool tieneGuion = id.Contains("-");
            bool esUnico = BoolUnico(id);

            if (id.Length != 10 || tieneGuion == false || tieneLetra == false || esUnico == false)
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
    }

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
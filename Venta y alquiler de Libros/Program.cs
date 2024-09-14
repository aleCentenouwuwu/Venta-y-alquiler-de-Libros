using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;

namespace Venta_y_alquiler_de_Libros
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<(string, string, int, int)> LibrosCompra = new List<(string, string, int, int)>();

            LibrosCompra.Add(("Cien anos de soledad", "Gabriel Garcia Marquez", 10, 200));
            LibrosCompra.Add(("Pepito", "Pepe", 0, 5));
            LibrosCompra.Add(("La mano arriba", "cintura sola", 20, 300));
            LibrosCompra.Add(("Harry Potter y la piedra filosofal o algo asi", "J.K. Rowling", 10, 500));
            LibrosCompra.Sort();

            List<(string, string, int, int)> LibrosAlquiler = new List<(string, string, int, int)>();
            LibrosAlquiler.Add(("Azul", "Ruben Dario", 50, 150));
            LibrosAlquiler.Add(("Prosas Profanas", "Ruben Dario", 20, 150));
            LibrosAlquiler.Sort();

            bool continuar = false;

            do // programa principal
            {
                try
                {
                    Menu1();
                    byte opc = Convert.ToByte(Console.ReadLine());
                    switch (opc)
                    {
                        case 0: continuar = true; break;

                        case 1: // CLIENTE
                            Menu2(); //muestra metodo de comprar
                            break;

                        case 2: // PROFESOR
                            Console.WriteLine("Por favor, ingrese su ID actual");
                            string id = Console.ReadLine();
                            VerifID(id);
                            //if verif valida xd mostrar opcion de alquilar y comprar
                            break;

                        case 3: // ESTUDIANTE
                            Console.WriteLine("Por favor, ingrese su ID actual");
                            string ID = Console.ReadLine();//con id
                            VerifID(ID);
                            //mostrar opcion de alquilar y comprar (con variantes)
                            break;

                        case 4: // ADMINISTRADOR DE LIBRERIA
                            //mostrar inventario e informacion de usuarios
                            Console.Clear();
                            Console.WriteLine("Ingrese su nombre");
                            string nombre = Console.ReadLine();
                            Console.WriteLine("Ingrese contrasena para poder acceder");
                            string contrasena = "1234pepitoclavounclavito"; //contrasena unica
                            string contraconfirm = Console.ReadLine();
                            if (contraconfirm == contrasena)
                            {
                                Console.Clear();
                                Console.WriteLine($"Bienvenido, administrador/a {nombre}");
                                Console.WriteLine("1. Inventario");
                                Console.WriteLine("2. Informacion de usuarios");
                                Console.WriteLine("0. Volver");
                                byte opc1 = Convert.ToByte(Console.ReadLine());
                                switch (opc1) {
                                    case 1:
                                    Console.Clear();
                                    Console.WriteLine($"Los libros disponibles en apartado de ventas son: ");
                                    foreach (var libro in LibrosCompra)
                                    {
                                        Console.WriteLine($"Título: {libro.Item1}, Autor: {libro.Item2}, Cantidad: {libro.Item3}, Precio: {libro.Item4}");
                                    }
                                    Console.WriteLine("");
                                    Console.WriteLine($"Los libros disponibles en apartado de alquiler son: ");
                                    foreach (var libro in LibrosAlquiler)
                                    {
                                        Console.WriteLine($"Título: {libro.Item1}, Autor: {libro.Item2}, Cantidad: {libro.Item3}, Precio: {libro.Item4}");
                                    }
                                        break;
                                    case 2:
                                        break;
                                    case 0:
                                        Menu1();
                                        break;
                                    }
                            } 
                            else 
                                {
                                Console.Clear();
                                Console.WriteLine("Incorrecto :("); }
                                Console.ReadKey();
                            break;

                        default:
                            Console.WriteLine("Error! Intentelo de nuevo");
                            break;
                    }
                }
                catch (FormatException)
                {
                    MensajeError("Ingrese datos correctamente, por favor.");
                }
            } while (!continuar);
        }

        public static void MensajeError(string msg) // mensaje de error mejorado
        {
            Console.Clear();
            Console.WriteLine(msg);
            Console.ReadKey();
        }

        public static void Menu1()
        {
            Console.Clear();
            Console.WriteLine("Bienvenido a libreria Pompompurin");
            Console.WriteLine("1. Cliente");
            Console.WriteLine("2. Profesor");
            Console.WriteLine("3. Estudiante");
            Console.WriteLine("4. Administrador");
            Console.WriteLine("0. Salir");
        }

        public static void Menu2()
        {
            Console.Clear();
            Console.WriteLine("Que desea hacer?");
            Console.WriteLine("1. Comprar");
            Console.WriteLine("2. Alquilar");
            Console.WriteLine("0. Volver");
            byte opc1 = byte.Parse(Console.ReadLine());

            switch (opc1)
            {
                case 1:
                    // metodo de compras
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Lo sentimos, pero no puede realizar esta accion.");
                    Console.ReadKey();
                    break;
                case 0:
                    Menu1();
                    break;
                default:
                    MensajeError("pruebe potra cosa pls");
                    break;
            }

        }

        public static void Menu3()
        {
            Console.Clear();
            Console.WriteLine("Que desea hacer?");
            Console.WriteLine("1. Comprar");
            Console.WriteLine("2. Alquilar");
            byte opc1 = byte.Parse(Console.ReadLine());
        }

        public static void VerifID(string id)
        {
            bool tieneLetra = id.Any(char.IsLetter);
            bool tieneGuion = id.Contains("-");
            if (id.Length<10 || id.Length>10 || tieneGuion == true || tieneLetra == true)
            {
                Console.Clear();
                Console.WriteLine("Su identificacion es invalida");
                Console.ReadKey();
            }
            else
            {
                Menu3();
            }
            
        }
    }
}

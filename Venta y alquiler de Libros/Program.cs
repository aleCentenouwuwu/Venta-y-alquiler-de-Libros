using System;
using System.Collections.Generic;
using System.Linq;

namespace Venta_y_alquiler_de_Libros
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<(string, string, int, int)> LibrosC = new List<(string, string, int, int)>(10);

            LibrosC.Add(("Cien anos de soledad", "Gabriel Garcia Marquez", 10, 200));
            LibrosC.Add(("Pepito", "Pepe", 0, 5));
            LibrosC.Add(("La mano arriba", "cintura sola", 20, 300));

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
                            // tambien podria agregar o quitar cosas del inventario, pero no es necesario
                            break;

                        default:
                            Console.WriteLine("Error, intentelo de nuevo");
                            break;
                    }
                }
                catch (FormatException)
                {
                    MensajeError("Sorry mejae de error");
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

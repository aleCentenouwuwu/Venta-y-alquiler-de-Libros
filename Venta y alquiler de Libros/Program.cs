using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Venta_y_alquiler_de_Libros
{
    public class Program
    { // programa principal
        static void Main(string[] args)
        {
            bool continuar = false;
            do
            {
                try
                {
                    Menu1();
                    byte opc = Convert.ToByte(Console.ReadLine());
                    switch (opc)
                    {
                        case 0: continuar = true; break;

                        case 1: // CLIENTE
                            Menu2();
                            //mostrar METODO de comprar
                            break;


                        case 2: // PROFESOR
                            Console.WriteLine("Por favor, ingrese su ID actual");
                            string id = Console.ReadLine();
                            VerifID(id);
                            break;


                        case 3: // ESTUDIANTE
                            Console.WriteLine("Por favor, ingrese su ID actual");
                            string ID = Console.ReadLine();//con id
                            VerifID(ID);
                            //mostrar opcion de alquilar y comprar (con variantes)
                            break;


                        case 4: // ADMINISTRADOR DE LIBRERIA
                            //mostrar inventario e informacion de usuarios
                            break;


                        default:
                            Console.WriteLine("Error, intentelo de nuevo");
                            break;
                    }
                }
                catch (FormatException)
                {
                    MensajeError();
                }
            } while (!continuar);
        }



        public static void MensajeError()
        {
            Console.Clear();
            Console.WriteLine("Error, por favor ingrese informacion correctamente");
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
                    MensajeError();
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

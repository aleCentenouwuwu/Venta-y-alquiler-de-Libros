using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;

namespace Venta_y_alquiler_de_Libros
{
    public class Program // falta: Funcion de comprar, alquilar, devolucion, guardar info de usuario (clientes) y FECHAAAAS
    {
        static void Main(string[] args)  // no entiendo nada :( omgg utilizaste mi codigo de usuarios? :DDDDDDDDD
        {
            // almacenamiento

            List<Libro> LibrosVenta = new List<Libro>
            {
                new Libro("Cien años de soledad", "Gabriel Garcia Marquez", 10, 200),
                new Libro("Pepito", "Pepe", 0, 5),
                new Libro("La mano arriba", "cintura sola", 20, 300),
                new Libro("Harry Potter y la piedra filosofal o algo así", "J.K. Rowling", 10, 500)
            };

            List<Libro> LibrosAlquiler = new List<Libro>
            {
                new Libro("Azul", "Ruben Dario", 50, 150),
                new Libro("Prosas Profanas", "Ruben Dario", 20, 150)
            };

            Dictionary<string, Usuario> Usuarios = new Dictionary<string, Usuario>()
            {
                {
                    "id aqui", new Usuario("aleale@gmail.com", "contrasena123","Tipo de cliente", "nombre")
                },
                {
                    "id aqui", new Usuario("uwu@gmail.com", "omg12345", "Tipo de cliente", "nombre")
                }
            };

            bool Salir = false;

            while (!Salir) // programa principal
            {
                Console.WriteLine("Ingrese su identificacion");
                bool Id = VerificarId(Console.ReadLine());

                Console.Clear();
                Console.WriteLine($"Bienvenido, a libreria Pompompurin\n\n");
                Console.WriteLine("Que desea hacer?\n");
                Console.WriteLine("1. Comprar \n2. Alquilar \n0. Volver");

                byte opc = byte.Parse(Console.ReadLine());

                switch (opc)
                {
                    case 0: Salir = true; break;      // Salida
                    case 1: Compra(); break;      // Comprar
                    case 2: Alquiler(); break;      // Alquilar
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

        public static void Compra()
        {

        }

        public static void Alquiler()
        {

        }

        public static bool VerificarId(string id)
        {
            bool tieneLetra = id.Any(char.IsLetter);
            bool tieneGuion = id.Contains("-");
            bool esUnico = ComprobarUnicidad(id);

            if (id.Length != 10 || tieneGuion == false || tieneLetra == false || esUnico == false)
            {
                MensajeError("Su identificacion es invalida");
                return false;
            }
            return true;
        }

        public static bool ComprobarUnicidad(string id)
        {
            if (id == "Si se hace x cosa y es unica")
            {
                return true;
            }
            return false;
        }
    }

    public class Libro
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int Cantidad { get; set; }
        public int Precio { get; set; }

        public Libro(string titulo, string autor, int cantidad, int precio)
        {
            Titulo = titulo;
            Autor = autor;
            Cantidad = cantidad;
            Precio = precio;
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

    public class Sistema
    {
        private List<Usuario> usuarios;
        private Usuario usuarioActual;

        public bool IniciarSesion(string correo, string contraseña)
        {
            foreach (var usuario in usuarios)
            {
                if (usuario.Correo == correo && usuario.Contraseña == contraseña)
                {
                    usuarioActual = usuario;
                    return true;
                }
            }
            return false;
        }

        public bool EsCorreoValido(string correo)
        {
            return true;
        }

        public bool CrearCuenta(string correo, string contraseña)
        {
            if (!EsCorreoValido(correo))
            {
                Console.WriteLine("Correo electrónico no válido.");
                return false;
            }

            if (contraseña.Length < 8)
            {
                Console.WriteLine("La contraseña debe tener al menos 8 caracteres.");
                return false;
            }

            foreach (var usuario in usuarios)
            {
                if (usuario.Correo == correo)
                {
                    Console.WriteLine("El correo electrónico ya está en uso.");
                    return false;
                }
            }

            //usuarios.Add(new Usuario(correo, contraseña));
            Console.Clear();
            Console.WriteLine("Cuenta creada exitosamente.");
            Console.ReadKey();

            return true;
        }
    }
}
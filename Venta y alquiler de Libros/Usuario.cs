using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta_y_alquiler_de_Libros
{
    public class Usuario
    {

        public string nombre { get; set; }
        public string TipoUsuario { get; set; }
        public Usuario(string nombre, string TipoUsuario)
        {
            this.nombre= nombre;
            this.TipoUsuario = TipoUsuario;
        }

        public virtual void RealizarCompra()
        {

        }

        public virtual void RealizarAlquiler()
        {

        }
    }
}

/* Agregare aca el codigo que tenia sobre los usuarios, por si te sirve de algo, si es que estas aca xddd 
 * namespace SistemadeUsuarios
{
    public class Usuario
    {
        public string Correo { get; set; }
        public string Contraseña { get; set; }

        public Usuario(string correo, string contraseña)
        {
            Correo = correo;
            Contraseña = contraseña;
        }
    }
}


 public class Sistema
    {
        private List<Usuario> usuarios;
        private Usuario usuarioActual;

        public Sistema()
        {
            usuarios = new List<Usuario>();
            usuarios.Add(new Usuario("aleale@gmail.com", "contrasena123"));
            usuarios.Add(new Usuario("uwu@gmail.com", "omg12345"));
        }

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

            usuarios.Add(new Usuario(correo, contraseña));
            Console.Clear();
            Console.WriteLine("Cuenta creada exitosamente.");
            Console.ReadKey();

            return true;
        }
*/
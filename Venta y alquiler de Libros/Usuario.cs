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

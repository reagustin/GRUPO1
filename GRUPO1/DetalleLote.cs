using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRUPO1
{
    public class DetalleLote
    {
        public Comercio comercio { get; set; }
        public List<Elemento> listapedido { get; set; }

        public DetalleLote()
        {
            comercio = new Comercio();
            listapedido = new List<Elemento>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRUPO1
{
    public class Lote
    {
        public Empresa empresa { get; set; }
        public List<DetalleLote> detallelote { get; set; }

        public Lote()
        {
            empresa = new Empresa();
            detallelote = new List<DetalleLote>();
        }
    }
}

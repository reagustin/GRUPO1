using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRUPO1
{
    public class Empresa
    {

        public string codigo { get; set; }
        public string razonsocial { get; set; }
        public string cuit { get; set; }
        public string domicilio { get; set; }

        private List<Elemento> _stock;
        public List<Elemento> stock
        {
            get
            {
                return _stock; // esto esta implicito en el get
            }
            set
            {
                _stock = value; // esto esta implicito en el set
            }


        }

        private List<Pedido> _pedidos;
        public List<Pedido> pedidos
        {
            get
            {
                return _pedidos; // esto esta implicito en el get
            }
            set
            {
                _pedidos = value; // esto esta implicito en el set
            }
        }

        public void GuardarPedido(Pedido pedido)
        {
            _pedidos.Add(pedido);
        }

        public Empresa()
        {
            _stock = new List<Elemento>();
            _pedidos = new List<Pedido>();
        }
        public void GuardarStock(Elemento stock)
        {
            _stock.Add(stock);
        }


    }
}

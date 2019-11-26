using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRUPO1
{
    /// <summary>
    /// Pedido_[Codigo].txt. <-- string codigo contiene el codigo leido del txt para identificar el codigo del pedido
    /// [Código de comercio];[Razón Social];[CUIT];[Dirección de entrega] <<-- La clase comercio tiene todos los datos
    /// [Código de producto];[Cantidad] <-- Lista de Elementos, objeto que contiene cantidad y el objeto producto dentro.
    /// [Código de producto];[Cantidad]
    /// </summary>
    public class Pedido
    {
        public Comercio comercio { get; set; }
        public List<Elemento> listaproducto { get; set; }
        public string codigo { get; set; }
        public bool EnviadoLogistica { get; set; }
        public bool Entregado { get; set; }

        public Pedido()
        {
            comercio = new Comercio();
            listaproducto = new List<Elemento>();
        }

        public void GuardarPedido(Elemento element)
        {
            listaproducto.Add(element);
        }



    }

}


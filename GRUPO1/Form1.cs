// v9.4 3/12 9AM

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GRUPO1
{
    public partial class FrmTP1 : Form
    {
        enum tipodelista
        {
            listapedidos = 1, listadevoluciones = 2

        }
        string rutaarchivo = @"C:\TP\stock.txt";
        string rutapedidohistorico = @"C:\TP\pedidoscodigohistorico.txt";
        string rutapedidodiario = @"C:\TP\pedidos.txt";
        string enviologistica = @"C:\TP\";
        string codigodecomercio = "M023";
        string rutadevolucionesprocesadas = @"C:\TP\DevolucionesProcesadas.txt";
        Empresa EmpresaInstanciada;

        List<string> ListaDevolucionesHistorica = new List<string>();
        List<string> ListaPedidosTemporal = new List<string>();

        List<Devolucion> ListaDevoluciones = new List<Devolucion>(); //antes estaba en el metodo ProcesaDevoluciones
        Devolucion Devoluc; //antes estaba en el metodo ProcesaDevoluciones

        public FrmTP1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "*.txt")
            {
                txtRuta.Text = openFileDialog1.FileName;
            }


        }



        private void btnProcesar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRuta.Text != string.Empty)
                {
                    if (RdnRecepcionPedido.Checked)
                    {
                        ProcesaPedido(txtRuta.Text);

                    }
                    else if (rdnRecepcionStock.Checked)
                    {
                        ProcesaStock(txtRuta.Text);
                        MessageBox.Show("El stock fue procesado correctamente", "Informacion");
                    }
                    else if (rdnRecepcionLogistica.Checked)
                    {
                        ProcesaDevoluciones(txtRuta.Text); //PROCESO EL ENVIO A LOGISTICA -- //DEVUELVO AL STOCK
                        RegenerarArchivoDePedidos(); // DEVUELVO AL STOCK
                        MessageBox.Show("Se procesaron las devoluciones correctamente", "Informacion");
                    }

                }
                else
                {
                    if (RdnEnvioLogistica.Checked)
                    {
                        ProcesaEnvioLogistica(); // PROCESO EL ENVIO A LOGISTICA
                        MessageBox.Show("El proceso de envio a logistica culmino correctamente.", "Informacion");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void RegenerarArchivoDePedidos() // ya tengo la devolucion de pedidos cargada en memoria
        {
            foreach (Devolucion devolucion in ListaDevoluciones) // EMPIEZO A RECORRER UNO POR UNO LAS DEVOLUCIONES
            {
                Pedido DevolucionPedido = (Pedido)EmpresaInstanciada.pedidos.Find(x => x.codigo == (devolucion.CodigoReferencia)); // BUSCO PARA X DEVOLUCION EL PEDIDO EQUIVALENTE EN MEMORIA

                if (DevolucionPedido != null) // SI NO ES NULO, O SEA SI ENCONTRO UN PEDIDO DENTRO DE LA LISTA DE PEDIDOS DE MEMORIA, HACE LO SIGUIENTE
                {
                    if (devolucion.Entregado == false) // SI EL CAMPO QUE VENIA DEL PRIMER PEDIDO MATCHEADO DE LA DEVOLUCION ESTABA EN FALSE
                    {
                        DevolucionPedido.Entregado = false; // ENTONCES LO CAMBIA A TRUE Y ADEMAS LO TIENE QUE DEVOLVER AL STOCK


                        foreach (Elemento ElementoPedido in DevolucionPedido.listaproducto) // PARA CADA ELEMENTO DE LA LISTA DE ELEMENTOS DEL PEDIDO QUE MATCHEO EN EL PUNTO ANTERIOR                 
                        {

                            Elemento ElementoEncontrado = (Elemento)EmpresaInstanciada.stock.Find(x => x.prod.idprod == (ElementoPedido.prod.idprod)); // BUSCO EL ELEMENTO EN EL STOCK
                            ElementoEncontrado.cantidad = ElementoEncontrado.cantidad + ElementoPedido.cantidad; // LE ADICIONO AL STOCK LO QUE DEVUELVO DE ESTE ITEM

                        }
                    }
                    else
                    {
                        DevolucionPedido.Entregado = true;
                    }

                }
            }
            ListaDevoluciones = new List<Devolucion>();
            GrabarPedidosTxt();
            GrabarStockTxt();
            //ConfirmacionProcesado.Items.Add(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " - Se grabo el archivo pedidos");
        }

        private void ProcesaDevoluciones(string Ruta)
        {

            string[] lineas = null;


            if (File.Exists(Ruta))
            {
                lineas = File.ReadAllLines(Ruta);
                string[] lineasplit = null;
                foreach (string linea in lineas)
                {
                    if (linea != string.Empty)
                    {


                        //aca empiezo a guardar cada contenido del array en el list de elementos acorde a mi stock (llamo a los objeto elemento, les pongo lo que corresponde
                        // y luego es lo guardo en un metodo en la clase Empresa dentro de la lista Stock! voila
                        Devoluc = new Devolucion();
                        lineasplit = linea.Split(';');
                        bool entrego = false;
                        if (
                                (lineasplit.Count() == 2) &&
                                (bool.TryParse(lineasplit[1], out entrego)))

                        {
                            Devoluc.CodigoReferencia = lineasplit[0];
                            Devoluc.Entregado = entrego;

                            if (VerificaDevolucion(Devoluc.CodigoReferencia))
                            {
                                if (!ListaDevolucionesHistorica.Contains(Devoluc.CodigoReferencia))
                                {

                                    ListaDevoluciones.Add(Devoluc);
                                    ListaDevolucionesHistorica.Add(Devoluc.CodigoReferencia);
                                    using (StreamWriter sw = File.AppendText(rutadevolucionesprocesadas))
                                    {
                                        sw.Write(Devoluc.CodigoReferencia + "\r\n");
                                    }

                                }
                                else
                                {
                                    ConfirmacionProcesado.Items.Add(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " - La devolucion del pedido " + Devoluc.CodigoReferencia + " ya fue procesado con anterioridad");
                                }
                            }
                            else
                            {
                                ConfirmacionProcesado.Items.Add(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " - El pedido informado con el codigo " + Devoluc.CodigoReferencia + " no se encuentra registrado en nuestra lista de pedidos enviado a logistica.");
                            }
                        }
                        else
                        {
                            throw new Exception("Error en el formato del archivo de devoluciones, el proceso finalizo sin completarse");
                        }
                    }

                }
            }
        }

        private bool VerificaDevolucion(string CodigoDePedido)
        {
            var pedido = EmpresaInstanciada.pedidos.Find(n => n.codigo == CodigoDePedido);
            return pedido != null;
        }

        private bool VerificaStockDePedido(List<Elemento> lista)
        {
            foreach (Elemento elem in lista)
            {
                Elemento ElementoEncontrado = (Elemento)EmpresaInstanciada.stock.Find(x => x.prod.idprod == (elem.prod.idprod));
                if (ElementoEncontrado != null)
                {
                    if (ElementoEncontrado.cantidad < elem.cantidad)
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        private void ProcesaEnvioLogistica()
        {
            string nombreArchivoLogistica = enviologistica + "Lote_" + codigodecomercio + "_L" + new Random().Next(1, 999).ToString() + ".txt";

            using (StreamWriter sw = File.CreateText(nombreArchivoLogistica))
            {


                foreach (Pedido PedidoActualComercio in EmpresaInstanciada.pedidos)
                {
                    if (PedidoActualComercio.EnviadoLogistica == false)//Esto lo habiamos visto pero no se por que no lo modificamos, unicamente procesa los enviados a logistica false.. ya que los true ya los envio
                    {
                        //Ultima modificacion 25-11-2019, esto controla que todos los productos del pedido tengan stock, caso contrario el pedido no se procesa
                        //Tampoco se elimina, solo que se "saltea", de forma tal que si se procesa otro lote de produccion que aumenta el stock en el siguiente envio a logistica se vuelve a procesar
                        //Como los pedidos tienen orden cronologico se va a procesar primero.
                        if (VerificaStockDePedido(PedidoActualComercio.listaproducto))//Esto es lo ultimo que hicimos no lo modifique anda
                        {
                            sw.WriteLine(EmpresaInstanciada.razonsocial + ";" + EmpresaInstanciada.cuit + ";" + EmpresaInstanciada.domicilio);
                            sw.WriteLine("---");
                            sw.WriteLine(PedidoActualComercio.codigo + ";" + PedidoActualComercio.comercio.domicilio);


                            foreach (Elemento elementopedido in PedidoActualComercio.listaproducto)
                            {

                                Elemento ElementoEncontrado = (Elemento)EmpresaInstanciada.stock.Find(x => x.prod.idprod == (elementopedido.prod.idprod));

                                if (ElementoEncontrado != null)
                                {
                                    if (ElementoEncontrado.cantidad >= elementopedido.cantidad)
                                    {
                                        ElementoEncontrado.cantidad = ElementoEncontrado.cantidad - elementopedido.cantidad;
                                        sw.WriteLine(elementopedido.prod.idprod + ";" + elementopedido.cantidad);
                                    }

                                }
                            }
                            PedidoActualComercio.EnviadoLogistica = true;
                        }
                        else
                        {
                            //No tenia stock entonces le dejo estado en false, procesalo en el  siguiente envio
                            PedidoActualComercio.EnviadoLogistica = false;
                        }
                    }
                }
                sw.Flush();
            }
            string[] manipulacionDeRuta = nombreArchivoLogistica.Split('\\');
            //Si el archivo esta vacio es que no tiene pedidos lo borro
            bool seborra = false;
            using (var myFile = File.Open(nombreArchivoLogistica, FileMode.Open))
            {
                if (myFile.Length == 0)
                    seborra = true;
            }
            if (seborra)
            {
                File.Delete(nombreArchivoLogistica);
                ConfirmacionProcesado.Items.Add(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " - No se genero ningun lote a logistica");
                ConfirmacionProcesado.SelectedIndex = ConfirmacionProcesado.Items.Count - 1;
            }
            else
            {
                ConfirmacionProcesado.Items.Add(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " - Se genero el archivo " + manipulacionDeRuta[2] + " de lote para logistica");
                ConfirmacionProcesado.SelectedIndex = ConfirmacionProcesado.Items.Count - 1;
            }
            GrabarStockTxt();
            GrabarPedidosTxt();
            //ConfirmacionProcesado.Items.Add(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " - Se actualizo stock y pedidos");

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ruta"></param>
        private void ProcesaPedido(string Ruta)
        {
            long numerocuit = 0;
            int nrocodcli = 0;
            int nrolinea = 0;
            string[] lineas = null;
            string[] nombrepedidoruta = null;
            string[] remueveExtension = null;
            Elemento Element;
            Pedido pedidoComercio = new Pedido();
            if (File.Exists(Ruta))
            {
                nombrepedidoruta = Ruta.Split('_');
                remueveExtension = nombrepedidoruta[1].Split('.');
                if (ListaPedidosTemporal.Contains(remueveExtension[0]))
                {
                    MessageBox.Show("Ya esta cargado este pedido", "Atencion");
                }
                else
                {

                    lineas = File.ReadAllLines(Ruta);
                    string[] lineasplit = null;
                    foreach (string linea in lineas)
                    {
                        if (nrolinea == 0)
                        {
                            lineasplit = linea.Split(';');
                            if (lineasplit.Count() == 4 && !string.IsNullOrWhiteSpace(lineasplit[0]) 
                                                        && lineasplit[0].Count()>=2 && lineasplit[0].Remove(1).ToUpper() == "C" //v9.5
                                                        && int.TryParse(lineasplit[0].Remove(0, 1), out nrocodcli)
                                                        && nrocodcli > 0
                                                        && !string.IsNullOrWhiteSpace(lineasplit[1])
                                                        && !string.IsNullOrWhiteSpace(lineasplit[2]) && lineasplit[2].IndexOf("-") == 2 && lineasplit[2].LastIndexOf("-") == 11
                                                        && lineasplit[2].Replace("-", "").Count() == 11
                                                        && Int64.TryParse(lineasplit[2].Replace("-", ""), out numerocuit)
                                                        && numerocuit >= 0 && numerocuit <= 99999999999 // (Son 11 numeros 9) Int64 maximo valor: 9223372036854775807.													
                                                        && !string.IsNullOrWhiteSpace(lineasplit[3]))
                            {
                                pedidoComercio.codigo = remueveExtension[0];
                                pedidoComercio.EnviadoLogistica = false;
                                pedidoComercio.comercio.codigo = lineasplit[0];
                                pedidoComercio.comercio.razonsocial = lineasplit[1];
                                pedidoComercio.comercio.cuit = lineasplit[2];
                                pedidoComercio.comercio.domicilio = lineasplit[3];
                                nrolinea = nrolinea + 1;
                            }
                            else
                            {
                                throw new Exception("Error en el formato del archivo de pedido en la cabecera, el proceso finalizo sin completarse");
                            }
                        }
                        else
                        {
                            int numprod;
                            int cantidad = 0;
                            lineasplit = linea.Split(';');
                            if ((lineasplit.Count() == 2) && lineasplit[0].Count() >= 2 //v9.5
                                                          && lineasplit[0].Remove(1).ToUpper() == "P"
                                                          && lineasplit[0].Remove(0, 1).Count() >= 1
                                                          && lineasplit[0].Remove(0, 1).Count() < 6
                                                          && int.TryParse(lineasplit[0].Remove(0, 1), out numprod)
                                                          && numprod >= 1
                                                          && (int.TryParse(lineasplit[1], out cantidad))
                                                          && cantidad > 0)
                            {
                                Elemento ElementoEncontrado = (Elemento)EmpresaInstanciada.stock.Find(x => x.prod.idprod == lineasplit[0]);
                                if (ElementoEncontrado != null)
                                {
                                    Element = new Elemento();
                                    Element.prod.idprod = lineasplit[0];
                                    Element.cantidad = cantidad;
                                    pedidoComercio.GuardarPedido(Element);
                                }
                            }
                            else
                            {
                                throw new Exception("Error en el formato del archivo de pedido, el proceso finalizo sin completarse");
                            }
                        }

                    }
                    if (pedidoComercio.listaproducto.Count > 0)
                    {
                        EmpresaInstanciada.pedidos.Add(pedidoComercio);
                    }
                    ListaPedidosTemporal.Add(remueveExtension[0]);
                    using (StreamWriter sw = File.AppendText(rutapedidohistorico))
                    {
                        sw.Write(remueveExtension[0] + "\r\n");
                    }
                    MessageBox.Show("El pedido fue procesado correctamente", "Informacion");
                }
                GrabarPedidosTxt();

            }
            ConfirmacionProcesado.Items.Add(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " - Pedido Procesado");
            ConfirmacionProcesado.SelectedIndex = ConfirmacionProcesado.Items.Count - 1;

        }


        private void GrabarPedidosTxt()
        {
            if (File.Exists(rutapedidodiario))
            {
                File.Delete(rutapedidodiario);

                using (StreamWriter sw = File.CreateText(rutapedidodiario))
                {

                    foreach (Pedido PedidoActualComercio in EmpresaInstanciada.pedidos)
                    {
                        sw.WriteLine(PedidoActualComercio.codigo + ";" + PedidoActualComercio.comercio.codigo + ";" + PedidoActualComercio.comercio.razonsocial + ";" + PedidoActualComercio.comercio.cuit + ";" + PedidoActualComercio.comercio.domicilio + ";" + PedidoActualComercio.EnviadoLogistica);


                        foreach (Elemento elementopedido in PedidoActualComercio.listaproducto)
                        {
                            sw.WriteLine(elementopedido.prod.idprod + ";" + elementopedido.cantidad);
                        }
                    }

                }

            }
            //ConfirmacionProcesado.Items.Add(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " - Se grabo el archivo pedidos");

        }




        private void ProcesaStock(string Ruta) //////////// STOCK SE GRABA EN MEMORIA //////////////
        {

            int numprod;
            List<Elemento> listaproduccion = new List<Elemento>();
            string[] lineas = null;
            Elemento Element;
            if (File.Exists(Ruta))
            {

                lineas = File.ReadAllLines(Ruta);
                string[] lineasplit = null;

                if (lineas.Any())
                {
                    var okvalidaliena = true;

                    foreach (string linea in lineas)
                    {
                        if (linea != string.Empty)
                        {
                            //aca empiezo a guardar cada contenido del array en el list de elementos acorde a mi stock (llamo a los objeto elemento, les pongo lo que corresponde
                            // y luego es lo guardo en un metodo en la clase Empresa dentro de la lista Stock! voila
                            Element = new Elemento();
                            lineasplit = linea.Split(';');
                            int cantidad = 0;
                            if (!(lineasplit.Count() == 2) || !(lineasplit[0].Count() >= 2) // v9.5
                                                           || !(lineasplit[0].Remove(1).ToUpper() == "P")
                                                           || !(lineasplit[0].Remove(0, 1).Count() >= 1)
                                                           || !(lineasplit[0].Remove(0, 1).Count() < 6)
                                                           || !(int.TryParse(lineasplit[0].Remove(0, 1), out numprod))
                                                           || !(numprod >= 1)
                                                           || !(int.TryParse(lineasplit[1], out cantidad))
                                                           || !(cantidad > 0)
                                                           || !(cantidad < 100000))//Se puede mejorar este if como hice con "carga stock inicial".
                            {
                                okvalidaliena = false;
                                throw new Exception("Error en el formato del archivo enviado por Produccion. El proceso finalizo sin cargar stock. Verifique las cantidades y el formato.");
                            }
                        }
                    }//ACA TERMINA EL PRIMER FOREACH.
                    if (okvalidaliena == true) //RECIEN ACA GUARDA EL STOCK.
                    {
                        foreach (string linea in lineas) //TOME EL ARRAY LINEAS (QUE TENIA LO DE LA RUTA) Y LO SEPARA EN LINEAS DE TIPO STRING. RECORRE TODAS. 
                                                         //LO DE MAS ABAJO SIMPLEMENTE LO COPIE DEL DE ARRIBA SIN MODIFICAR, SALVO LA VARIABLE CANTIDAD. 
                                                         //CANTIDAD SALIA DEL IF QUE VALIDAVABA, AHORA SALE DIRECTO DEL SPLIT CON UN INT.PARSE.
                        {
                            Element = new Elemento();
                            lineasplit = linea.Split(';');

                            Element.prod.idprod = lineasplit[0];
                            Element.cantidad = int.Parse(lineasplit[1]);
                            listaproduccion.Add(Element);
                        }
                    }
                }//TODO LO QUE SE AGREGO SE PROBO Y FUNCIONA. BORRA STOCK Y PROBALO POR LAS DUDAS (CARGA POR LO MENOS UNO BUENO Y UNO MALO).
                else
                {
                    throw new Exception("El archivo esta vacio, el proceso finalizo sin completarse");
                }
            }
            //ConfirmacionProcesado.Items.Add(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " - Se lee archivo de produccion");

            //en el punto anterior chupe lo que tenia en el txt que viene de planta y guarde en una lista todo en memoria
            // a continuacion voy a buscar los elementos de la lista del punto anterior a ver si existe en mi stock actual, 
            // recorda que en el form load el stock actual siempre se carga en memoria, y abajo esta lista para ser recorrido,
            // busco si los elementos del punto anterior estan presentes en mi stock, si es asi, solo acumulo la cantidad, de lo contrario agrego una linea.

            foreach (Elemento elemprod in listaproduccion)
            {
                Elemento ElementoEncontrado = (Elemento)EmpresaInstanciada.stock.Find(x => x.prod.idprod == (elemprod.prod.idprod));
                if (ElementoEncontrado != null)
                {
                    ElementoEncontrado.cantidad = ElementoEncontrado.cantidad + elemprod.cantidad; // aca adiciona al stock los items que existen
                }
                else
                {
                    EmpresaInstanciada.GuardarStock(elemprod); // aca guarda items nuevos 
                }
            }
            GrabarStockTxt(); // aca vuelca al txt lo que tiene en memoria de la lista resultante actualizada del foreach anterior.
            ConfirmacionProcesado.Items.Add(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " - Se actualizo stock");
            ConfirmacionProcesado.SelectedIndex = ConfirmacionProcesado.Items.Count - 1;


        }

        private void GrabarStockTxt() /////////////////////////////// STOCK PASA A TXT /////////////////////////////////////
        {
            if (File.Exists(rutaarchivo))
            {
                if (EmpresaInstanciada.stock.Count > 0)
                {
                    using (StreamWriter sw = File.CreateText(rutaarchivo))
                    {
                        foreach (Elemento elementoarchivo in EmpresaInstanciada.stock)
                        {
                            sw.WriteLine(elementoarchivo.prod.idprod + ";" + elementoarchivo.cantidad);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Error", "No tiene informacion de stock");
                    ConfirmacionProcesado.Items.Add(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " - Error procesando archivo de stock");
                    ConfirmacionProcesado.SelectedIndex = ConfirmacionProcesado.Items.Count - 1;
                }
            }
            //ConfirmacionProcesado.Items.Add(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " - Se grabo el archivo stock");
            ConfirmacionProcesado.SelectedIndex = ConfirmacionProcesado.Items.Count - 1;
        }
        /// <summary>
        ///
        /// </summary>
        private void CargarStockInicial()
        {
            string[] lineas = null;
            Elemento Element;
            if (File.Exists(rutaarchivo))
            {
                var ok = true; //NUEVO.
                lineas = File.ReadAllLines(rutaarchivo);
                string[] lineasplit = null;
                foreach (string linea in lineas)
                {
                    if (linea != string.Empty)
                    {
                        int cantidad = 0;
                        int numprod;

                        lineasplit = linea.Split(';');
                        if (!(lineasplit.Count() == 2))
                        {
                            ok = false;
                            throw new Exception("Error en el formato del archivo de Stock. No se pudo procesar correctamente.");
                        }//NUEVO (MEJORADO).
                        if (!(lineasplit[0].Count() >= 2) || !(lineasplit[0].Remove(1).ToUpper() == "P") // v9.5
                                                          || !(lineasplit[0].Remove(0, 1).Count() >= 1)
                                                          || !(lineasplit[0].Remove(0, 1).Count() < 6)
                                                          || !(int.TryParse(lineasplit[0].Remove(0, 1), out numprod))
                                                          || !(numprod >= 1))
                        {
                            ok = false;
                            throw new Exception("Error en el formato del codigo de producto (" + lineasplit[0] + ") cargado en stock.");
                        }//NUEVO.
                        if (!(int.TryParse(lineasplit[1], out cantidad)) || !(cantidad >= 0))
                        {
                            ok = false;
                            throw new Exception("Error en la cantidad del producto " + lineasplit[0] + " de stock.");
                        }//NUEVO.
                    }
                }//Termina el FOREACH.
                if (ok == true)
                {
                    foreach (string linea in lineas)
                    {
                        lineasplit = linea.Split(';');
                        Element = new Elemento();

                        Element.prod.idprod = lineasplit[0];
                        Element.cantidad = int.Parse(lineasplit[1]);
                        EmpresaInstanciada.GuardarStock(Element);
                    }
                }//ESTO ES NUEVO. Similar a la carga de Stock desde produccion.
            }
            else
            {

                using (StreamWriter sw = File.CreateText(rutaarchivo))
                {
                    sw.Flush();

                }






            }


        }
        private void CargaDeListasHistoricas(tipodelista tipo)   /// corregir con ale este Metodo
        {

            string[] pedhistlineas = null;
            string[] lineadevolucion = null;
            if (tipo == tipodelista.listadevoluciones)
            {
                if (File.Exists(rutadevolucionesprocesadas))
                {
                    lineadevolucion = File.ReadAllLines(rutadevolucionesprocesadas);
                    foreach (string linea in lineadevolucion)
                    {
                        ListaDevolucionesHistorica.Add(linea);
                    }
                }
                else
                {


                    using (StreamWriter sw = File.CreateText(rutadevolucionesprocesadas))
                    {
                        sw.Flush();

                    }


                }

            }
            else
            {

                if (File.Exists(rutapedidohistorico))
                {
                    pedhistlineas = File.ReadAllLines(rutapedidohistorico);
                    foreach (string linea in pedhistlineas)
                    {
                        ListaPedidosTemporal.Add(linea);
                    }
                }
                else
                {

                    using (StreamWriter sw = File.CreateText(rutapedidohistorico))
                    {
                        sw.Flush();

                    }

                }
            }
        }




        public void CargarPedidosActualesPendientes() //aca se guarda en memo toda la lista pedidos
        {
            string[] pedidoslineas = null;
            Elemento Element;
            Pedido PedidoActual = new Pedido();
            if (File.Exists(rutapedidodiario)) // si se crashea el programa trata de levantar la lista pedido actual
            {
                string[] lineasplit = null;
                int lineacabecera = 1;
                pedidoslineas = File.ReadAllLines(rutapedidodiario);
                foreach (string linea in pedidoslineas)
                {
                    if (linea != string.Empty)
                    {
                        lineasplit = linea.Split(';');

                        if (lineasplit.Count() == 6)
                        {
                            if (lineacabecera != 1)
                            {
                                EmpresaInstanciada.GuardarPedido(PedidoActual);
                                //Cuando termino de cargar el pedido a la lista lo destruyo
                                PedidoActual = null;

                            }
                            else
                            {
                                lineacabecera = lineacabecera + 1;
                            }
                            //y Aca lo creo de vuelta cuando empiezo uno nuevo
                            PedidoActual = new Pedido();
                            PedidoActual.codigo = lineasplit[0];
                            PedidoActual.comercio.codigo = lineasplit[1];
                            PedidoActual.comercio.razonsocial = lineasplit[2];
                            PedidoActual.comercio.cuit = lineasplit[3];
                            PedidoActual.comercio.domicilio = lineasplit[4];
                            PedidoActual.EnviadoLogistica = Convert.ToBoolean(lineasplit[5]);

                        }
                        else if (lineasplit.Count() == 2)
                        {
                            int cantidad = 0;
                            if (int.TryParse(lineasplit[1], out cantidad))
                            {
                                Element = new Elemento();
                                Element.prod.idprod = lineasplit[0];
                                Element.cantidad = cantidad;
                                PedidoActual.GuardarPedido(Element);
                            }
                            else
                            {
                                throw new Exception("Error en el formato del archivo de Pedidos Pendientes,no se pudo procesar correctamente.");
                            }
                        }
                        else
                        {
                            throw new Exception("Error en el formato del archivo de Pedidos Pendientes,no se pudo procesar correctamente.");
                        }
                    }
                }

                if (PedidoActual.codigo != null)
                {
                    EmpresaInstanciada.GuardarPedido(PedidoActual);
                }
            }
            else
            {

                using (StreamWriter sw = File.CreateText(rutapedidodiario))
                {
                    sw.Flush();

                }

            }


        }
        private void FrmTP1_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Directory.Exists(enviologistica))
                {
                    Directory.CreateDirectory(enviologistica);
                }
                CargarDatosEmpresa();  //Verificado, no carga archivos de texto
                CargarStockInicial();  //Corregido
                ConfirmacionProcesado.Items.Add(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " - Se cargo el stock"); //Info relevante para el usuario
                CargaDeListasHistoricas(tipodelista.listapedidos); //Corregido
                //ConfirmacionProcesado.Items.Add(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " - Se cargo la tabla de codigos de pedidos diario");
                CargarPedidosActualesPendientes(); //Corregido segun pide profesor
                ConfirmacionProcesado.Items.Add(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " - Pedidos pendientes cargados en memoria");
                CargaDeListasHistoricas(tipodelista.listadevoluciones); //Corregido
                //ConfirmacionProcesado.Items.Add(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " - Se cargo la tabla de codigos de devoluciones historica");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + "Comuniquese con el administrador", "Fatal Error", MessageBoxButtons.OK);
                this.Close();

            }
        }

        private void CargarDatosEmpresa()
        {
            EmpresaInstanciada = new Empresa();
            EmpresaInstanciada.razonsocial = Program.RazonSocial;
            EmpresaInstanciada.cuit = Program.CUIT;
            EmpresaInstanciada.domicilio = Program.Domicilio;
        }
        private void btnPedidosTot_Click(object sender, EventArgs e)
        {

            string[] pedidoslineas = null;
            pedidoslineas = File.ReadAllLines(rutapedidodiario);
            if (pedidoslineas.Count() > 0)
            {
                ConfirmacionProcesado.Items.Add("---------------------------");
                foreach (string linea in pedidoslineas)
                {
                    ConfirmacionProcesado.Items.Add(linea);
                }
                ConfirmacionProcesado.Items.Add("---------------------------");
                ConfirmacionProcesado.SelectedIndex = ConfirmacionProcesado.Items.Count - 1;
            }
            else
            {
                ConfirmacionProcesado.Items.Add((DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " - Archivo de pedidos vacio"));
                ConfirmacionProcesado.SelectedIndex = ConfirmacionProcesado.Items.Count - 1;
            }
        }



        private void btnStock_Click(object sender, EventArgs e)
        {

            string[] stocklineas = null;

            stocklineas = File.ReadAllLines(rutaarchivo);
            if (stocklineas.Count() > 0)
            {
                ConfirmacionProcesado.Items.Add("---------------------------");
                foreach (string linea in stocklineas)
                {

                    ConfirmacionProcesado.Items.Add(linea);
                }
                ConfirmacionProcesado.Items.Add("---------------------------");
                ConfirmacionProcesado.SelectedIndex = ConfirmacionProcesado.Items.Count - 1;
            }
            else
            {
                ConfirmacionProcesado.Items.Add((DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " - Stock vacio"));
                ConfirmacionProcesado.SelectedIndex = ConfirmacionProcesado.Items.Count - 1;
            }
        }

        private void RdnEnvioLogistica_CheckedChanged(object sender, EventArgs e)
        {

            if (RdnEnvioLogistica.Checked)
            {
                btnExplorarArchivo.Enabled = false;
                txtRuta.Text = "";
            }
            else
            {
                btnExplorarArchivo.Enabled = true;
            }
        }

        private void RdnRecepcionPedido_CheckedChanged(object sender, EventArgs e)
        {
            txtRuta.Text = "";
        }

        private void rdnRecepcionStock_CheckedChanged(object sender, EventArgs e)
        {
            txtRuta.Text = "";
        }

        private void rdnRecepcionLogistica_CheckedChanged(object sender, EventArgs e)
        {
            txtRuta.Text = "";
        }
    } /// esto es GIT prueba
}

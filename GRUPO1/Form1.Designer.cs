namespace GRUPO1
{
    partial class FrmTP1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RdnEnvioLogistica = new System.Windows.Forms.RadioButton();
            this.rdnRecepcionStock = new System.Windows.Forms.RadioButton();
            this.rdnRecepcionLogistica = new System.Windows.Forms.RadioButton();
            this.RdnRecepcionPedido = new System.Windows.Forms.RadioButton();
            this.btnPedidosTot = new System.Windows.Forms.Button();
            this.btnStock = new System.Windows.Forms.Button();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.btnExplorarArchivo = new System.Windows.Forms.Button();
            this.btnProcesar = new System.Windows.Forms.Button();
            this.ConfirmacionProcesado = new System.Windows.Forms.ListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RdnEnvioLogistica);
            this.groupBox1.Controls.Add(this.rdnRecepcionStock);
            this.groupBox1.Controls.Add(this.rdnRecepcionLogistica);
            this.groupBox1.Controls.Add(this.RdnRecepcionPedido);
            this.groupBox1.Location = new System.Drawing.Point(61, 133);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(328, 180);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            // 
            // RdnEnvioLogistica
            // 
            this.RdnEnvioLogistica.AutoSize = true;
            this.RdnEnvioLogistica.Location = new System.Drawing.Point(17, 96);
            this.RdnEnvioLogistica.Margin = new System.Windows.Forms.Padding(4);
            this.RdnEnvioLogistica.Name = "RdnEnvioLogistica";
            this.RdnEnvioLogistica.Size = new System.Drawing.Size(131, 21);
            this.RdnEnvioLogistica.TabIndex = 6;
            this.RdnEnvioLogistica.Text = "Envio a logistica";
            this.RdnEnvioLogistica.UseVisualStyleBackColor = true;
            this.RdnEnvioLogistica.CheckedChanged += new System.EventHandler(this.RdnEnvioLogistica_CheckedChanged);
            // 
            // rdnRecepcionStock
            // 
            this.rdnRecepcionStock.AutoSize = true;
            this.rdnRecepcionStock.Location = new System.Drawing.Point(17, 19);
            this.rdnRecepcionStock.Margin = new System.Windows.Forms.Padding(4);
            this.rdnRecepcionStock.Name = "rdnRecepcionStock";
            this.rdnRecepcionStock.Size = new System.Drawing.Size(224, 21);
            this.rdnRecepcionStock.TabIndex = 5;
            this.rdnRecepcionStock.Text = "Recepcion linea de produccion";
            this.rdnRecepcionStock.UseVisualStyleBackColor = true;
            this.rdnRecepcionStock.CheckedChanged += new System.EventHandler(this.rdnRecepcionStock_CheckedChanged);
            // 
            // rdnRecepcionLogistica
            // 
            this.rdnRecepcionLogistica.AutoSize = true;
            this.rdnRecepcionLogistica.Location = new System.Drawing.Point(17, 135);
            this.rdnRecepcionLogistica.Margin = new System.Windows.Forms.Padding(4);
            this.rdnRecepcionLogistica.Name = "rdnRecepcionLogistica";
            this.rdnRecepcionLogistica.Size = new System.Drawing.Size(171, 21);
            this.rdnRecepcionLogistica.TabIndex = 4;
            this.rdnRecepcionLogistica.Text = "Recepcion de logistica";
            this.rdnRecepcionLogistica.UseVisualStyleBackColor = true;
            this.rdnRecepcionLogistica.CheckedChanged += new System.EventHandler(this.rdnRecepcionLogistica_CheckedChanged);
            // 
            // RdnRecepcionPedido
            // 
            this.RdnRecepcionPedido.AutoSize = true;
            this.RdnRecepcionPedido.Checked = true;
            this.RdnRecepcionPedido.Location = new System.Drawing.Point(17, 58);
            this.RdnRecepcionPedido.Margin = new System.Windows.Forms.Padding(4);
            this.RdnRecepcionPedido.Name = "RdnRecepcionPedido";
            this.RdnRecepcionPedido.Size = new System.Drawing.Size(144, 21);
            this.RdnRecepcionPedido.TabIndex = 2;
            this.RdnRecepcionPedido.TabStop = true;
            this.RdnRecepcionPedido.Text = "Recepcion Pedido";
            this.RdnRecepcionPedido.UseVisualStyleBackColor = true;
            this.RdnRecepcionPedido.CheckedChanged += new System.EventHandler(this.RdnRecepcionPedido_CheckedChanged);
            // 
            // btnPedidosTot
            // 
            this.btnPedidosTot.Location = new System.Drawing.Point(809, 334);
            this.btnPedidosTot.Name = "btnPedidosTot";
            this.btnPedidosTot.Size = new System.Drawing.Size(126, 43);
            this.btnPedidosTot.TabIndex = 23;
            this.btnPedidosTot.Text = "Pedidos totales acumulados";
            this.btnPedidosTot.UseVisualStyleBackColor = true;
            this.btnPedidosTot.Click += new System.EventHandler(this.btnPedidosTot_Click);
            // 
            // btnStock
            // 
            this.btnStock.Location = new System.Drawing.Point(495, 334);
            this.btnStock.Name = "btnStock";
            this.btnStock.Size = new System.Drawing.Size(131, 43);
            this.btnStock.TabIndex = 22;
            this.btnStock.Text = "Ver Stock Actual";
            this.btnStock.UseVisualStyleBackColor = true;
            this.btnStock.Click += new System.EventHandler(this.btnStock_Click);
            // 
            // txtRuta
            // 
            this.txtRuta.Enabled = false;
            this.txtRuta.Location = new System.Drawing.Point(154, 58);
            this.txtRuta.Margin = new System.Windows.Forms.Padding(4);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.Size = new System.Drawing.Size(312, 22);
            this.txtRuta.TabIndex = 21;
            // 
            // btnExplorarArchivo
            // 
            this.btnExplorarArchivo.Location = new System.Drawing.Point(483, 55);
            this.btnExplorarArchivo.Margin = new System.Windows.Forms.Padding(4);
            this.btnExplorarArchivo.Name = "btnExplorarArchivo";
            this.btnExplorarArchivo.Size = new System.Drawing.Size(37, 28);
            this.btnExplorarArchivo.TabIndex = 20;
            this.btnExplorarArchivo.Text = "...";
            this.btnExplorarArchivo.UseVisualStyleBackColor = true;
            this.btnExplorarArchivo.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnProcesar
            // 
            this.btnProcesar.Location = new System.Drawing.Point(641, 55);
            this.btnProcesar.Margin = new System.Windows.Forms.Padding(4);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(100, 28);
            this.btnProcesar.TabIndex = 19;
            this.btnProcesar.Text = "Procesar";
            this.btnProcesar.UseVisualStyleBackColor = true;
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);
            // 
            // ConfirmacionProcesado
            // 
            this.ConfirmacionProcesado.ForeColor = System.Drawing.Color.Red;
            this.ConfirmacionProcesado.FormattingEnabled = true;
            this.ConfirmacionProcesado.HorizontalScrollbar = true;
            this.ConfirmacionProcesado.ItemHeight = 16;
            this.ConfirmacionProcesado.Location = new System.Drawing.Point(446, 133);
            this.ConfirmacionProcesado.Margin = new System.Windows.Forms.Padding(4);
            this.ConfirmacionProcesado.Name = "ConfirmacionProcesado";
            this.ConfirmacionProcesado.ScrollAlwaysVisible = true;
            this.ConfirmacionProcesado.Size = new System.Drawing.Size(560, 180);
            this.ConfirmacionProcesado.TabIndex = 18;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "txt";
            this.openFileDialog1.FileName = "*.txt";
            this.openFileDialog1.InitialDirectory = "C:\\TP";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 17);
            this.label1.TabIndex = 24;
            this.label1.Text = "Ruta del Archivo:";
            // 
            // FrmTP1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1065, 432);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnPedidosTot);
            this.Controls.Add(this.btnStock);
            this.Controls.Add(this.txtRuta);
            this.Controls.Add(this.btnExplorarArchivo);
            this.Controls.Add(this.btnProcesar);
            this.Controls.Add(this.ConfirmacionProcesado);
            this.Controls.Add(this.label1);
            this.Name = "FrmTP1";
            this.Text = "Aplicacion industria";
            this.Load += new System.EventHandler(this.FrmTP1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton RdnEnvioLogistica;
        private System.Windows.Forms.RadioButton rdnRecepcionStock;
        private System.Windows.Forms.RadioButton rdnRecepcionLogistica;
        private System.Windows.Forms.RadioButton RdnRecepcionPedido;
        private System.Windows.Forms.Button btnPedidosTot;
        private System.Windows.Forms.Button btnStock;
        private System.Windows.Forms.TextBox txtRuta;
        private System.Windows.Forms.Button btnExplorarArchivo;
        private System.Windows.Forms.Button btnProcesar;
        private System.Windows.Forms.ListBox ConfirmacionProcesado;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label1;
    }
}


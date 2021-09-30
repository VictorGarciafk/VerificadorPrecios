using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Verificador_Precios
{
    public partial class Form1 : Form
    {
        private int segundos = 0;
        Boolean error = false;
        Boolean correcto = false;

        private String codigo = "";
        public Form1()
        {
            InitializeComponent();
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
            pictureBox5.Visible = false;
            pictureBox6.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox3.Size = new Size(500, 500);
            pictureBox4.Size = new Size(500, 500);
            pictureBox6.Size = new Size(350, 250);
            pictureBox5.Size = new Size(585, 575);
            label1.Location = new Point(this.Width / 2 - label1.Width / 2, this.Height / 2);
            label2.Location = new Point(this.Width / 8 + pictureBox3.Width, this.Height / 2 - pictureBox3.Height / 2);
            label3.Location = new Point(this.Width / 2 - label3.Width / 2, this.Height/8);
            label4.Location = new Point(this.Width / 2 - label4.Width / 2, this.Height / 8);
            label5.Location = new Point(this.Width / 8 + pictureBox5.Width, this.Height / 2 - pictureBox5.Height / 4);
            label6.Location = new Point(0, 0);
            pictureBox1.Location = new Point(this.Width / 2 - pictureBox1.Width / 2, this.Height / 4 - pictureBox1.Height / 2);
            pictureBox2.Location = new Point(this.Width / 2 - pictureBox2.Width/2, (this.Height * 3 / 4) - pictureBox2.Height / 2);
            pictureBox3.Location = new Point(this.Width / 8, this.Height / 2 - pictureBox3.Height / 2);
            pictureBox4.Location = new Point(this.Width / 2 - pictureBox4.Width / 2, this.Height / 2 - pictureBox4.Height / 2);
            pictureBox5.Location = new Point(this.Width / 8, pictureBox5.Height / 2);
            pictureBox6.Location = new Point(this.Width - pictureBox6.Width, this.Height / 8);
            pictureBox7.Location = new Point(this.Width / 2 - pictureBox7.Width / 2, this.Height - pictureBox7.Height);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //MessageBox.Show("vamos a buscar el producto "+codigo);
                try
                {
                    pictureBox1.Visible = false;
                    pictureBox2.Visible = false;
                    pictureBox7.Visible = false;
                    label1.Visible = false;
                    label6.Visible = false;
                    pictureBox4.Visible = true;
                    MySqlConnection servidor;
                    servidor = new MySqlConnection("server = 127.0.0.1; user = root; database = verificador_precios; SSL Mode = None; ");
                    servidor.Open();
                    string query = "SELECT producto_nombre, producto_precio, producto_stock, producto_imagen FROM productos WHERE producto_codigo =" + codigo + ";";
                    //MessageBox.Show(query);
                    MySqlCommand consulta;
                    consulta = new MySqlCommand(query, servidor);
                    MySqlDataReader resultado = consulta.ExecuteReader();
                    if (resultado.HasRows)
                    {
                        encontrado(resultado);
                    }
                    else
                    {
                        noEncontrado();
                    }
                }
                catch (Exception x)
                {
                    noEncontrado();
                }
                codigo = "";
            }
            else
            {
                codigo += e.KeyChar;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            segundos++;

            if (segundos == 4)
            {
                timer1.Enabled = false;
                pictureBox1.Visible = true;
                pictureBox2.Visible = true;
                pictureBox3.Visible = false;
                pictureBox5.Visible = false;
                pictureBox6.Visible = false;
                pictureBox7.Visible = true;
                label1.Visible = true;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = true;
                label2.Text = "";

                error = false;
                correcto = false;
            }
        }

        public void encontrado(MySqlDataReader resultado1)
        {
            correcto = true;
            if (error == true)
            {
                pictureBox5.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                error = false;
            }
            resultado1.Read();
            //MessageBox.Show(resultado.GetString(1));

            pictureBox3.Visible = true;
            pictureBox4.Visible = false;
            label2.Visible = true;
            label3.Visible = true;
            pictureBox6.Visible = true;
            label2.Text = resultado1.GetString(0) + Environment.NewLine + "Precio:" + resultado1.GetString(1) +
                Environment.NewLine + "Stock:" + resultado1.GetString(2);
            pictureBox3.ImageLocation = resultado1.GetString(3);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            segundos = 0;
            timer1.Enabled = true;
        }

        public void noEncontrado()
        {
            error = true;
            if (correcto == true)
            {
                pictureBox3.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                pictureBox6.Visible = false;
            }
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox4.Visible = false;
            pictureBox5.Visible = true;
            pictureBox7.Visible = false;
            label5.Visible = true;
            label6.Visible = false;
            label1.Visible = false;
            label4.Visible = true;

            segundos = 0;
            timer1.Enabled = true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cargarDatosCSVPostgres
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }


        private void procesar()
        {
            string[] arreglo = textBox1.Text.Replace(" ","").Split(',');
            for(int i =0; i < arreglo.Length; i++)
            {
                txtQueries.AppendText("insert into " + textBox2.Text + " (necesidad,sinonimo) values ('" 
                    + textBox3.Text + "','" + textBox4.Text+ " " + changeUTF8( arreglo[i]) + "');\n");
            }

        }

        private string changeUTF8(string x)
        {
            return x.Replace('á', 'a').Replace('é', 'e').Replace('í', 'i').Replace('ó', 'o').Replace('ú', 'u').Replace('ñ', 'n')
                .Replace('Á', 'A').Replace('É', 'E').Replace('Í', 'I').Replace('Ó', 'O').Replace('Ú', 'U').Replace('Ñ', 'N').ToLower();
        }
        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void GO_Click(object sender, EventArgs e)
        {
            procesar();
        }
    }
}

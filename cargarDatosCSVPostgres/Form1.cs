using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cargarDatosCSVPostgres
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        public string createScript(string[] campos)
        {
            string lista = "create table public." + txtTabla.Text + " ( \n" +
                "id SERIAL PRIMARY KEY,\n";
            for (int i = 0; i < campos.Length; i++)
            {
                lista = lista + campos[i].Replace("'", "") + " text null ,\n";
            }
            lista = lista.Remove(lista.Length - 2);
            return  lista + " );\n ";
        }
        public string encabezado()
        {
            return "insert into " + txtTabla.Text + " " ;
        }

        public string campos(string[] campos)
        {
            //campos de encabezado
            string lista = "";
            for(int i =0; i < campos.Length; i++)
            {
                lista = lista +   campos[i].Replace("'","") + ",";
            }
            lista = lista.Remove(lista.Length - 1);
            return encabezado() + " ( " + lista + " ) ";
        }

        public string valores(string[] campos)
        {
            string lista = "";
            for (int i = 0; i < campos.Length; i++)
            {
                lista = lista + campos[i] + ",";
            }
            lista = lista.Remove(lista.Length - 1);
            return " values ( " + lista + " ); ";
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private string modificaStr(string token)
        {
            
            if (token.IndexOf('\'') > 0) {
                token = token.Replace("'", "''");
            }
            return "'" + token + "'";
        }
        private void cargaCSV(string archivo)
        {
            using (var reader = new StreamReader(archivo, Encoding.GetEncoding("iso-8859-15")))
            {
                int c = 0;
                string encabezado = null;
                List<string[]> listA = new List<string[]>();
                List<string[]> listB = new List<string[]>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line.Length > 0)
                    {
                        
                        var values = line.Split(Char.Parse(textBox2.Text.ToString()));
                        
                        for(int i = 0; i < values.Length; i++)
                        {
                            values[i] = modificaStr(values[i]);
                            Console.WriteLine(values[i]);
                        }
                        
                        if (c == 0)
                        {
                            listA.Add(values);
                            encabezado = campos(values);
                            string cStr = createScript(values);
                            Console.WriteLine(cStr);
                            txtCreate.AppendText(cStr);

                        }

                        else
                        {
                            listB.Add(values);
                            txtQueries.AppendText(encabezado + valores(values) + "\n");
                        }
                        c++;
                        
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();
            string Archivo = openFileDialog1.FileName;
            if (Archivo.Length > 0)
                cargaCSV(Archivo);
            else
                MessageBox.Show("ingrese nombre de la tabla y delimitador de archivo csv");
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Lab1CoreDavidAlvarado
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            List<string> lista = sql.Llenar_combo("PAIS");
            // Use for loop.
            for (int i = 0; i < lista.Count; i++)
            {
                string valor = lista[i];
                comboBox1.Items.Add(valor);
                comboBox2.Items.Add(valor);
            }
        }

        Conexion sql = new Conexion();
        int mostrando = 0;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            List<string>  lista = sql.Show_table("PAIS");
            listBox1.Items.Clear();
            for (int i = 0; i < lista.Count; i++)
            {
                string valor = lista[i];
                listBox1.Items.Add(valor);
            }
            mostrando = 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            List<string> lista = sql.Show_table("DIVISA");
            // Use for loop.
            for (int i = 0; i < lista.Count; i++)
            {
                string valor = lista[i];
                listBox1.Items.Add(valor);
            }
            mostrando = 2;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (listBox1.SelectedItem == null)
            {
                return;
            }
            string text = listBox1.SelectedItem.ToString();
            
            text = listBox1.SelectedItem.ToString();
            string[] split = text.Split(new Char[] { '|' });
            string codigo = split[0];
            string nombre = split[1];
            string divisa = split[2];
            if (mostrando == 1)
            {

                txt_cod_pais.Text = codigo;
                txt_nombre_pais.Text = nombre;
                txt_div_pais.Text = divisa;

            }
            else if (mostrando == 2)
            {
                txt_cod_div.Text = codigo;
                txt_nombre_div.Text = nombre;
                txt_valor_div.Text = divisa;
            }

        }

        private void labeldiv_Click(object sender, EventArgs e)
        {

        }

        private void btn_agregar_div_Click(object sender, EventArgs e)
        {
            string cod = txt_cod_div.Text;
            string nombre = txt_nombre_div.Text;
            string valor = txt_valor_div.Text;

            if(String.IsNullOrEmpty(cod) || String.IsNullOrEmpty(nombre) || String.IsNullOrEmpty(valor))
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("Por Favor Complete los datos para Agregar.");
                mostrando = 0;
            }
            else
            {
                if (sql.Agregar("DIVISA", cod, nombre, valor))
                {

                    listBox1.Items.Clear();
                    listBox1.Items.Add(sql.Buscar("DIVISA", nombre));
                    mostrando = 2;
                    
                }
            }

            
        }

        private void btn_eliminar_div_Click(object sender, EventArgs e)
        {
            string cod = txt_cod_div.Text;
            string prueba = "'" + cod + "'";
            if (String.IsNullOrEmpty(cod) || sql.BuscarPruebas("PAIS", "divisa", prueba))
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("No se puede Borrar, No se encuentra o Está relacionado");
                mostrando = 0;
            }
            else
            {
                if(sql.Eliminar(cod, "DIVISA"))
                {
                    listBox1.Items.Clear();
                    listBox1.Items.Add("Borrado Exitoso");
                    mostrando = 0;
                }
            }
            
        }

        private void btn_buscar_div_Click(object sender, EventArgs e)
        {
            string nombre = txt_nombre_div.Text;

            if(String.IsNullOrEmpty(nombre))
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("Inserte un nombre para buscar");
                mostrando = 0;
            }
            else
            {
                string resultado = sql.Buscar("DIVISA", nombre);
                if (String.IsNullOrEmpty(resultado))
                {
                    listBox1.Items.Clear();
                    listBox1.Items.Add("NO SE ENCONTRÓ");
                    mostrando = 0;
                }
                else
                {
                    listBox1.Items.Clear();
                    listBox1.Items.Add(resultado);
                    mostrando = 2;
                }
                
            }
        }

        private void btn_agregar_pais_Click(object sender, EventArgs e)
        {
            string cod = txt_cod_pais.Text;
            string nombre = txt_nombre_pais.Text;
            string valor = txt_div_pais.Text;

            if (String.IsNullOrEmpty(cod) || String.IsNullOrEmpty(nombre) || String.IsNullOrEmpty(valor))
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("Por Favor Complete los datos para Agregar.");
                mostrando = 0;
            }
            else
            {
                if (sql.Agregar("PAIS", cod, nombre, valor))
                {

                    listBox1.Items.Clear();
                    listBox1.Items.Add(sql.Buscar("PAIS", nombre));
                    mostrando = 1;

                }
                else
                {
                    listBox1.Items.Clear();
                    listBox1.Items.Add("No se pudo Agregar Datos inválidos...");
                    mostrando = 0;
                }
            }
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            List<string> lista = sql.Llenar_combo("PAIS");
            // Use for loop.
            for (int i = 0; i < lista.Count; i++)
            {
                string valor1 = lista[i];
                comboBox1.Items.Add(valor1);
                comboBox2.Items.Add(valor1);
            }
        }

        private void btn_eliminar_pais_Click(object sender, EventArgs e)
        {
            string cod = txt_cod_pais.Text;
            if (String.IsNullOrEmpty(cod))
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("Por Favor Ingrese Código Para Borrar");
                mostrando = 0;
            }
            else
            {
                if (sql.Eliminar(cod, "PAIS"))
                {
                    listBox1.Items.Clear();
                    listBox1.Items.Add("Borrado Exitoso");
                    mostrando = 0;
                }
            }
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            List<string> lista = sql.Llenar_combo("PAIS");
            // Use for loop.
            for (int i = 0; i < lista.Count; i++)
            {
                string valor = lista[i];
                comboBox1.Items.Add(valor);
                comboBox2.Items.Add(valor);
            }
        }

        private void btn_buscar_pais_Click(object sender, EventArgs e)
        {
            string nombre = txt_nombre_pais.Text;

            if (String.IsNullOrEmpty(nombre))
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("Inserte un nombre para buscar");
                mostrando = 0;
            }
            else
            {
                string resultado = sql.Buscar("PAIS", nombre);
                if (String.IsNullOrEmpty(resultado))
                {
                    listBox1.Items.Clear();
                    listBox1.Items.Add("NO SE ENCONTRÓ");
                    mostrando = 0;
                }
                else
                {
                    listBox1.Items.Clear();
                    listBox1.Items.Add(resultado);
                    mostrando = 1;
                }

            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("Faltan Datos");
                mostrando = 0;
                return;
            }
            string cantidad = txt_cantidad.Text;
            string pais1 = comboBox1.SelectedItem.ToString();
            string pais2 = comboBox2.SelectedItem.ToString();

            if(String.IsNullOrEmpty(cantidad))
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("Faltan Datos");
                mostrando = 0;
            }
            else
            {
                string resul = sql.Convertir(pais1, pais2, cantidad);
                string[] split = resul.Split(new Char[] { ',' });
                string resultado = split[0];
                string conCosto = split[1];
                string resulSin = split[2];
                string conCostoSin = split[3];
                txt_resul.Text = resultado;
                txt_resul_costo.Text = conCosto;
                txt_conv_sin.Text = resulSin;
                txt_cost_sin.Text = conCostoSin;
            }


            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string cod = txt_cod_div.Text;
            string nombre = txt_nombre_div.Text;
            string valor = txt_valor_div.Text;

            if (String.IsNullOrEmpty(cod) || String.IsNullOrEmpty(nombre) || String.IsNullOrEmpty(valor))
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("Por Favor Complete los datos para Actualizar");
                mostrando = 0;
            }
            else
            {
                if (sql.Actualizar(cod, nombre, valor, "DIVISA"))
                {
                    listBox1.Items.Clear();
                    listBox1.Items.Add("Se Actualizó correctamente...");
                    mostrando = 0;
                }else
                {
                    listBox1.Items.Clear();
                    listBox1.Items.Add("NO Se Actualizó correctamente...");
                    mostrando = 0;
                }

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string cod = txt_cod_pais.Text;
            string nombre = txt_nombre_pais.Text;
            string valor = txt_div_pais.Text;

            if (String.IsNullOrEmpty(cod) || String.IsNullOrEmpty(nombre) || String.IsNullOrEmpty(valor))
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("Por Favor Complete los datos para Actualizar");
                mostrando = 0;
            }
            else
            {
                if (sql.Actualizar(cod, nombre, valor, "PAIS"))
                {
                    listBox1.Items.Clear();
                    listBox1.Items.Add("Se Actualizó correctamente...");
                    mostrando = 0;
                }
                else
                {
                    listBox1.Items.Clear();
                    listBox1.Items.Add("NO Se Actualizó correctamente...");
                    mostrando = 0;
                }

            }
        }

        private void txt_cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            comboBox2_SelectedIndexChanged(sender, e);
        }
    }
}

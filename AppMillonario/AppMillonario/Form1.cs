using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppMillonario
{
    public partial class Form1 : Form
    {
        public string nombre;
        public int edad;

        public Form1()
        {
            InitializeComponent();
        }
        #region Metodo Personalizado
        private bool Validar()
        {
            if (edad < 15)
            {
                return false;
            }
            return true;
        }
        private void Mensaje(string texto)
        {
            MessageBox.Show(texto, "Aviso ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion
        private void btnJugar_Click(object sender, EventArgs e)
        {
            try
            {
                nombre = txbNombre.Text;
                edad = Convert.ToUInt16(txbEdad.Text);
                if (Validar())
                {
                    Program.form1.Hide();

                    frmRonda frm2 = new frmRonda(nombre, edad);
                    frm2.Show();
                }
                else
                {
                    Mensaje("Edad ingresada no valida.\nLos particitantes deben ser mayores de 15 años");
                }
            }
            catch (Exception ex)
            {

                Mensaje("Ingrese un nombre y una edad valida");
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

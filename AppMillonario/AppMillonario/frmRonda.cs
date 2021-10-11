using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using libBDPreguntas;
using libBDUsuarios;

namespace AppMillonario
{
    public partial class frmRonda : Form
    {
        string rCorrecta;
        int ronda = 1;
        int estado = 0;
        string nombre = string.Empty;
        int edad = 0;

        public frmRonda(string nombre, int edad)
        {
            InitializeComponent();
            buscarPregunta();
            this.radR1.Checked = false;
            this.nombre = nombre;
            this.edad = edad;
            this.edad = edad;
        }
        #region Metodos Personalizados
        private void buscarPregunta()
        {
            try
            {
                clsPreguntas objpreguntas = new clsPreguntas();
                objpreguntas.ronda = ronda;

                if (!objpreguntas.leerArchivo())
                {
                    Mensaje(objpreguntas.Error);
                    objpreguntas = null;
                    return;
                }

                this.lblPregunta.Text = objpreguntas.Pregunta;
                this.radR1.Text = objpreguntas.R1;
                this.radR2.Text = objpreguntas.R2;
                this.radR3.Text = objpreguntas.R3;
                this.radR4.Text = objpreguntas.R4;
                this.rCorrecta = objpreguntas.Verdadera;

            }
            catch (Exception ex)
            {
                Mensaje(ex.Message);
                throw;
            }
        }

        private void limpiar()
        {
            this.btnRetiro.Visible = true;
            this.lblIncorrecto.Visible = false;
            this.lblCorrecto.Visible = false;
            this.btnUltimaPalabra.Visible = false;
            this.btnFinalizar.Visible = false;
            this.btnSigPregunta.Visible = false;
        }


        private void Mensaje(string texto)
        {
            MessageBox.Show(texto, "Aviso ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool ValPregunta()
        {
            if (radR1.Checked == true && radR1.Text == rCorrecta)
            {
                radR1.Checked = false;
                return true;
            }
            if (radR2.Checked == true && radR2.Text == rCorrecta)
            {
                radR2.Checked = false;
                return true;
            }
            if (radR3.Checked == true && radR3.Text == rCorrecta)
            {
                radR3.Checked = false;
                return true;
            }
            if (radR4.Checked == true && radR4.Text == rCorrecta)
            {
                radR4.Checked = false;
                return true;
            }
            return false;
        }

        #endregion

        private void btnSigPregunta_Click(object sender, EventArgs e)
        {

            limpiar();
            buscarPregunta();
            this.btnUltimaPalabra.Visible = true;

            if (ronda == 2)
            {
                this.lblV2.BackColor = Color.FromArgb(255, 255, 128);
                this.lblV2.ForeColor = Color.Black;
                this.lblV1.BackColor = Color.CornflowerBlue;
                this.lblV1.ForeColor = Color.White;
            }
            if (ronda == 3)
            {
                this.lblV3.BackColor = Color.FromArgb(255, 255, 128);
                this.lblV3.ForeColor = Color.Black;
                this.lblV2.BackColor = Color.CornflowerBlue;
                this.lblV2.ForeColor = Color.White;
            }
            if (ronda == 4)
            {
                this.lblV4.BackColor = Color.FromArgb(255, 255, 128);
                this.lblV4.ForeColor = Color.Black;
                this.lblV3.BackColor = Color.CornflowerBlue;
                this.lblV3.ForeColor = Color.White;
            }
            if (ronda == 5)
            {
                this.lblV5.BackColor = Color.FromArgb(255, 255, 128);
                this.lblV5.ForeColor = Color.Black;
                this.lblV4.BackColor = Color.CornflowerBlue;
                this.lblV4.ForeColor = Color.White;
            }
        }

        private void btnUltimaPalabra_Click(object sender, EventArgs e)
        {
            int aux1 = (ronda == 1) ? 100000 : (ronda == 2) ? 200000 : (ronda == 3) ? 400000 : (ronda == 4) ? 800000 : 1600000;

            if (!ValPregunta())
            {
                this.lblIncorrecto.Visible = true;
                this.lblAcumuado.Text = "0";
                this.btnFinalizar.Visible = true;
                this.btnUltimaPalabra.Visible = false;

                estado = 2;
            }
            else
            {
                if (ronda != 5)
                {
                    this.lblCorrecto.Visible = true;
                    this.btnSigPregunta.Visible = true;
                    int aux2 = Convert.ToInt32(this.lblAcumuado.Text);
                    aux2 = aux2 + aux1;
                    this.lblAcumuado.Text = Convert.ToString(aux2);
                    ronda++;
                    this.btnUltimaPalabra.Visible = false;
                    this.btnSigPregunta.Visible = true;
                    this.btnRetiro.Visible = true;
                    this.btnRetiro.Visible = false;

                }
                else
                {
                    int aux2 = Convert.ToInt32(this.lblAcumuado.Text);
                    aux2 = aux2 + aux1;
                    this.lblAcumuado.Text = Convert.ToString(aux2);
                    Mensaje("Felicitaciones ha terminado el juego\n Valor acumulado: $" + this.lblAcumuado.Text);
                    this.btnFinalizar.Visible = true;
                    estado = 1;
                }


            }

        }

        private void btnRetiro_Click(object sender, EventArgs e)
        {
            Mensaje("Gacias por particitar su premio acumulado es $" + this.lblAcumuado.Text);
            limpiar();
            this.btnFinalizar.Visible = true;
            estado = 3;
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            try
            {
               
                clsBDUsuarios usuario = new clsBDUsuarios();
                usuario.Nombre = nombre;
                usuario.Edad = edad;
                usuario.Estado = estado;
                usuario.Ronda = ronda;
                usuario.Valor = Convert.ToInt32(lblAcumuado.Text);
                if (!usuario.guardarBd())
                {
                    Mensaje(usuario.Error);
                }

                Form1 obj = new Form1();
                obj.Show();
                this.Close();
            }
            catch (Exception ex)
            {

                Mensaje(Convert.ToString(ex));
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}


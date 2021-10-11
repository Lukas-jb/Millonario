using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace libBDUsuarios
{
    public class clsBDUsuarios
    {
        #region Atributos - Propiedades

        private int intId;
        private string strNombre;
        private int intEdad;
        private int intRonda;
        private int intValor;
        private int intEstado;
        private DateTime dtFecha;
        private string strError;

        public int id
        {
            set { intId = value; }
        }
        public string Nombre
        {
            set { strNombre = value; }
        }
        public int Edad
        {
            set { intEdad = value; }
        }
        public int Ronda
        {
            set { intRonda = value; }
        }
        public int Valor
        {
            set { intValor = value; }
        }
        public int Estado
        {
            set { intEstado = value; }
        }
        public DateTime Fecha
        {
            set { dtFecha = value; }
        }
        public string Error
        {
            get { return strError; }
        }

        #endregion

        #region Constructor
        public clsBDUsuarios()
        {
            this.intId = 0;
            this.strNombre = string.Empty;
            this.intEdad = 0;
            this.intRonda = 0;
            this.intValor = 0;
            this.intEstado = 0;
            this.strError = string.Empty;
        }
        #endregion

        #region Metodos Publicos
        public bool guardarBd()
        {
            try
            {
                if (!Validar())
                    return false;

                if (!Escribir())
                    return false;

                return true;

            }
            catch (Exception ex)
            {

                strError = "Error: " + ex.Message;
                return false;
            }


        }
        #endregion

        #region Metodos Privados

        private bool Validar()
        {
            if (string.IsNullOrEmpty(strNombre))
            {
                strError = "Nombre ingresado no valido.";
                return false;
            }
            if (intEdad < 15)
            {
                strError = "Edad ingresada no valida.";
                return false;
            }
            if (intRonda < 1 && intRonda > 5)
            {
                strError = "Ronda no valida.";
                return false;
            }
            if (intValor < 0)
            {
                strError = "Valor del Premio no valido.";
                return false;
            }
            if (intEstado < 1 && intEstado > 3)
            {
                strError = "Estado no valido.";
                return false;
            }
            return true;
        }

        private bool Escribir()
        {
            try
            {
                //Id | Fecha | Nombre | Edad | Ronda |Estado |Premio
                //Estados : 1- Ganador, 2-Perdedor,3-Retirado

                string strpath = AppDomain.CurrentDomain.BaseDirectory + @"BD_ Usuario.txt";
                int cantidad = 0;
                cantidad = File.ReadAllLines(strpath).Length;

                if (cantidad == 0)
                {
                    intId = 1;
                }
                else if (cantidad > 0)
                {
                    intId = cantidad + 1;
                }

                StreamWriter swEscribir = File.AppendText("BD_ Usuario.txt");
                dtFecha = DateTime.Now.Date;
                string cadena = intId.ToString() + "|" + dtFecha.ToString("dd-MM-yyyy") + "|" + strNombre + "|" + intEdad.ToString() + "|" + intRonda.ToString() + "|" + intEstado + "|" + intValor;
                swEscribir.WriteLine(cadena);
                swEscribir.Close();

                return true;
            }
            catch (Exception ex)
            {
                strError = "Error: " + ex.Message;
                return false;
            }
        }


        #endregion
    }
}

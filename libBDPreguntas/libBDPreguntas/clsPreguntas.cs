using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libBDPreguntas
{
    public class clsPreguntas
    {
        #region Atributos - Propiedades
        private int intCodigo;
        private int intTipo;
        private string strPregunta;
        private string strR1;
        private string strR2;
        private string strR3;
        private string strR4;
        private string strRV;
        private string strError;
        private int intRonda;


        public int codigo
        {
            // get { return intCodigo; }
            set { intCodigo = value; }
        }
        public int ronda
        {
            set { intRonda = value; }
        }
        public int tipo
        {
            get { return intTipo; }
        }

        public string Pregunta
        {
            get { return strPregunta; }
        }
        public string R1
        {
            get { return strR1; }
        }
        public string R2
        {
            get { return strR2; }
        }
        public string R3
        {
            get { return strR3; }
        }
        public string R4
        {
            get { return strR4; }
        }
        public string Verdadera
        {
            get { return strRV; }
        }
        public string Error
        {
            get { return strError; }
        }

        #endregion

        #region Constructor
        public clsPreguntas()
        {
            intCodigo = 0;
            intTipo = 0; ;
            strPregunta = string.Empty;
            strR1 = string.Empty;
            strR2 = string.Empty;
            strR3 = string.Empty;
            strR4 = string.Empty;
            strRV = string.Empty;
            strError = string.Empty;
            intRonda = 0; ;
        }


        #endregion

        #region Metodos Publicos

        #endregion

        #region Metodos Privados
        public bool leerArchivo()
        {
            try


            {
                Random aleatorio = new Random();
                intCodigo = (intRonda == 1) ? aleatorio.Next(1, 5) : (intRonda == 2) ? aleatorio.Next(6, 10) : (intRonda == 3) ? aleatorio.Next(11, 15) : (intRonda == 4) ? aleatorio.Next(16, 20) : (intRonda == 5) ? aleatorio.Next(21, 25) : 0;
                //intCodigo = Convert.ToInt16(aleatorio);

                //@ hace que \ sea \\ para evitar los caracteres de escape
                string strPath = AppDomain.CurrentDomain.BaseDirectory + @"BD_Preguntas.txt";
                int intCant = 0; // Para la cantidad de líneas que tiene el archivo
                string strLinea = string.Empty; // Para la línea leída del archivo /*captura la linea
                string[] vectorLinea; // Vector para almacenar la línea del archivo
                string strCodigo = null;
                intCant = File.ReadAllLines(strPath).Length; // Lee la cantidad de líneas que tiene el archivo
                if (intCant <= 0)
                {
                    strError = "Sin registros";
                    return false;
                }
                StreamReader Archivo = new StreamReader(@strPath); // Crear objeto para leer el archivo //Streamwriter escribir
                while ((strLinea = Archivo.ReadLine()) != null) // Leer línea * línea el archivo

                {
                    vectorLinea = strLinea.Split('|');//corta cuando encuentre:, pone la informacion en cada espacio del vector
                    strCodigo = vectorLinea[0]; //Nombre Dato -> clave primaria
                    if (strCodigo == intCodigo.ToString())
                    {
                        intTipo = Convert.ToInt16(vectorLinea[1]);
                        strPregunta = Convert.ToString(vectorLinea[2]);
                        strR1 = Convert.ToString(vectorLinea[3]); 
                        strR2 = Convert.ToString(vectorLinea[4]); 
                        strR3 = Convert.ToString(vectorLinea[5]); 
                        strR4 = Convert.ToString(vectorLinea[6]);
                        strRV = Convert.ToString(vectorLinea[7]); 


                        break;
                    }
                }
                Archivo.Close();//cerrar el archivo
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }
        #endregion
    }
}

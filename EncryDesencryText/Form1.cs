using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncryDesencryText
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        //Encryptar
        public byte[] Clave = Encoding.ASCII.GetBytes("Tu Password");
        public byte[] IV = Encoding.ASCII.GetBytes("harlericho.86hAES");
        public string EncriptaMD5(string Cadena)
        {

            byte[] inputBytes = Encoding.ASCII.GetBytes(Cadena);
            byte[] encripted;
            RijndaelManaged cripto = new RijndaelManaged();
            using (MemoryStream ms = new MemoryStream(inputBytes.Length))
            {
                using (CryptoStream objCryptoStream = new CryptoStream(ms, cripto.CreateEncryptor(Clave, IV), CryptoStreamMode.Write))
                {
                    objCryptoStream.Write(inputBytes, 0, inputBytes.Length);
                    objCryptoStream.FlushFinalBlock();
                    objCryptoStream.Close();
                }
                encripted = ms.ToArray();
            }
            return Convert.ToBase64String(encripted);
        }

        //Desencrytar
        public string DesencriptaMD5(string Cadena)
        {
            byte[] inputBytes = Convert.FromBase64String(Cadena);
            byte[] resultBytes = new byte[inputBytes.Length];
            string textoLimpio = String.Empty;
            RijndaelManaged cripto = new RijndaelManaged();
            using (MemoryStream ms = new MemoryStream(inputBytes))
            {
                using (CryptoStream objCryptoStream = new CryptoStream(ms, cripto.CreateDecryptor(Clave, IV), CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(objCryptoStream, true))
                    {
                        textoLimpio = sr.ReadToEnd();
                    }
                }
            }
            return textoLimpio;
        }


        private void btnGenerar_Click(object sender, EventArgs e)
        {
            if (!txtTexto.Text.Equals(""))
            {
                txtEncriptacion.Text = EncriptaMD5(txtTexto.Text);
                txtDesencriptar.Text = DesencriptaMD5(txtEncriptacion.Text);
            }
            else
            {
                string message = "Campo texto esta vacio";
                string title = "Aviso";
                MessageBox.Show(message, title);
                txtTexto.Focus();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtEncriptacion.Text = "";
            txtTexto.Text = "";
            txtDesencriptar.Text = "";
        }
    }
}

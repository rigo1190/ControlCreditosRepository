﻿using BusinessLogicLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SCC.Formas
{
    public partial class Hash : System.Web.UI.Page
    {
        public string clave = "3ncript4d4"; // Clave de cifrado.
        private UnitOfWork uow;
        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();
        }

        protected void btnDesencriptar_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Value;
            string passwordE = string.Empty;
            Usuario user = null;

            if (!login.Equals(string.Empty))
                user = uow.UsuarioBL.Get(u => u.Login == login).FirstOrDefault();

            if (user != null)
                passwordE = user.Password;
            else
            {
                if (!txtPasswordE.Value.Equals(string.Empty))
                    passwordE = txtPasswordE.Value;
            }

            if (!passwordE.Equals(string.Empty))
                txtPasswordD.Value = Desencriptar(passwordE);
        }

        private string Desencriptar(string cadena)
        {
            byte[] llave;

            byte[] arreglo = Convert.FromBase64String(cadena); // Arreglo donde guardaremos la cadena descovertida.

            // Ciframos utilizando el Algoritmo MD5.
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(clave));
            md5.Clear();

            //Ciframos utilizando el Algoritmo 3DES.
            TripleDESCryptoServiceProvider tripledes = new TripleDESCryptoServiceProvider();
            tripledes.Key = llave;
            tripledes.Mode = CipherMode.ECB;
            tripledes.Padding = PaddingMode.PKCS7;
            ICryptoTransform convertir = tripledes.CreateDecryptor();
            byte[] resultado = convertir.TransformFinalBlock(arreglo, 0, arreglo.Length);
            tripledes.Clear();

            string cadena_descifrada = UTF8Encoding.UTF8.GetString(resultado); // Obtenemos la cadena
            return cadena_descifrada; // Devolvemos la cadena
        }
    }
}
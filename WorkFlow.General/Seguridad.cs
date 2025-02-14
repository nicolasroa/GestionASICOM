using System;
using System.Security.Cryptography;
using System.Text;

namespace WorkFlow.General
{
    public class Seguridad
    {
        public static string Encriptar(string texto)
        {
            byte[] keyArray;
            byte[] Arreglo_a_Cifrar = UTF8Encoding.UTF8.GetBytes(texto);
            string key = "ABCDEFGHIJKLMÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz";
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

            hashmd5.Clear();

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();

            byte[] ArrayResultado = cTransform.TransformFinalBlock(Arreglo_a_Cifrar, 0, Arreglo_a_Cifrar.Length);

            tdes.Clear();

            return Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);
        }


        public static string Desencriptar(string textoEncriptado)
        {
            try
            {

                if (textoEncriptado == null)
                    return "";

                byte[] keyArray;
                byte[] Array_a_Descifrar = Convert.FromBase64String(textoEncriptado);
                string key = "ABCDEFGHIJKLMÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz";

                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

                hashmd5.Clear();

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();

                byte[] resultArray = cTransform.TransformFinalBlock(Array_a_Descifrar, 0, Array_a_Descifrar.Length);

                tdes.Clear();
                return UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch
            {

                return textoEncriptado;
            }
        }

    }
}

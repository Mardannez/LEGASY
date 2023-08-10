using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace LEGASY.Encripta
{
    public class EncryptMD5
    {
        public string Encriptar (string mensaje)
        {

            string hash = "Encriptacion Legasy";
            byte[] data = UTF32Encoding.UTF8.GetBytes (mensaje);

            MD5 md5 = MD5.Create ();    
            TripleDES tripldes = TripleDES.Create ();

            tripldes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripldes.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripldes.CreateEncryptor ();
            byte[] result = transform.TransformFinalBlock(data,0,data.Length);
            
            return Convert.ToBase64String(result);

        }

        public string DesEncriptar(string mensajeEn)
        {

            string hash = "Encriptacion Legasy";
            byte[] data = Convert.FromBase64String(mensajeEn);

            MD5 md5 = MD5.Create();
            TripleDES tripldes = TripleDES.Create();

            tripldes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripldes.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripldes.CreateDecryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return UTF8Encoding.UTF8.GetString(result);

        }

    }
}
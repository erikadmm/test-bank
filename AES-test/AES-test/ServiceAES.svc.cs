using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AES_test
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ServiceAES : IServiceAES
    {
        public string GetDecryption(string inputMessage, string iv, string pass)
        {
            try
            {
                var AES = new System.Security.Cryptography.RijndaelManaged();
                var Buffer = Convert.FromBase64String(inputMessage);
                AES.Key = Convert.FromBase64String(pass);
                AES.IV = Convert.FromBase64String(iv);
                AES.Mode = System.Security.Cryptography.CipherMode.CBC;
                AES.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

                System.Security.Cryptography.ICryptoTransform DESDecrypter = AES.CreateDecryptor();

                return ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length));
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public Dictionary<string, string> GetEncryption(string inputMessage, string pass)
        {
            Dictionary<string, string> response = new Dictionary<string, string>();
            try
            {
                var AES = new System.Security.Cryptography.RijndaelManaged();
                System.Security.Cryptography.ICryptoTransform Encryptor;

                var Buffer = Convert.FromBase64String(inputMessage);
                AES.Mode = System.Security.Cryptography.CipherMode.CBC;
                AES.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                AES.Key = Convert.FromBase64String(pass);
                AES.GenerateIV();
                Encryptor = AES.CreateEncryptor();
                var plaintext = Encryptor.TransformFinalBlock(Buffer, 0, Buffer.Length);

                response.Add("Data", Convert.ToBase64String(plaintext));
                response.Add("IV", Convert.ToBase64String(AES.IV));
                response.Add("Key", Convert.ToBase64String(AES.Key));
            }
            catch (Exception e)
            {
                response.Add("Error message", e.Message);
            }

            return response;
        }

    }
}

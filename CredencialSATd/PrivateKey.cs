using CredencialSAT.Tools;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CredencialSAT
{
    public  class PrivateKey
    {
        public  string? Pem { get; set; }
        public string? PassPhrase { get; set; }
        public string? PublicKey { get; set; }

        public  PrivateKey (string privateKeyPath, string passPhrase){
            var data = File.ReadAllBytes(privateKeyPath);
            var content = Convert.ToBase64String(data);

            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentNullException(nameof(privateKeyPath));
            }

             Pem = new PemExtractor(content).ExtractPrivateKey();

            if (string.IsNullOrEmpty(Pem))
            {
                //Pem = ConverDerToPem(content,!string.IsNullOrEmpty(passPhrase));
            }

            PublicKey = GetRsaParameters(privateKeyPath,  passPhrase);
        }

        private static string GetRsaParameters(string rsaPrivateKey, string passPhrase)
        {
            var pwRSA = RSA.Create();
            var filebyte = File.ReadAllBytes(rsaPrivateKey);
            if (string.IsNullOrEmpty(passPhrase))
            {
                pwRSA.ImportPkcs8PrivateKey(filebyte, out _);
            }
            else
            {
                pwRSA.ImportEncryptedPkcs8PrivateKey(Encoding.ASCII.GetBytes(passPhrase), filebyte, out _);
            }
            
            var publickey = pwRSA.ExportRSAPublicKey();
           // var keysize = pwRSA.KeySize;
            return ConverDerToPem(Convert.ToBase64String(publickey));
        }

        
        public static string ConverDerToPem(string contents/*,bool isEncrypted*/)
        {
            return contents.ChunkSplit(64, "\r\n");
        }



    }

   
}


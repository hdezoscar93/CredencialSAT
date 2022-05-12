using CredencialSAT.DTO;
using CredencialSAT.Tools;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;


namespace CredencialSAT
{
    public class Certificate
    {
        public string? Pem { get; set; }
        //public string? RFC { get; set; }
        //public string? LegalName { get; set; }
        public PemValue? PemValue {get;set;}

        public Certificate(string path)
        {
            var data = File.ReadAllBytes(path);
            

            if (data.Length <= 0) throw new ArgumentNullException(nameof(path));
            
            var content = Convert.ToBase64String(data);
            Pem = new PemExtractor(content).ExtractCertificate();

            if (string.IsNullOrEmpty(Pem))
            {
                Pem = ConvertDerToPem(content);
            }

            byte[] p8bytes = Convert.FromBase64String(content.ChunkSplit(64, "\r\n"));
            X509Certificate2 cert = new X509Certificate2(p8bytes);
            PemValue = cert.ToString(true).GetPemValue();
        }


        public static string ConvertDerToPem(string contents)
        {
            return contents.ChunkSplit( 64, "\r\n");
        }

        public static string GeneratePemFormat(string contents)
        {
            return "-----BEGIN CERTIFICATE-----\r\n"
                + contents.ChunkSplit( 64, "\r\n")
                + "-----END CERTIFICATE-----";
        }
         
       
    }
}

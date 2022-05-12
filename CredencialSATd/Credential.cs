using System;
using System.Collections.Generic;
using System.Text;

namespace CredencialSAT
{
    public class Credential
    {
        public Certificate? Certificate { get; set; }
        public PrivateKey? PrivateKey { get; set; }

        public Credential(string certificatePath, string privateKeyPath, string passPhrase)
        {
            Certificate = new Certificate(certificatePath);
            PrivateKey = new PrivateKey(privateKeyPath, passPhrase);
        }

    }
}

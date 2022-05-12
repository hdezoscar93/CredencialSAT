
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CredencialSAT
{
    public class PemExtractor
    {
        private readonly string contents;

        public PemExtractor(string contents)
        {
            this.contents = contents;
        }

        public string? ExtractCertificate()
        {
            return ExtractBase64("CERTIFICATE");
        }

        public string? ExtractPublicKey()
        {
            return ExtractBase64("PUBLIC KEY");
        }


        public string? ExtractPrivateKey() {

            string? extracted= ExtractBase64("PRIVATE KEY");
             // PKCS#8 plain private key
            if (!string.IsNullOrEmpty(extracted)){
                return extracted;
            }

            extracted= ExtractBase64("PRSA PRIVATE KEY");
            // PKCS#5 plain private key
            if (!string.IsNullOrEmpty(extracted)){
                return extracted;
            }
            extracted=ExtractRsaProtected();
            // PKCS#5 encrypted private key
            if (!string.IsNullOrEmpty(extracted)){
                return extracted;
            }

            // PKCS#8 encrypted private key
            return ExtractBase64("ENCRYPTED PRIVATE KEY");
        }

        protected string? ExtractRsaProtected(){
            string pattern = "^-----BEGIN RSA PRIVATE KEY-----\r?\n"+
                             "Proc-Type: .+\r?\n"+
                             "DEK-Info: .+\r?\n\r?\n"+
                             "([A-Za-z0-9+\\/=]+\r?\n)+"+
                             "-----END RSA PRIVATE KEY-----\r?\n?"+
                             "$";

            Regex rg = new Regex(pattern);
            MatchCollection matchedAuthors = rg.Matches(contents);
            var data = matchedAuthors.FirstOrDefault();
            return data?.Value;
        }

        /*
          protected function extractRsaProtected(): string {
            $matches = [];
            $pattern = '/^'
                . '-----BEGIN RSA PRIVATE KEY-----\r?\n'
                . 'Proc-Type: .+\r?\n'
                . 'DEK-Info: .+\r?\n\r?\n'
                . '([A-Za-z0-9+\/=]+\r?\n)+'
                . '-----END RSA PRIVATE KEY-----\r?\n?'
                . '$/m';
            preg_match($pattern, $this->getContents(), $matches);
            return $this->normalizeLineEndings(strval($matches[0] ?? ''));
            }
        
        */

        private string? ExtractBase64(string type)
        {
            string pattern = "^-----BEGIN " + type + "-----\r?\n([A-Za-z0-9+\\/=]+\r?\n)+-----END CERTIFICATE-----\r?\n?$";
            Regex rg = new Regex(pattern);
            MatchCollection matchedAuthors = rg.Matches(contents);
            var data = matchedAuthors.FirstOrDefault();
            return data?.Value;
        }



    }
}

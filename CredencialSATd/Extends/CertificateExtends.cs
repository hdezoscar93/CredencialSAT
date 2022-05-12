using System;
using System.Collections.Generic;
using System.Text;

namespace CredencialSAT.Extends
{
    public static class CertificateExtends
    {
        public static bool IsValid(this Credential credencial)
        {
            var date = DateTime.Now;
            return date >= credencial?.Certificate?.PemValue?.ValidFrom && date <= credencial.Certificate.PemValue.ValidTo;
        }

        public static bool IsFiel(this Credential credencial)
        {
            return string.IsNullOrEmpty(credencial!.Certificate!.PemValue!.Issuer!.OU);
        }




    }
}

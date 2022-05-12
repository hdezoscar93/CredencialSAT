using System;
using System.Collections.Generic;
using System.Text;

namespace CredencialSAT.DTO
{
    public class PemValue
    {
        public string? Name { get; set; }
        public Subject? Subject { get; set; }
        public string? Hash { get; set; }
        public Issuer? Issuer { get; set; }
        public string? Version { get; set; }
        public string? SerialNumber { get; set; }
        public string? SerialNumberHex { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string? ValidFromTimeT { get; set; }
        public string? ValidToTimeT { get; set; }
        public string? SignatureTypeSN { get; set; }
        public string? SignatureTypeLN { get; set; }
        public string? SignatureTypeNID { get; set; }
        public Extensions? Extensions { get; set; }
    }

    public class Subject
    {
        public string? CN { get; set; }
        public string? Name { get; set; }
        public string? O { get; set; }
        public string? C { get; set; }
        public string? EmailAddress { get; set; }
        public string? UniqueIdentifier { get; set; }
        public string? SerialNumber { get; set; }
    }

    public class Issuer
    {
        public string? CN { get; set; }
        public string? O { get; set; }
        public string? OU { get; set; }
        public string? EmailAddress { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
        public string? C { get; set; }
        public string? ST { get; set; }
        public string? L { get; set; }
        public string? UniqueIdentifier { get; set; }
        public string? UnstructureName { get; set; }
    }

    public class Extensions
    {
        public string? BasicConstraints { get; set; }
        public string? KeyUsage { get; set; }
        public string? NsCertType { get; set; }
        public string? ExtendedKeyUsage { get; set; }
    }

}

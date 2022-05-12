using CredencialSAT.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CredencialSAT.Tools
{
    public static class MapTool
    {
        public static Dictionary<string, string> MapOID(this string data)
        {
            string pattern = @"[(a-zA-Z\.0-9)]+=";
            var label = Regex.Match(data, pattern);
            var values = Regex.Split(data, pattern);
            values = values[1..values.Length];

            Dictionary<string, string> map = new Dictionary<string, string>();

            foreach (var value in values)
            {
                map.Add(label.Value, value.TrimEnd(new char[] { ' ', ',' }));
                label = label.NextMatch();
            }
            return map;
        }

        public static string[] MapSectionOID(this string datacr)
        {
            string pattern = @"\[([a-zA-Z\s]*)\]";
            var values = Regex.Split(datacr, pattern);
            return values[1..values.Length];
        }

        public static PemValue GetPemValue(this string dataCR)
        {
            var sections = dataCR.MapSectionOID();
            var value = new PemValue();
            Dictionary<string, string> map = new Dictionary<string, string>();  

            for (int i = 0; i < sections.Length; i++)
            {
                map.Add(sections[i], sections[++i].Replace("\r\n",""));
            }

            var subject = map["Subject"].MapOID();
            var issuer = map["Issuer"].MapOID();

            value.Version = map["Version"];
            value.Subject = new Subject
            {
                C = subject["C="],
                CN = subject["CN="],
                EmailAddress = subject["E="],
                UniqueIdentifier = subject["OID.2.5.4.45="],
                SerialNumber = subject["SERIALNUMBER="],
                O = subject["O="],
            };
            value.Issuer = new Issuer
            {
                CN = issuer["CN="],
                O = issuer["O="],
                EmailAddress = issuer["E="],
                Street = issuer["STREET="],
                PostalCode =issuer["PostalCode="],
                C = issuer["C="],
                ST = issuer["S="],
                L = issuer["L="],
                ///Todo
                ///Identificar los campos ya que pueden cambiar la version del oid y quedarnos sin datos
                UniqueIdentifier =  issuer.ContainsKey("OID.2.5.4.45=")?  issuer["OID.2.5.4.45="]: null,
                UnstructureName = issuer.ContainsKey("OID.1.2.840.113549.1.9.2=") ?  issuer["OID.1.2.840.113549.1.9.2="] : null,
                OU = issuer["OU="]
            };
            value.SerialNumber = map["Serial Number"];
            value.ValidFrom = Convert.ToDateTime(map["Not Before"]);
            value.ValidTo = Convert.ToDateTime(map["Not After"]);
            return value;
        }


    }
}

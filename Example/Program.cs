
using CredencialSAT;
using CredencialSAT.Extends; 


///Para poder realizar pruebas copiar la carpeta _files  a la raiz del directorio
string filePath = @"c:\_files";

var datacer = Path.Combine(filePath, "RFC-PAC-SC","Personas Morales", "FIEL_EKU9003173C9_20190614160838", "eku9003173c9.cer");
var datakey = Path.Combine(filePath, "RFC-PAC-SC", "Personas Morales","FIEL_EKU9003173C9_20190614160838", "Claveprivada_FIEL_EKU9003173C9_20190614_160838.key");
var password = File.ReadAllText(Path.Combine(filePath, "RFC-PAC-SC", "Contraseña.txt"));



var credencial = new Credential(datacer, datakey, password);

if (credencial.IsFiel())
{
    throw new Exception("The certificate and private key is not a FIEL");
}

if (!credencial.IsValid())
{
    throw new Exception("The certificate and private key is not valid at this moment");
}

Console.WriteLine(credencial.Certificate.PemValue.Issuer.OU);
Console.WriteLine(credencial.Certificate.PemValue.Issuer.Street);
Console.WriteLine(credencial.Certificate.PemValue.Issuer.L);
Console.WriteLine(credencial.Certificate.PemValue.Issuer.ST);
Console.WriteLine(credencial.PrivateKey.PublicKey);



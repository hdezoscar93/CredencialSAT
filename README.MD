# cscfdi/credentials

> Libreria para el uso de FIEL para el  SAT

La documentación del proyecto está en español porque ese es el lenguaje principal de los usuarios.

Esta librería ha sido creada para poder trabajar con los FIEL del SAT. De esta forma,
se simplifica el proceso de obtener datos particulares del archivo de certificado
así como de la llave pública.

- La FIEL (o eFirma) es utilizada para firmar electrónicamente documentos (generalmente usando XML-SEC) y
  está reconocida por el gobierno mexicano como una manera de firma legal de una persona física o moral.

Con esta librería no es necesario convertir los archivos generados por el SAT a otro formato,
se pueden utilizar tal y como el SAT los entrega.


## Ejemplo básico de uso
```cs
//cs 6.0lts
using CredencialSAT;
using CredencialSAT.Extends; 

var cerFile = 'fiel/certificado.cer';
var pemKeyFile = 'fiel/private-key.key';
var passPhrase = '12345678a'; // contraseña para abrir la llave privada

var credencial = new Credential(cerFile, pemKeyFile, password);

 if (!credencial.IsValid()) //verifica la validez del cerificado
{
    throw new Exception("The certificate and private key is not valid at this moment");
}

Console.WriteLine(credencial.Certificate.PemValue.Issuer.OU);
Console.WriteLine(credencial.Certificate.PemValue.Issuer.Street);
Console.WriteLine(credencial.Certificate.PemValue.Issuer.L);
Console.WriteLine(credencial.Certificate.PemValue.Issuer.ST);
Console.WriteLine(credencial.PrivateKey.PublicKey);

```
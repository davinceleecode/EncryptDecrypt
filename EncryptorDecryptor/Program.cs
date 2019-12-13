using System;
using EncryptLeebrary;
using DecryptLeebrary;

namespace EncryptorDecryptor
{
    class Program
    {

        static void Main(string[] args)
        {
            DecryptData decryptData = new DecryptData();
            EncryptData encryptData = new EncryptData();





            Console.WriteLine("Please input value to Encrypt:");
            var val = Console.ReadLine();
            var encryptedValue = encryptData.Encryptor(val, true, "lee");
            Console.WriteLine($"\nEncrypted Value: {encryptedValue}");
            Console.WriteLine($"Decrypted Value: {decryptData.Decryptor(encryptedValue, true, "lee")}");

        }


    }
}

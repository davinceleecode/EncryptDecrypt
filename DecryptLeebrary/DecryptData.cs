using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Security.Cryptography;

namespace DecryptLeebrary
{
    /// <summary>
    /// DecryptText
    /// </summary>
    public class DecryptData
    {
        /// <summary>
        /// Decrypt the text value
        /// </summary>
        /// <param name="cipherString">value to be decrypt</param>
        /// <param name="useHashing">option to use hashing in encryption</param>
        /// <param name="securityKey">your personal securitykey</param>
        /// <returns></returns>
        public string Decryptor(string cipherString, bool useHashing, string securityKey)
        {
            byte[] keyArray;
            //get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            AppSettingsReader settingsReader =
                                               new AppSettingsReader();
            //Get your key from config file to open the lock!
            string key = (string)settingsReader.GetValue(securityKey,
                                                         typeof(String));

            if (useHashing)
            {
                //if hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Clear();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);


        }
    }
}

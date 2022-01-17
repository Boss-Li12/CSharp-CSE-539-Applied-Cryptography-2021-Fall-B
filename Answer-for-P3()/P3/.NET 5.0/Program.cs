using System;
using System.Security.Cryptography; // Aes
using System.Numerics; // BigInteger
using System.IO;

namespace P3
{
    class Program
    {
        //get_bytes_from_string
        static byte[] get_bytes_from_string(string input)
        {
            var input_split = input.Split(' ');
            byte[] inputBytes = new byte[input_split.Length];
            int i = 0;
            foreach (string item in input_split)
            {
                inputBytes.SetValue(Convert.ToByte(item, 16), i);
                i++;
            }
            return inputBytes;
        }


        static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {   
                //aesAlg.KeySize = 256;
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }


        static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {   
                aesAlg.KeySize = 256;
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }


        public static string P3(string[] args)
        {
            // Make sure you are familiar with the System.Numerics.BigInteger class and how to use some of the functions it has (Parse, Pow, ModPow, Subtract, ToByteArray, etc.)
            
            // optional hint: for encryptiong/ decryption with AES, use google or another search engine to find the microsoft documentation on Aes (google this--> System.Security.Cryptography.Aes)

            // optional hint: here is an example of how to convert the IV input string to a byte array https://gist.github.com/GiveThanksAlways/df9e0fa9e7ea04d51744df6a325f7530

            // you will be using BigInteger functions for almost all, if not all mathmatical operations. (Pow, ModPow, Subtract)
            // N = 2^(N_e) - N_c (this calculation needs to be done using BigInteger.Pow and BigInteger.Subtract)

            // Diffie-Hellman key is g^(xy) mod N. In the input you are given g_y which is g^y. So to make the key you need to perform g_y^(x) using the BigInteger class
            // key = g_y^(x) mod N (this calculation needs to be done using BigInteger.ModPow)

            // you can convert a BigInteger into a byte array using the BigInteger.ToByteArray() function/method

            //Console.WriteLine("just printing the input");
            //store the useful input
            string IV_string = "";
            int n_e = 0, n_c = 0;
            BigInteger n = 0;
            BigInteger x = 0, g_y = 0;
            string c_encrypted = "";
            string p_plaintext = "";
            //the index
            int index = 1;
            foreach(var item in args)
            {   
                //get the useful input
                switch (index)
                {
                    case 1:
                        IV_string = item;
                        break;
                    case 4:
                        n_e = int.Parse(item);
                        break;
                    case 5:
                        n_c = int.Parse(item);
                        break;
                    case 6:
                        x = BigInteger.Parse(item);
                        break;
                    case 7:
                        g_y = BigInteger.Parse(item);
                        break;
                    case 8:
                        c_encrypted = item;
                        break;
                    case 9:
                        p_plaintext = item;
                        break;
                    default:
                        break;
                }
                //Console.WriteLine(item); //DELETE, just a placeholder
                index++;
            }
            //CHECK
            /*
            Console.WriteLine(IV_string);
            Console.WriteLine(n_e);
            Console.WriteLine(n_c);
            Console.WriteLine(x);
            Console.WriteLine(g_y);
            Console.WriteLine(c_encrypted);
            Console.WriteLine(p_plaintext);
            Console.WriteLine("\n");
            */
            //GET IV
            byte[] IV_bytes = get_bytes_from_string(IV_string);
            //Console.WriteLine(BitConverter.ToString(IV_bytes).Replace("-", " "));
            //Console.WriteLine(IV_bytes.Length);

            //GET KEY
            n = BigInteger.Subtract(BigInteger.Pow(2, n_e), n_c);
            //Console.WriteLine(n);
            BigInteger key = BigInteger.ModPow(g_y, x, n);
            //Console.WriteLine(key);
            byte[] KEY_bytes = key.ToByteArray();
            //Console.WriteLine(BitConverter.ToString(KEY_bytes).Replace("-", " "));
            //Console.WriteLine(KEY_bytes.Length);
            
            //get the results
                
            //myAes.KeySize = 256;
            //Console.WriteLine("IV:{0}", BitConverter.ToString(myAes.IV).Replace("-", " "));
            //Console.WriteLine("IV:{0}", BitConverter.ToString(myAes.IV).Replace("-", " "));

            // Encrypt the string to an array of bytes.
            //Console.WriteLine(KEY_bytes.Length);
            //Console.WriteLine(IV_bytes.Length);
            //Console.WriteLine(p_plaintext);
            byte[] encrypted = EncryptStringToBytes_Aes(p_plaintext, KEY_bytes, IV_bytes);               
            
            // Decrypt the bytes to a string.
            byte[] c_encrypted_byte = get_bytes_from_string(c_encrypted);
            string decrypted = DecryptStringFromBytes_Aes(c_encrypted_byte, KEY_bytes, IV_bytes);

            //Display 
            //Console.WriteLine(encrypted.Length);
            //Console.WriteLine(get_bytes_from_string(c_encrypted).Length);
            //Console.WriteLine("encrypted: {0}", BitConverter.ToString(encrypted).Replace("-", " "));
            //Console.WriteLine("decrypted: {0}", decrypted);
            
            
            
            /*

            dotnet run "A2 2D 93 61 7F DC 0D 8E C6 3E A7 74 51 1B 24 B2" 251 465 255 1311 2101864342 8995936589171851885163650660432521853327227178155593274584417851704581358902 "F2 2C 95 FC 6B 98 BE 40 AE AD 9C 07 20 3B B3 9F F8 2F 6D 2D 69 D6 5D 40 0A 75 45 80 45 F2 DE C8 6E C0 FF 33 A4 97 8A AF 4A CD 6E 50 86 AA 3E DF" AfYw7Z6RzU9ZaGUloPhH3QpfA1AXWxnCGAXAwk3f6MoTx

            */
            string P3_answer = decrypted + "," + BitConverter.ToString(encrypted).Replace("-", " "); 
            Console.WriteLine(P3_answer);
            return P3_answer;

        }
        static void Main(string[] args)
        {
            // args is the array that contains the command line inputs
            P3(args); // This will run your project code. The autograder will grade the return value of the P3 function
        }
    }
}

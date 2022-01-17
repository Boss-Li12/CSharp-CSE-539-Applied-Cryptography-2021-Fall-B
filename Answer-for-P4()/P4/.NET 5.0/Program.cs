using System;
using System.Numerics; // BigInteger
using System.Collections.Generic;

namespace P4
{
    class Program
    {   
        public static BigInteger egcd(BigInteger a, BigInteger b)
        {
            BigInteger x = 0, y = 1, u = 1, v = 0;
            BigInteger q = 0, r = 0, m = 0, n = 0;
            while(a != 0)
            {
                q = b / a;
                r = b % a;
                m = x - u * q;
                n = y - v * q;
                b = a;
                a = r;
                x = u;
                y = v;
                u = m;
                v = n;
            }
            return x;
        } 
        public static string P4(string[] args)
        {
            /*
            * useful help for RSA encrypt/decrypt: https://www.di-mgt.com.au/rsa_alg.html
            * help with extended euclidean algorithm: https://en.wikipedia.org/wiki/Extended_Euclidean_algorithm
            * 
            */

            // get the args
            BigInteger e = 65537;
            int p_e = 0, p_c = 0, q_e = 0, q_c = 0;
            BigInteger CipherText = 0;
            BigInteger PlaintText = 0;
            //Console.WriteLine("just printing the input");
            //the index
            int index = 1;
            foreach(var item in args)
            {   
                //get the useful input
                switch (index)
                {
                    case 1:
                        p_e = int.Parse(item);
                        break;
                    case 2:
                        p_c = int.Parse(item);
                        break;
                    case 3:
                        q_e = int.Parse(item);
                        break;
                    case 4:
                        q_c = int.Parse(item);
                        break;
                    case 5:
                        CipherText = BigInteger.Parse(item);
                        break;
                    case 6:
                        PlaintText = BigInteger.Parse(item);
                        break;
                    default:
                        break;
                }
                //Console.WriteLine(item); //DELETE, just a placeholder
                index++;
            }
            
            BigInteger p = 0, q = 0;
            p = BigInteger.Subtract(BigInteger.Pow(2, p_e), p_c);
            q = BigInteger.Subtract(BigInteger.Pow(2, q_e), q_c);
            BigInteger phi_n = BigInteger.Multiply(p - 1, q - 1);
            //Console.WriteLine(phi_n);
            //e * d mod phi_n = 1 =>
            //d * e - m * phi_n = 1 and (e, phi_n) = 1
            BigInteger d = egcd(e, phi_n);
            //Console.WriteLine(d);
            //Check if (e * d) % phi_c == 1
            //Console.WriteLine((e * d) % phi_n);

            //Console.WriteLine(CipherText);
            //Console.WriteLine(PlaintText);
            //Console.WriteLine(p * q);
            //decryption
            BigInteger result_1 = BigInteger.ModPow(CipherText, d, p * q);
            //Console.WriteLine(result_1);

            //encryption
            BigInteger result_2 = BigInteger.ModPow(PlaintText, e, p * q);
            //Console.WriteLine(result_2);
            
            //Console.WriteLine(p);
            //Console.WriteLine(q);
            // Some other helpful links: https://gist.github.com/GiveThanksAlways/00a5c4e911795992268b0c998e2ec487

            // dotnet run 254 1223 251 1339 66536047120374145538916787981868004206438539248910734713495276883724693574434582104900978079701174539167102706725422582788481727619546235440508214694579  1756026041
            string P4_answer = result_1.ToString() + "," + result_2.ToString();
            Console.WriteLine(P4_answer);
            return P4_answer;
        }

        static void Main(string[] args)
        {
            // args is the array that contains the command line inputs
            P4(args); // This will run your project code. The autograder will grade the return value of the P1_2 function
        }
    }
}

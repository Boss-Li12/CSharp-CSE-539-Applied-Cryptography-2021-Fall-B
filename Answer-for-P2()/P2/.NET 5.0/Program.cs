using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;

namespace P2
{
    class Program
    {
        // This function will help us get the input from the command line
        public static string GetInputFromCommandLine(string[] args)
        {
            // get the input from the command line
            string input = "";
            if (args.Length == 1)
            {
                input = args[0]; // Gets the first string after the 'dotnet run' command
            }
            else
            {
                Console.WriteLine("Not enough or too many inputs provided after 'dotnet run' ");
            }
            return input;
        }

        // the function to compute md5 with salt
        public static string compute_md5_with_salt(string plaintext, string salt)
        {
            string res;
            byte[] data_before_salt = Encoding.UTF8.GetBytes(plaintext);
            byte[] data_after_salt = new byte[data_before_salt.Length + 1];
            for(int i = 0; i < data_before_salt.Length; i++)
            {
                data_after_salt[i] = data_before_salt[i];
            }
            data_after_salt[data_before_salt.Length] = Convert.ToByte(salt, 16);

            MD5 md5 = MD5.Create();
            byte[] data = md5.ComputeHash(data_after_salt);
            res = BitConverter.ToString(data).Replace("-", " ").Substring(0, 14);
            return res;                        
        }
        
        // create random plaintext with same length
        public static string create_random_plaintext(string alphanumeric_characters, int len)
        {
            string res = "";
            for(int i = 0; i < len; i++)
            {
                Random rand = new Random();
                int rndnum = rand.Next(0, 62); 
                res += alphanumeric_characters[rndnum];               
            }
            return res;
        }
        
        public static string P2(string[] args)
        {
            // Some helpful hints:
            // The main idea is to concateneate the salt to a random string, 
            // then feed that into the hashFunction, 
            // then keep track of those salted hashes until you find a matching pair of salted hashes, 
            // then print the solution which is the two strings that gave the matching salted hashes
            // NOTE: When I say salted hashes, I mean that you salted the password and then fed it into the hashFunction. So it is the hash of the password+salt (in this case "+" means concatenated together into one)

            // https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.md5?view=netcore-3.1
            // hint: what does Create() do?

            // optional hint: review converting a string into a byte array (byte[]) and the reverse, converting a byte array (byte[]) into a string BitConverter.ToString(exampleByteArray).Replace("-", " ");

            // This code will convert a string to a byte array
           /* string example = "Edward Snowden";
            byte[] exampleByteArray = Encoding.UTF8.GetBytes(example);
            Console.WriteLine(exampleByteArray[0]);*/
            // passwords have to be made only using alphanumeric characters, so you can make random passwords using any of the characters in the string provided below (note: The starter code doesn't include lowercase just for simplicity but you can include lowercase as well. )
            string alphanumeric_characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";

            // optional hint: What data structure can you use to store the salted hashes that has a really fast lookup time of O(1) (constant) ?
            // You don't have to use this data structure, but it will make your code run fast. The System.Collections.Generic libary is a good place to start

            // TODO: Employ the Birthday Paradox to find a collision in the MD5 hash function

            string salt = GetInputFromCommandLine(args);
            Console.WriteLine(salt);
            // These were given as en example, you are going to have to find two passwords that have matching salted hashes with your code and then output them for the autograder to see
            string password1 = "";
            string password2 = "";
            int len = 10;
            bool flag = false;// find the same hash?
            // to define it, you have to use another line
            // the key is md5hash, the value is plaintext
            Dictionary<string, string> md5hashwithsalt_plaintext =
                new Dictionary<string, string>();
            
            while(!flag)
            {
                string new_plaintext = create_random_plaintext(alphanumeric_characters, len);
                string new_md5 = compute_md5_with_salt(new_plaintext, salt);
                //md5hashwithsalt_plaintext.Add(compute_md5_with_salt(password1, salt), password1);
                //Console.WriteLine(md5hashwithsalt_plaintext.ContainsKey(compute_md5_with_salt(password1, salt)));
                if(md5hashwithsalt_plaintext.ContainsKey(new_md5) == false)
                    md5hashwithsalt_plaintext.Add(new_md5, new_plaintext);
                else if(md5hashwithsalt_plaintext[new_md5] != new_plaintext)
                {
                    password1 = md5hashwithsalt_plaintext[new_md5];
                    password2 = new_plaintext;
                    break;
                }
            }


            Console.WriteLine(compute_md5_with_salt(password1, salt));
            Console.WriteLine(compute_md5_with_salt(password2, salt));

            string P2_answer = password1 + "," + password2;
            Console.WriteLine(P2_answer); // you can still print things to the console. The autograder will ignore this, it will only test the return value of this function

            /*MD5 md5 = MD5.Create();
            byte[] data_before_salt = Encoding.UTF8.GetBytes(password1);
            string str_before_salt = BitConverter.ToString(data_before_salt).Replace("-", " ");
            Console.WriteLine(str_before_salt);
            byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(password1));
            string str_pwd1 = BitConverter.ToString(data).Replace("-", " ");
            Console.WriteLine(str_pwd1);

            //加盐后多一个byte
            //通过构造一个新的byte[]存入加入salt的结果
            byte[] data_after_salt = new byte[data_before_salt.Length + 1];
            for(int i = 0; i < data_before_salt.Length; i++)
            {
                data_after_salt[i] = data_before_salt[i];
            }
            data_after_salt[data_before_salt.Length] = Convert.ToByte(salt, 16);
            string str_after_salt = BitConverter.ToString(data_after_salt).Replace("-", " ");
            Console.WriteLine(str_after_salt);
            MD5 md5_1 = MD5.Create();
            byte[] data_1 = md5_1.ComputeHash(data_after_salt);
            string data_1_to_string = BitConverter.ToString(data_1).Replace("-", " ").Substring(0, 14);
            Console.WriteLine(data_1_to_string);*/

            // return the solution to the autograder
            return P2_answer; // autograder will grade this value to see if it is correct
        }

        static void Main(string[] args)
        {
            // args is the array that contains the command line inputs
            P2(args); // This will run your project code. The autograder will grade the return value of the P1_2 function
        }

    }
}

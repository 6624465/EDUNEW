using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZY.EDU.Utility
{
    public class QueryStringHelper
    {

        /// <summary>
        /// Method to convert the dictionary objects' key and values pairs to a string and
        /// then to encrypt the string
        /// <para>Example:</para>
        /// <para>1. key1 - value1</para>
        /// <para>2. key2 - value2</para>
        /// <para>3. key3 - value3</para>
        /// <para>Convert the above into key1~value1|key2~value2|key2~value2 </para>
        /// <para>then encryp the above string</para>
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string EncryptQueryString(Dictionary<string, string> target)
        {
            //Create a List of string type
            var keyValue = new List<string>();

            // Loop through the hash table and populate the keyValue List variable with
            // the key string and value string concatenated
            foreach (string key in target.Keys)
                keyValue.Add(String.Concat(key + "~", target[key]));

            //Convert the keyValues List to Array
            string[] keyValues = keyValue.ToArray();

            //Combine all the items in the array into a single long string concatenated with the "|" character
            string combinedKeyValues = String.Join("|", keyValues);

            //Now Encrypt the string
            string encryptCombinedKeyValues = EncryptDecrypt.Encrypt(combinedKeyValues);

            //Return the string
            return encryptCombinedKeyValues;

        }



        /// <summary>
        /// Method to convert an encrypted string to a dictonary object
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static Dictionary<string, string> DecryptQueryString(string target)
        {
            //Decrypt the String
            string decryptString = EncryptDecrypt.Decrypt(target);

            //Split the string based on the delimiter and convert to array.
            string[] deQueryStringLimiter = { "|" };
            string[] arrayValues = decryptString.Split(deQueryStringLimiter, StringSplitOptions.None);


            //Now populate all items in the array to a Dictionary object after splitting to key value pairs.
            var queryString = new Dictionary<string, string>();

            string[] keyValueLimiter = { "~" };

            //Loop through the arrayValues, split and assign in the Dictionary object.
            foreach (string val in arrayValues)
            {
                string[] keyValues = val.Split(keyValueLimiter, StringSplitOptions.None);
                queryString.Add(keyValues[0], keyValues[1]);
            }

            return queryString;


        }


    }
}

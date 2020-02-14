using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace lib.Class
{
    public class Encryption
    {
        private byte[] Key;
        private char[] CharacterMap;

        public Encryption(byte[] key)
        {
            //this.CharacterMap = " !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~¡¢£¤¥¦§¨©ª«¬­®¯°±²³´µ¶·¸¹º»¼½¾¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõö÷øùúûüý".toCharArray();
            string numeros = "0123456789";
            string letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            string caracteres_extendidos = "ÀÁÂÃÄÅÇÈÉÊËÌÍÎÏÑÒÓÔÕÖÙÚÛÜÝàáâãäåçèéêëìíîïñòóôõöùúûüý";
            string digitos = " _()/|,;'";
            //string excluidos = "!\"#$%&*+-.:<=>?@[\\]^`{}~¡¢£¤¥¦§¨©ª«¬­®¯°±²³´µ¶·¸¹º»¼½¾¿Æ×ØÞßæ÷øÐð";
            string characterMap = (numeros + letras + caracteres_extendidos + digitos);
            
            Initialize(Key, characterMap);
        }

        public Encryption(byte[] key, String characterMap)
        {
            Initialize(key, characterMap);
        }

        public Encryption(string key, String characterMap) 
        {
            Initialize(Encoding.ASCII.GetBytes(key), characterMap);
        }

        private void Initialize(byte[] key, String characterMap)
        {
            this.CharacterMap = characterMap.ToCharArray();
            this.Key = key;
        }

        public String getAllMap() {
            return " !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~¡¢£¤¥¦§¨©ª«¬­®¯°±²³´µ¶·¸¹º»¼½¾¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõö÷øùúûüý";
        }

        public String encripty(String s)
        {
            char[] c_source = s.ToCharArray();
            char[] c_destination = new char[c_source.Length];
            int add = 0;
            char c = '\0';
            for (int i = 0; i < c_source.Length; i++)
            {
                add = getValueKeyPosition(i);
                c = getCharMoveMap(add, c_source[i]);
                c_destination[i] = c;
            }
            return new String(c_destination);
        }

        public String descrypt(String s)
        {
            char[] c_source = s.ToCharArray();
            char[] c_destination = new char[c_source.Length];
            int add = 0;
            char c = '\0';
            for (int i = 0; i < c_source.Length; i++)
            {
                add = -getValueKeyPosition(i);
                c = getCharMoveMap(add, c_source[i]);
                c_destination[i] = c;
            }
            return new String(c_destination);
        }

        private int getValueKeyPosition(int i_text_position)
        {
            int div = i_text_position / this.Key.Length;
            int pos = i_text_position - (this.Key.Length * div);
            return this.Key[pos];
        }

        private char getCharMoveMap(int add, char c)
        {
            int i_current_position = 0;
            for (int i = 0; i < this.CharacterMap.Length; i++)
            {
                if (this.CharacterMap[i] == c)
                {
                    i_current_position = i;
                    break;
                }
            }

            i_current_position += add;
            
            while (i_current_position >= this.CharacterMap.Length)
            {
                i_current_position -= this.CharacterMap.Length;
            }

            while (i_current_position < 0)
            {
                i_current_position += this.CharacterMap.Length;
            }

            return this.CharacterMap[i_current_position];
        }

        public static byte[] getNewKey() 
        {
            return Guid.NewGuid().ToByteArray();
        }

        public static string getSHA1Hash(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }

        public static string getMD5Hash(string input)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            { sb.Append(hash[i].ToString("x2")); }
            return sb.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.Checksum;
using SimpleISO7064;
using System.Diagnostics;

namespace sj2324_5ehif_cooking_user_relations.Application.Model
{
    public abstract class Key
    {
        public String _prefix { get; }
        private int _length { get; }
        public readonly string _value;

        protected Key(string prefix, int length)
        {
            _prefix = prefix;
            _length = length;
            _value = GenerateKey();
        }

        private string GetRandomPart()
        {
            Random rnd = new Random();
            StringBuilder sb = new StringBuilder(_length);
            string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            int counter = 0;
            for (int i = 0; i < _length; i++)
            {
                sb.Append(chars[rnd.Next(chars.Length)]);
            }

            return sb.ToString();
        }

        public string GenerateKey()
        {
            string randomPart = GetRandomPart();
            string key = _prefix + randomPart;
            string a = new Iso7064Factory().GetMod37Radix2().ComputeCheckDigit(key);
            return key + a;
        }

        public static bool CheckPrefix(string value, string prefix)
        {
            if (value.Length < prefix.Length)
            {
                return false;
            }

            return value.Substring(0, prefix.Length - 1) == prefix;
        }

        public static bool CheckKey(string value, string? prefix = null)
        {
            var check = prefix == null ? true : CheckPrefix(value: value, prefix: prefix);
            return new Iso7064Factory().GetMod37Radix2()
                .IsValid(value);
        }
    }
    }

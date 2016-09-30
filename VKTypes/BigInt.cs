using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;

namespace VKTypes
{
    public class BigInt
    {
        private List<byte> _digits = new List<byte>();

        private List<byte> FromIntToBytes(int number)
        {
            List<byte> _resultBytes = new List<byte>();
            int digit = 0;
            while (number > 0)
            {
                digit = number % 10;
                _resultBytes.Insert(0, Convert.ToByte(digit));
                number /= 10;
            }
            return _resultBytes;
        }

        private List<byte> FromStringToBytes(string number)
        {
            List<byte> _result = new List<byte>();
            if (!CanParse(number))
                throw new ArgumentException("Check your string input, it can contains some not digit characters");
            foreach (var ch in number) _result.Add(Convert.ToByte(ch.ToString()));
            return _result;
        }

        /*private BigInt Multiply(BigInt another)
        {
            List<byte> result = new List<byte>();
            //return new BigInt(result);

        }*/

        public int Length()
        {
            return _digits.Count;
        }

        public byte GetSumRemainder(byte a, byte b)
        {
            var sum = a + b;
            return Convert.ToByte(sum >= 10 ? sum % 10 : sum);
        }

        public void Normalize(BigInt another)
        {
            int diff = another.Length() - Length();
            if (Math.Sign(diff) < 0) another.Normalize(this);
            else if (Math.Sign(diff) > 0)
            {
                for (int i = Math.Abs(diff); i > 0; i--)
                    _digits.Insert(0, 0);
            }
            
        }

        public byte GetSumWhole(byte a, byte b)
        {
            return Convert.ToByte((a + b) / 10);
        }

        /*public BigInt Add(BigInt another)
        {
            
        }*/

        public static bool CanParse(string number)
        {
            if (Regex.IsMatch(number, @"[^\d]")) return false;
            return true;
        }

        public BigInt(int number)
        {
            _digits = FromIntToBytes(number);
        }

        public BigInt(string number)
        {
            _digits = FromStringToBytes(number);
        }

        public static BigInt Zero()
        {
            return new BigInt(0);
        }

        public override string ToString()
        {
            return _digits.Aggregate("",(result, digit) => result += digit);
        }
    }
}

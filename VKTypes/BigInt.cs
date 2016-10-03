using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace VKTypes
{
    public class BigInt
    {
        private List<byte> _digits = new List<byte>();

        private List<byte> FromIntToBytes(int number)
        {
            List<byte> _resultBytes = new List<byte>();
            int digit = 0;
            do
            {
                digit = number % 10;
                _resultBytes.Insert(0, Convert.ToByte(digit));
                number /= 10;
            } while (number > 0);
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

        public BigInt Multiply(BigInt another)
        {
            List<byte> iteration = new List<byte>();
            List<byte> result = new List<byte>();
            if (another.ToString().TrimEnd('0') == "1")
            {
                for (int i = 0; i < _digits.Count(); i++) result.Add(_digits[i]);
                for (int i = 1; i < another.Length(); i++) result.Add(0);
                return new BigInt(result);
            }
            for (int i = Length() - 1; i >= 0; i--)
            {
                byte carry = 0;
                for (int j = another.Length() - 1; j >= 0; j--)
                {
                    var mul = another.GetBytes()[j] * GetBytes()[i];
                    iteration.Insert(0, GetRemainder(mul + carry));
                    carry = GetWhole(mul + carry);
                }
                if (carry > 0) iteration.Insert(0, carry);
                for (int j = 0; j < Length() - i - 1; j++) iteration.Add(0);
                result = AddBytes(result, iteration);
                iteration.Clear();
            }
            return new BigInt(result);
        }

        public int Length()
        {
            return _digits.Count;
        }

        private void NormalizeBytes(List<byte> bytesA, List<byte> bytesB)
        {
            int diff = bytesA.Count() - bytesB.Count();
            if (Math.Sign(diff) < 0) NormalizeBytes(bytesB, bytesA);
            else if (Math.Sign(diff) > 0)
            {
                for (int i = Math.Abs(diff); i > 0; i--) bytesB.Insert(0, 0);
            }
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

        private byte GetRemainder(int number)
        {
            return Convert.ToByte(number >= 10 ? number % 10 : number);
        }

        private byte GetWhole(int number)
        {
            return Convert.ToByte(number / 10);
        }

        private List<byte> AddBytes(List<byte> bytesA, List<byte> bytesB)
        {
            NormalizeBytes(bytesA, bytesB);
            List<byte> result = new List<byte>();
            byte carry = 0;
            for (int i = bytesA.Count() - 1; i >= 0; i--)
            {
                result.Insert(0, GetRemainder(bytesA[i] + bytesB[i] + carry));
                carry = GetWhole(bytesA[i] + bytesB[i] + carry);
            }
            if (carry > 0) result.Insert(0, carry);
            return result;
        }

        public BigInt Add(BigInt another)
        {
            List<byte> result = AddBytes(GetBytes(), another.GetBytes());
            return new BigInt(result);
        }

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

        public BigInt(List<byte> digits)
        {
            _digits = digits;
        }

        public static BigInt Zero
        {
            get { return new BigInt(0); }
        }

        public static BigInt ValueOf(int number)
        {
            return new BigInt(number);
        }

        public bool IsZero()
        {
            return _digits.FirstOrDefault(d => d > 0) == 0;
        }

        public List<byte> GetBytes()
        {
            return _digits;
        }

        public static BigInt operator *(BigInt one,BigInt two)
        {
            return one.Multiply(two);
        }

        public static BigInt operator *(BigInt one, int two)
        {
            return one.Multiply(ValueOf(two));
        }

        public static BigInt operator +(BigInt one, int two)
        {
            return one.Add(ValueOf(two));
        }

        public static BigInt operator +(BigInt one, BigInt two)
        {
            return one.Add(two);
        }

        public override string ToString()
        {
            if (IsZero()) return "0";
            return _digits.Aggregate("",(result, digit) => result += digit).TrimStart('0');
        }
    }
}

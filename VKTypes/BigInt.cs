using System;
using System.Collections.Generic;
using System.Linq;
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

        public BigInt(int number)
        {
            _digits = FromIntToBytes(number);
        }

        public override string ToString()
        {
            return _digits.Aggregate("",(result, digit) => result += digit);
        }
    }
}

using System;

namespace VKTypes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Factorial(8));
            Console.ReadLine();
        }

        public static BigInt Factorial(int number)
        {
            BigInt result = new BigInt(1);
            for (int i = 2; i <= number; i++)
            {
                result = result * i;
            }
            return result;
        }
    }
}

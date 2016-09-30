using System;
using NUnit.Framework;
using VKTypes;

namespace VKTypes.Tests
{
    [TestFixture]
    public class BigIntTests
    {
        [Test]
        public void FromIntToByteConversionSuccessTest()
        {
            int testInt = 895302938;
            BigInt testBigInt = new BigInt(testInt);
            Assert.AreEqual(testInt.ToString(), testBigInt.ToString());
        }

        [Test]
        public void FromStringToByteConversionSuccessTest()
        {
            int testInt = 895302938;
            BigInt testBigInt = new BigInt(testInt.ToString());
            Assert.AreEqual(testInt.ToString(), testBigInt.ToString());
        }

        [Test]
        public void NormalizeTwoBigIntWhenFirstIsSmaller()
        {
            BigInt first = new BigInt(123);
            BigInt second = new BigInt(12345);
            second.Normalize(first);
            Assert.AreEqual(first.ToString(),"123");
            Assert.IsTrue(first.Length() == 5);
        }

        [Test]
        public void NormalizeTwoBigIntWhenSecondIsSmaller()
        {
            BigInt first = new BigInt(12345);
            BigInt second = new BigInt(12);
            second.Normalize(first);
            Assert.AreEqual(second.ToString(), "12");
            Assert.IsTrue(first.Length() == 5);
        }

        [Test]
        public void CanParseStringToBigInt()
        {
            string notDigitInTheMiddle = "12331f123123";
            string notDigitAtStart = "+af3234234234";
            string notDigitAtEnd = "12312312../";
            string validString = "12345234234456457456";

            Assert.IsFalse(BigInt.CanParse(notDigitInTheMiddle));
            Assert.IsFalse(BigInt.CanParse(notDigitAtStart));
            Assert.IsFalse(BigInt.CanParse(notDigitAtEnd));
            Assert.IsTrue(BigInt.CanParse(validString));
        }

        [Test]
        public void AddTestWithoutCarryNumber()
        {
            BigInt number1 = new BigInt(1111111);
            BigInt number2 = new BigInt(111);
            Assert.AreEqual(number1.Add(number2).ToString(), "1111222");
        }

        [Test]
        public void AddTestWithCarryNumber()
        {
            BigInt number1 = new BigInt(1111111);
            BigInt number2 = new BigInt(99);
            Assert.AreEqual(number1.Add(number2).ToString(), "1111210");
        }

        [Test]
        public void AddTestWithCarryNumberAndOverflow()
        {
            BigInt number1 = new BigInt(999);
            BigInt number2 = new BigInt(1);
            Assert.AreEqual(number1.Add(number2).ToString(), "1000");
        }
    }
}

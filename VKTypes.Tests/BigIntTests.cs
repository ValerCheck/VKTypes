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
            Assert.IsTrue(first.ToString() == "00123");
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
    }
}

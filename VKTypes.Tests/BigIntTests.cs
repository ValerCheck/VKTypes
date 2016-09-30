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
    }
}

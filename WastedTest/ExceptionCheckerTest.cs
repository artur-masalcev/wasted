using System;
using NUnit.Framework;
using Wasted.Utils.Exceptions;

namespace WastedTest
{
    public class ExceptionCheckerTest
    {
        [Test]
        public void ShouldThrowException_WhenInvalidParams()
        {
            Assert.Throws<ArgumentNullException>(() => ExceptionChecker.CheckValidParams(""));
        }
        
        [Test]
        public void ShouldNotThrowException_WhenValidParams()
        {
            Assert.DoesNotThrow(() => ExceptionChecker.CheckValidParams("name"));
        }
    }
}
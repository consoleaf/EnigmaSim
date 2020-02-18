using System;
using NUnit.Framework;
using EnigmaLib;

namespace EnigmaTest
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestAccurate()
        {
            string message = "Hello world!";

            Enigma enigma = new Enigma(Premade.A, Premade.I, Premade.II, Premade.III, "ABC",
                "AV BS CG DL FU HZ IN KM OW RX");

            string encoded = enigma.Encode(message);

            Assert.AreEqual("QGQOP VWOXN!", encoded);
        }

        [Test]
        public void TestDecodable()
        {
            string message = "HELLO WORLD";

            Enigma enigma = new Enigma(Premade.A, Premade.I, Premade.II, Premade.III, "ABC",
                "AV BS CG DL FU HZ IN KM OW RX");

            string encoded = enigma.Encode(message);
            
            enigma = new Enigma(Premade.A, Premade.I, Premade.II, Premade.III, "ABC",
                "AV BS CG DL FU HZ IN KM OW RX");

            encoded = enigma.Encode(encoded);
            
            Assert.AreEqual(message, encoded);
        }
    }
}
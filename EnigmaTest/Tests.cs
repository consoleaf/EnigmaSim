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

            Enigma enigma = new Enigma(PreMade.A, PreMade.I, PreMade.II, PreMade.III, "ABC",
                "AV BS CG DL FU HZ IN KM OW RX");

            string encoded = enigma.Encode(message);

            Assert.AreEqual("QGQOP VWOXN!", encoded);
        }

        [Test]
        public void TestUpperLower()
        {
            string message = "Hello world!";

            Enigma enigma = new Enigma(PreMade.A, PreMade.I, PreMade.II, PreMade.III, "ABC",
                "AV BS CG DL FU HZ IN KM OW RX");

            string encoded = enigma.Encode(message);

            Assert.AreEqual("Qgqop vwoxn!", encoded);
        }

        [Test]
        public void TestDecodable()
        {
            string message = "HELLO WORLD";

            Enigma enigma = new Enigma(PreMade.A, PreMade.I, PreMade.II, PreMade.III, "ABC",
                "AV BS CG DL FU HZ IN KM OW RX");

            string encoded = enigma.Encode(message);
            
            enigma = new Enigma(PreMade.A, PreMade.I, PreMade.II, PreMade.III, "ABC",
                "AV BS CG DL FU HZ IN KM OW RX");

            encoded = enigma.Encode(encoded);
            
            Assert.AreEqual(message, encoded);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace EnigmaLib
{
    /// <summary>
    /// Represents an Enigma machine
    /// </summary>
    public class Enigma
    {
        private readonly Reflector _reflector;
        private readonly Rotor _rotor1;
        private readonly Rotor _rotor2;

        private readonly Rotor _rotor3;

        // private int _ringSet;
        private readonly Dictionary<char, char> _transTab;

        public Enigma(Reflector reflector, Rotor r1, Rotor r2, Rotor r3, string key = "AAA", string plugs = "" /*, 
             int ringSet = 1 */)
        {
            this._reflector = reflector;
            this._rotor1 = r1;
            this._rotor2 = r2;
            this._rotor3 = r3;

            this._rotor1.State = key[0];
            this._rotor2.State = key[1];
            this._rotor3.State = key[2];
            this._reflector.State = 'A';
            // this._ringSet = ringSet;

            this._transTab = new Dictionary<char, char>();
            foreach (var plugPair in plugs.Split())
            {
                var k = plugPair[0];
                var v = plugPair[1];
                _transTab[k] = v;
                _transTab[v] = k;
            }
        }

        public string Encode(string plaintextIn)
        {
            StringBuilder ciphering = new StringBuilder();
            plaintextIn = plaintextIn.ToUpper();
            string plaintextTmp = "";

            foreach (char ch in plaintextIn)
            {
                if (_transTab.ContainsKey(ch))
                    plaintextTmp += _transTab[ch];
                else
                    plaintextTmp += ch;
            }

            plaintextIn = plaintextTmp;

            foreach (char c in plaintextIn)
            {
                if (_rotor2.IsTurnoverPos)
                {
                    _rotor2.Notch();
                    _rotor3.Notch();
                }

                if (_rotor1.IsTurnoverPos)
                    _rotor2.Notch();

                _rotor1.Notch();

                if (!char.IsLetter(c))
                {
                    ciphering.Append(c);
                    continue;
                }

                var t = _rotor1.EncodeRight(c);
                t = _rotor2.EncodeRight(t);
                t = _rotor3.EncodeRight(t);
                t = _reflector.Encipher(t);
                t = _rotor3.EncodeLeft(t);
                t = _rotor2.EncodeLeft(t);
                t = _rotor1.EncodeLeft(t);

                Console.WriteLine();

                ciphering.Append(t);
            }

            string cipheringTmp = "";

            foreach (char ch in ciphering.ToString())
            {
                if (_transTab.ContainsKey(ch))
                    cipheringTmp += _transTab[ch];
                else
                    cipheringTmp += ch;
            }

            return cipheringTmp;
        }

        public override string ToString()
        {
            return "Enigma machine.\r\n\r\n" +
                   $"Reflector: {_reflector}\r\n\r\n" +
                   $"Rotor 1: {_rotor1}\r\n\r\n" +
                   $"Rotor 2: {_rotor2}\r\n\r\n" +
                   $"Rotor 3: {_rotor3}\r\n\r\n";
        }
    }
}
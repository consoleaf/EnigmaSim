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
        private Reflector reflector;
        private Rotor rotor1;
        private Rotor rotor2;
        private Rotor rotor3;
        private int ringset;
        private Dictionary<char, char> transtab;

        public Enigma(Reflector reflector, Rotor r1, Rotor r2, Rotor r3, string key = "AAA", string plugs = "",
            int ringset = 1)
        {
            this.reflector = reflector;
            this.rotor1 = r1;
            this.rotor2 = r2;
            this.rotor3 = r3;

            this.rotor1.State = key[0];
            this.rotor2.State = key[1];
            this.rotor3.State = key[2];
            this.reflector.State = 'A';
            this.ringset = ringset;

            this.transtab = new Dictionary<char, char>();
            foreach (var plugpair in plugs.Split())
            {
                var k = plugpair[0];
                var v = plugpair[1];
                transtab[k] = v;
                transtab[v] = k;
            }
        }

        public string Encode(string plaintext_in)
        {
            StringBuilder ciphertext = new StringBuilder();
            plaintext_in = plaintext_in.ToUpper();
            string plaintextTmp = "";

            foreach (char ch in plaintext_in)
            {
                if (transtab.ContainsKey(ch))
                    plaintextTmp += transtab[ch];
                else
                    plaintextTmp += ch;
            }
            
            plaintext_in = plaintextTmp;

            foreach (char c in plaintext_in)
            {
                if (rotor2.IsTurnoverPos)
                {
                    rotor2.Notch();
                    rotor3.Notch();
                }

                if (rotor1.IsTurnoverPos)
                    rotor2.Notch();

                rotor1.Notch();

                Console.WriteLine(this);

                if (!char.IsLetter(c))
                {
                    ciphertext.Append(c);
                    continue;
                }

                char t;
                Console.Write($"{c} -> ");
                t = rotor1.EncodeRight(c);
                Console.Write($"{t} -> ");
                t = rotor2.EncodeRight(t);
                Console.Write($"{t} -> ");
                t = rotor3.EncodeRight(t);
                Console.Write($"{t} -> ");
                t = reflector.Encipher(t);
                Console.Write($"{t} -> ");
                t = rotor3.EncodeLeft(t);
                Console.Write($"{t} -> ");
                t = rotor2.EncodeLeft(t);
                Console.Write($"{t} -> ");
                t = rotor1.EncodeLeft(t);
                Console.Write($"{t}");

                Console.WriteLine();

                ciphertext.Append(t);
            }

            string ciphertextTmp = "";

            foreach (char ch in ciphertext.ToString())
            {
                if (transtab.ContainsKey(ch))
                    ciphertextTmp += transtab[ch];
                else
                    ciphertextTmp += ch;
            }

            return ciphertextTmp;
        }

        public override string ToString()
        {
            return $"Enigma machine.\r\n\r\n" +
                   $"Reflector: {reflector}\r\n\r\n" +
                   $"Rotor 1: {rotor1}\r\n\r\n" +
                   $"Rotor 2: {rotor2}\r\n\r\n" +
                   $"Rotor 3: {rotor3}\r\n\r\n";
        }
    }
}
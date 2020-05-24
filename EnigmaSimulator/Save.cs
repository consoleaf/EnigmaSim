using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnigmaLib;

namespace EnigmaSimulator
{
    [Serializable]
    internal class Save
    {
        public Enigma enigma;
        public string input, output;
        public char activeKey, activeEnc;
        public string plugs;
    }
}

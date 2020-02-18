using System.Diagnostics.CodeAnalysis;

namespace EnigmaLib
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class PreMade
    {
        public static readonly Rotor IC = new Rotor(wiring: "DMTWSILRUYQNKFEJCAZBPGXOHV", name: "IC",
            model: "Commercial Enigma A, B", date: "1924");

        public static readonly Rotor IIC = new Rotor(wiring: "HQZGPJTMOBLNCIFDYAWVEUSRKX", name: "IIC",
            model: "Commercial Enigma A, B", date: "1924");

        public static readonly Rotor IIIC = new Rotor(wiring: "UQNTLSZFMREHDPXKIBVYGJCWOA", name: "IIIC",
            model: "Commercial Enigma A, B", date: "1924");

        public static readonly Rotor GR_I = new Rotor(wiring: "JGDQOXUSCAMIFRVTPNEWKBLZYH", name: "I",
            model: "German Railway (Rocket)", date: "7 February 1941");

        public static readonly Rotor GR_II = new Rotor(wiring: "NTZPSFBOKMWRCJDIVLAEYUXHGQ", name: "II",
            model: "German Railway (Rocket)", date: "7 February 1941");

        public static readonly Rotor GR_III = new Rotor(wiring: "JVIUBHTCDYAKEQZPOSGXNRMWFL", name: "III",
            model: "German Railway (Rocket)", date: "7 February 1941");

        public static readonly Rotor GR_ETW = new Rotor(wiring: "QWERTZUIOASDFGHJKPYXCVBNML", name: "ETW",
            model: "German Railway (Rocket)", date: "7 February 1941");

        public static readonly Rotor I_K = new Rotor(wiring: "PEZUOHXSCVFMTBGLRINQJWAYDK", name: "I-K", model: "Swiss K",
            date: "February 1939");

        public static readonly Rotor II_K = new Rotor(wiring: "ZOUESYDKFWPCIQXHMVBLGNJRAT", name: "II-K", model: "Swiss K",
            date: "February 1939");

        public static readonly Rotor III_K = new Rotor(wiring: "EHRVXGAOBQUSIMZFLYNWKTPDJC", name: "III-K", model: "Swiss K",
            date: "February 1939");

        public static readonly Rotor ETW_K = new Rotor(wiring: "QWERTZUIOASDFGHJKPYXCVBNML", name: "ETW-K", model: "Swiss K",
            date: "February 1939");

        public static readonly Rotor I = new Rotor(wiring: "EKMFLGDQVZNTOWYHXUSPAIBRCJ", notches: "R", name: "I",
            model: "Enigma 1", date: "1930");

        public static readonly Rotor II = new Rotor(wiring: "AJDKSIRUXBLHWTMCQGZNPYFVOE", notches: "F", name: "II",
            model: "Enigma 1", date: "1930");

        public static readonly Rotor III = new Rotor(wiring: "BDFHJLCPRTXVZNYEIWGAKMUSQO", notches: "W", name: "III",
            model: "Enigma 1", date: "1930");

        public static readonly Rotor IV = new Rotor(wiring: "ESOVPZJAYQUIRHXLNFTGKDCMWB", notches: "K", name: "VI",
            model: "M3 Army", date: "December 1938");

        public static readonly Rotor V = new Rotor(wiring: "VZBRGITYUPSDNHLXAWMJQOFECK", notches: "A", name: "V",
            model: "M3 Army", date: "December 1938");

        public static readonly Rotor VI = new Rotor(wiring: "JPGVOUMFYQBENHZRDKASXLICTW", notches: "AN", name: "VI",
            model: "M3 & M4 Naval(February 1942)", date: "1939");

        public static readonly Rotor VII = new Rotor(wiring: "NZJHGRCXMYSWBOUFAIVLPEKQDT", notches: "AN", name: "VII",
            model: "M3 & M4 Naval(February 1942)", date: "1939");

        public static readonly Rotor VIII = new Rotor(wiring: "FKQHTLXOCBJSPDZRAMEWNIUYGV", notches: "AN", name: "VIII",
            model: "M3 & M4 Naval(February 1942)", date: "1939");

        public static readonly Rotor Beta = new Rotor(wiring: "LEYJVCNIXWPBQMDRTAKZGFUHOS", name: "Beta", model: "M4 R2",
            date: "Spring 1941");

        public static readonly Rotor Gamma = new Rotor(wiring: "FSOKANUERHMBTIYCWLQPZXVGJD", name: "Gamma", model: "M4 R2",
            date: "Spring 1941");

        public static readonly Rotor ETW = new Rotor(wiring: "ABCDEFGHIJKLMNOPQRSTUVWXYZ", name: "ETW", model: "Enigma 1");

        public static readonly Reflector A = new Reflector(wiring: "EJMZALYXVBWFCRQUONTSPIKHGD", name: "Reflector A");
        public static readonly Reflector B = new Reflector(wiring: "YRUHQSLDPXNGOKMIEBFZCWVJAT", name: "Reflector B");
        public static readonly Reflector C = new Reflector(wiring: "FVPJIAOYEDRZXWGCTKUQSBNMHL", name: "Reflector C");

        public static readonly Reflector B_Thin = new Reflector(wiring: "ENKQAUYWJICOPBLMDXZVFTHRGS",
            name: "Reflector_B_Thin", model: "M4 R1 (M3 + Thin)", date: "1940");

        public static readonly Reflector C_Thin = new Reflector(wiring: "RDOBJNTKVEHMLFCWZAXGYIPSUQ",
            name: "Reflector_C_Thin", model: "M4 R1 (M3 + Thin)", date: "1940");

        public static readonly Reflector GR_UKW = new Reflector(wiring: "QYHOGNECVPUZTFDJAXWMKISRBL", name: "UTKW",
            model: "German Railway (Rocket)", date: "7 February 1941");

        public static readonly Reflector UKW_K = new Reflector(wiring: "IMETCGFRAYSQBZXWLHKDVUPOJN", name: "UKW-K",
            model: "Swiss K", date: "February 1939");
    }
}
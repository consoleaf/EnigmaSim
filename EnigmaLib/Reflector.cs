using System;

namespace EnigmaLib
{
    /// <summary>
    /// Represents a reflector (aka UKW)
    /// </summary>
    public class Reflector
    {
        public class ReflectorEncodeArgs : EventArgs
        {
            public ReflectorEncodeArgs(char s)
            {
                msg = s;
            }
            private readonly char msg;
            public char Msg
            {
                get
                {
                    return msg;
                }
            }
        }
        public event EventHandler<ReflectorEncodeArgs> ReflectorEncodeEvent;

        private readonly string _wiring;
        private readonly string _name;
        private readonly string _model;
        private readonly string _date;
        public char State = 'A';

        public Reflector(
            String wiring = "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            String name = "N/A", String model = "N/A", String date = "N/A")
        {
            _wiring = wiring;
            _name = name;
            _model = model;
            _date = date;
        }

        public char Encipher(char key)
        {
            var shift = (State) - 'A';
            var index = (key - 'A' + 52) % 26;
            index = (index + shift + 52) % 26; // Actual connector hit

            char letter = _wiring[index]; // Rotor letter generated
            return (char) ('A' + (letter - 'A' + 52 - shift) % 26); // Actual output
        }

        public override string ToString()
        {
            return $"Reflector:\r\n\tName: {_name}\r\n\tModel: {_model}\r\n\tDate: {_date}\r\n\tWiring: {_wiring}";
        }
    }
}
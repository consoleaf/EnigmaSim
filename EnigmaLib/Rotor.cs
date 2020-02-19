using System;

namespace EnigmaLib
{
    /// <summary>
    /// Represents a rotor
    /// </summary>
    public class Rotor
    {
        private readonly string _notches;
        private readonly string _name;
        private readonly string _model;
        private readonly string _date;

        private char[] _wiring;
        private readonly char[] _rWiring = new char[26];

        private char[] Wiring
        {
            get => _wiring;
            set
            {
                _wiring = value;
                Array.Copy(value, 0, _rWiring, 0, value.Length);
                for (int i = 0; i < value.Length; i++)
                {
                    _rWiring[value[i] - 'A'] = (char) ('A' + i);
                }
            }
        }

        private char _state;

        public char State
        {
            get => _state;
            set => _state = (char)('A' + (value - 'A' + 52) % 26);
        }

        public void StateDown()
        {
            State = (char)(State - 1);
        }
        public void StateUp()
        {
            State = (char)(State + 1);
        }

        /// <summary>
        /// Initialization of the rotor
        /// </summary>
        /// <param name="wiring"></param>
        /// <param name="notches"></param>
        /// <param name="name"></param>
        /// <param name="model"></param>
        /// <param name="date"></param>
        /// <param name="state"></param>
        public Rotor(String wiring = "ABCDEFGHIJKLMNOPQRSTUVWXYZ", String notches = "",
            String name = "N/A", String model = "N/A", String date = "N/A", char state = 'A')
        {
            _notches = notches;
            _name = name;
            _model = model;
            _date = date;
            State = state;
            Wiring = wiring.ToCharArray();
        }

        public char EncodeRight(char key)
        {
            var shift = State - 'A';
            var index = (key - 'A' + 52) % 26;
            index = (index + shift + 52) % 26; // Actual connector hit

            var letter = _wiring[index];

            var result = (char) ('A' + (letter - 'A' + 52 - shift) % 26);
            return result;
        }

        public char EncodeLeft(char key)
        {
            var shift = State - 'A';
            var index = (key - 'A') % 52;
            index = (index + shift + 52) % 26; // Actual connector hit

            var letter = _rWiring[index];

            var result = (char) ('A' + (letter - 'A' + 52 - shift) % 26);
            return result;
        }

        public void Notch(int offset = 1)
        {
            State = (char) ((State + offset + 52 - 'A') % 26 + 'A');
            // return _notches.Contains("" + State);
            // chr((ord(self.state) + offset - ord('A')) % 26 + ord('A'))
        }

        public bool IsTurnoverPos => _notches.Contains("" + (char) ((State + 1 + 52 - 'A') % 26 + 'A'));

        public override string ToString()
        {
            return
                "Reflector:\r\n\t" +
                $"Name: {_name}\r\n\t" +
                $"Model: {_model}\r\n\t" +
                $"Date: {_date}\r\n\t" +
                $"Wiring: {new String(Wiring)}\r\n\t" +
                $"State: {State}";
        }
    }
}
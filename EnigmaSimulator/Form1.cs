using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EnigmaLib;

namespace EnigmaSimulator
{
    public partial class Form1 : Form
    {
        private Enigma enigma;
        private char activeKey;
        private char activeEnc;

        private string input = "";
        private string output = "";

        private char ETWForw(char ch)
        {
//            return (char)('A' + "QWERTZUIOASDFGHJKPYXCVBNML".IndexOf(ch));
            return ch;
        }

        private char ETWBackw(char ch)
        {
            return ch;
            return "QWERTZUIOASDFGHJKPYXCVBNML"[ch - 'A'];
        }

        public Form1()
        {
            this.enigma = new Enigma(PreMade.A, PreMade.III, PreMade.II,
                PreMade.I);
            InitializeComponent();
            UpdateGUITimer.Start();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            Debug.Assert(btn != null, nameof(btn) + " != null");
            ProcessKey(btn.Text[0]);
        }

        private void ProcessKey(char keyName)
        {
            this.activeKey = keyName;

            // Call enigma to encode it and light up the needed label
            this.activeEnc = ETWBackw(this.enigma.Encode("" + ETWForw(keyName)).ToUpper()[0]);

            input += activeKey;
            output += activeEnc;

            UpdateGUI();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            UpdateGUI();
        }

        private void UpdateGUI()
        {
            LabelRotorLeft.Text = "" + enigma._rotor3.State;
            LabelRotorMid.Text = "" + enigma._rotor2.State;
            LabelRotorRight.Text = "" + enigma._rotor1.State;
            
            if (this.activeEnc == 0)
                return;

            // Light up the correct label
            foreach (object control in Controls)
            {
                if (control is Label label)
                {
                    label.Font = new Font(label.Font, FontStyle.Regular);
                    label.BackColor = Color.Empty;
                }
            }

            if (Controls.Find("Label" + this.activeEnc, true)[0] is Label lbl)
            {
                lbl.Font = new Font(lbl.Font, FontStyle.Bold);
                lbl.BackColor = Color.Gold;
            }

            // Light up chosen button
            foreach (object control in Controls)
            {
                if (control is Button button)
                {
                    button.Font = new Font(button.Font, FontStyle.Regular);
                    button.BackColor = Color.Empty;
                }
            }

            if (Controls.Find("Button" + this.activeKey, true)[0] is Button btn)
            {
                btn.Font = new Font(btn.Font, FontStyle.Bold);
            }
            
            char tmp = this.activeKey;
            if (enigma._transTab.ContainsKey(tmp))
                tmp = enigma._transTab[tmp];
            
            LabelPostInput.Text = "" + tmp;
            LabelPreRight.Text = "" + tmp;

            tmp = enigma._rotor1.EncodeRight(tmp);
            LabelPreMid.Text = "" + tmp;

            tmp = enigma._rotor2.EncodeRight(tmp);
            LabelPreLeft.Text = "" + tmp;

            tmp = enigma._rotor3.EncodeRight(tmp);
            LabelPreReflector.Text = "" + tmp;

            tmp = enigma._reflector.Encipher(tmp);
            LabelPostReflector.Text = "" + tmp;

            tmp = enigma._rotor3.EncodeLeft(tmp);
            LabelPostLeft.Text = "" + tmp;

            tmp = enigma._rotor2.EncodeLeft(tmp);
            LabelPostMid.Text = "" + tmp;

            tmp = enigma._rotor1.EncodeLeft(tmp);
            LabelPostRight.Text = "" + tmp;
            LabelPreOutput.Text = "" + tmp;

            OutputTextBox.Text = output;
            InputTextBox.Text = input;
        }

        private void ButtonLeftUp_Click(object sender, EventArgs e)
        {
            enigma._rotor3.StateUp();
            UpdateGUI();
        }

        private void ButtonLeftDown_Click(object sender, EventArgs e)
        {
            enigma._rotor3.StateDown();
            UpdateGUI();
        }

        private void ButtonMidUp_Click(object sender, EventArgs e)
        {
            enigma._rotor2.StateUp();
            UpdateGUI();
        }

        private void ButtonMidDown_Click(object sender, EventArgs e)
        {
            enigma._rotor2.StateDown();
            UpdateGUI();
        }

        private void ButtonRightUp_Click(object sender, EventArgs e)
        {
            enigma._rotor1.StateUp();
            UpdateGUI();
        }

        private void ButtonRightDown_Click(object sender, EventArgs e)
        {
            enigma._rotor1.StateDown();
            UpdateGUI();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            input = "";
            output = "";
            UpdateGUI();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 'a' && e.KeyChar <= 'z')
            {
                char c = char.ToUpper(e.KeyChar);
                ProcessKey(c);
            }
            UpdateGUI();
        }
    }
}
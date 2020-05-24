using EnigmaLib;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace EnigmaSimulator
{
    public partial class Form1 : Form
    {
        private Save data = new Save();

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
            this.data.enigma = new Enigma(PreMade.A, PreMade.III, PreMade.II,
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
            this.data.activeKey = keyName;

            // Call enigma to encode it and light up the needed label
            this.data.activeEnc = ETWBackw(this.data.enigma.Encode("" + ETWForw(keyName)).ToUpper()[0]);

            data.input += data.activeKey;
            data.output += data.activeEnc;

            UpdateGUI();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            UpdateGUI();
        }

        private void SaveState(bool auto = false)
        {
            string path = @"Enigma.bin";
            if (auto)
            {
                path = @"Enigma.auto.bin";
            }

            try
            {
                Stream ms = File.OpenWrite(path);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, data);
                ms.Flush();
                ms.Close();
                ms.Dispose();
            }
            catch (Exception e)
            {
                if (!auto)
                    MessageBox.Show("Не удалось сохранить файл.");
            }
        }

        private void LoadState(bool auto = false)
        {
            string path = @"Enigma.bin";
            if (auto)
            {
                path = @"Enigma.auto.bin";
            }

            try
            {
                Stream ms = File.OpenRead(path);
                BinaryFormatter formatter = new BinaryFormatter();
                data = (Save)formatter.Deserialize(ms);
                panelTextBox.Text = data.plugs;
                ms.Flush();
                ms.Close();
                ms.Dispose();
            }
            catch (Exception e)
            {
                if (!auto)
                    MessageBox.Show("Не удалось загрузить файл!");
            }
        }

        private void UpdateGUI()
        {
            // Auto-save
            SaveState(auto: true);

            // Check if serialized file exists
            LoadButton.Enabled = File.Exists(@"Enigma.bin");

            LabelRotorLeft.Text = "" + data.enigma._rotor3.State;
            LabelRotorMid.Text = "" + data.enigma._rotor2.State;
            LabelRotorRight.Text = "" + data.enigma._rotor1.State;

            // Light up the correct label
            foreach (object control in Controls)
            {
                if (control is Label label)
                {
                    label.Font = new Font(label.Font, FontStyle.Regular);
                    label.BackColor = Color.Empty;
                }
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

            if (this.data.activeEnc == 0)
            {
                OutputTextBox.Text = data.output;
                InputTextBox.Text = data.input;
                LabelPostInput.Text = "N/A";
                LabelPreRight.Text = "N/A";
                LabelPreMid.Text = "N/A";
                LabelPreLeft.Text = "N/A";
                LabelPreReflector.Text = "N/A";
                LabelPostReflector.Text = "N/A";
                LabelPostLeft.Text = "N/A";
                LabelPostMid.Text = "N/A";
                LabelPostRight.Text = "N/A";
                LabelPreOutput.Text = "N/A";
                return;
            }

            if (Controls.Find("Label" + this.data.activeEnc, true)[0] is Label lbl)
            {
                lbl.Font = new Font(lbl.Font, FontStyle.Bold);
                lbl.BackColor = Color.Gold;
            }

            if (Controls.Find("Button" + this.data.activeKey, true)[0] is Button btn)
            {
                btn.Font = new Font(btn.Font, FontStyle.Bold);
            }

            char tmp = this.data.activeKey;
            if (data.enigma._transTab.ContainsKey(tmp))
                tmp = data.enigma._transTab[tmp];

            LabelPostInput.Text = "" + tmp;
            LabelPreRight.Text = "" + tmp;

            tmp = data.enigma._rotor1.EncodeRight(tmp);
            LabelPreMid.Text = "" + tmp;

            tmp = data.enigma._rotor2.EncodeRight(tmp);
            LabelPreLeft.Text = "" + tmp;

            tmp = data.enigma._rotor3.EncodeRight(tmp);
            LabelPreReflector.Text = "" + tmp;

            tmp = data.enigma._reflector.Encipher(tmp);
            LabelPostReflector.Text = "" + tmp;

            tmp = data.enigma._rotor3.EncodeLeft(tmp);
            LabelPostLeft.Text = "" + tmp;

            tmp = data.enigma._rotor2.EncodeLeft(tmp);
            LabelPostMid.Text = "" + tmp;

            tmp = data.enigma._rotor1.EncodeLeft(tmp);
            LabelPostRight.Text = "" + tmp;
            LabelPreOutput.Text = "" + tmp;

            OutputTextBox.Text = data.output;
            InputTextBox.Text = data.input;
        }

        private void ButtonLeftUp_Click(object sender, EventArgs e)
        {
            data.enigma._rotor3.StateUp();
            UpdateGUI();
        }

        private void ButtonLeftDown_Click(object sender, EventArgs e)
        {
            data.enigma._rotor3.StateDown();
            UpdateGUI();
        }

        private void ButtonMidUp_Click(object sender, EventArgs e)
        {
            data.enigma._rotor2.StateUp();
            UpdateGUI();
        }

        private void ButtonMidDown_Click(object sender, EventArgs e)
        {
            data.enigma._rotor2.StateDown();
            UpdateGUI();
        }

        private void ButtonRightUp_Click(object sender, EventArgs e)
        {
            data.enigma._rotor1.StateUp();
            UpdateGUI();
        }

        private void ButtonRightDown_Click(object sender, EventArgs e)
        {
            data.enigma._rotor1.StateDown();
            UpdateGUI();
        }

        private void Reset()
        {
            data.input = "";
            data.output = "";
            data.activeEnc = (char)0;
            data.activeKey = (char)0;
            data.plugs = "";
            UpdateGUI();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (panelTextBox.Focused)
                return;
            if (e.KeyChar >= 'a' && e.KeyChar <= 'z')
            {
                char c = char.ToUpper(e.KeyChar);
                ProcessKey(c);
            }
            UpdateGUI();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try { 
                string value = panelTextBox.Text.Trim().Replace('\n', ' ').ToUpper();
                data.plugs = value;
                data.enigma.SetPlugs(data.plugs);
            } catch (Exception ex)
            {
                MessageBox.Show("Не удалось применить настройки коммутационной панели. Проверьте правильность введённых данных.");
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            LoadState(auto: true); // Initial load if auto-saved.
            UpdateGUI();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveState();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadState();
        }
    }
}
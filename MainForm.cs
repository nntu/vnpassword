using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vnpassword
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        public static string ConvertToHexByteString(string hexInput)
        {
            if (string.IsNullOrEmpty(hexInput) || hexInput.Length % 2 != 0)
            {
                throw new ArgumentException("Invalid hex string. Must be non-empty and have even length.");
            }

            // Ensure the input is clean (remove any non-hex characters if needed, or validate)
            if (!hexInput.All(c => "0123456789ABCDEFabcdef".Contains(c)))
            {
                throw new ArgumentException("Input contains invalid hex characters.");
            }

            // Convert to byte array
            byte[] bytes = new byte[hexInput.Length / 2];
            for (int i = 0; i < hexInput.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hexInput.Substring(i, 2), 16);
            }

            // Format as comma-separated hex values
            return string.Join(",", bytes.Select(b => b.ToString("X2")));
        }

        private void bt_create_Click(object sender, EventArgs e)
        {
            var password = textBox1.Text;
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter a password.");
                return;
            }
            if (password.Length > 8)
            {
                MessageBox.Show("Password must not exceed 8 characters.");
                return;
            }
            try
            {
                var securePassword = new System.Security.SecureString();
                foreach (char c in password)
                {
                    securePassword.AppendChar(c);
                }
                securePassword.MakeReadOnly();
                var encryptedPassword = VncPasswordEncryptor.ConvertToEncryptedVncPassword(securePassword);
                textBox2.Text = BitConverter.ToString(encryptedPassword).Replace("-", "");
                textBox3.Text = ConvertToHexByteString(BitConverter.ToString(encryptedPassword).Replace("-", ""));
          
                string cmdCommand = $"net stop tvnserver \n";

                cmdCommand += $"reg add \"HKEY_LOCAL_MACHINE\\SOFTWARE\\TightVNC\\Server\" /v Password /t REG_BINARY /d {textBox2.Text} /f \n";
                
                cmdCommand += "net start tvnserver \n";



                richTextBox1.Text = cmdCommand;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

        }
    }
}

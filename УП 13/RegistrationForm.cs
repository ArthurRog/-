using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace УП_13
{
    public partial class RegistrationForm : Form
    {
        BinaryFileAccessor accessor;
        static Random rnd = new Random();
        public RegistrationForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            accessor = new BinaryFileAccessor("Users.bin");
            textBox2.Text = CreateRandomPassword();
            textBox3.Text = CreateRandomPassword();
            textBox4.Text = CreateRandomPassword();
            textBox6.KeyPress += textBox6_KeyPress_1;
        }


        public string CreateRandomPassword()
        {
            char[] password = new char[rnd.Next(7, 12)];
            password[0] = (char)rnd.Next(48, 120);
            if (Char.IsSeparator(password[0]) || Char.IsWhiteSpace(password[0]))
            {
                while (Char.IsSeparator(password[0]) || Char.IsWhiteSpace(password[0]))
                {
                    password[0] = (char)rnd.Next(48, 120);
                }
            }
            for (int i = 1; i < password.Length; i++)
            {
                password[i] = (char)rnd.Next(48, 120);
            }
            return String.Join("", password);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (accessor.RegistrateUser(textBox1.Text, textBox5.Text, textBox2.Text, textBox3.Text, textBox4.Text, Convert.ToInt32(textBox6.Text)))
            {
                MessageBox.Show("Регистрация прошла успешно");
            }
            else
            {
                MessageBox.Show("Данный пользователь уже зарегистрирован");
            }

            this.Close();
        }



        private void textBox6_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar)) return;
            else
                e.Handled = true;
        }
    }
}

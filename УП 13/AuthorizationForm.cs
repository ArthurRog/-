using Microsoft.VisualBasic;
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
    public partial class AuthorizationForm : Form
    {
        BinaryFileAccessor accessor;
        public AuthorizationForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            accessor = new BinaryFileAccessor("Users.bin");
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (accessor.AuthorizateUser(textBox1.Text, textBox2.Text))
            {

                this.Hide();
                MainForm mainForm = new MainForm();
                mainForm.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string number = Interaction.InputBox("Введите номер телефона", "восстановление пароля", "");
            accessor.GetPhoneNumber(number);
        }

        private void button1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                button2.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
            }
        }
    }
}

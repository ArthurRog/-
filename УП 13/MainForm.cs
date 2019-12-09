using MySql.Data.MySqlClient;
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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            ButtonAddData.Click += AddData;
            DeleteButton.Click += DeleteData;
            UpdateDataButton.Click += UpdateData;
        }

        public void UpdateData(object sender, EventArgs e)
        {
            DataBaseSQL dataBase = new DataBaseSQL();

            MySqlCommand command = new MySqlCommand("UPDATE product SET Name = @Name, KindOfSport = @KindOfSport, Manufacturer = @Manufacturer, Price= @Price, QuantityInStock = @QuantityInStock WHERE id = @id", dataBase.getConnection());
            command.Parameters.Add("@id", MySqlDbType.VarChar).Value = textBoxId.Text;

            command.Parameters.Add("@Name", MySqlDbType.VarChar).Value = textBoxUpdateName.Text;
            command.Parameters.Add("@KindOfSport", MySqlDbType.VarChar).Value = textBoxUpdateKindOfSport.Text;
            command.Parameters.Add("@Manufacturer", MySqlDbType.VarChar).Value = textBoxUpdateManufacturer.Text;
            command.Parameters.Add("@Price", MySqlDbType.VarChar).Value = textBoxUpdatePrice.Text;
            command.Parameters.Add("@QuantityInStock", MySqlDbType.VarChar).Value = textBoxUpdateQuantityInStock.Text;
            dataBase.openConnection();

            command.ExecuteNonQuery();

            dataBase.closeConnection();
        }

        public void AddData(object sender, EventArgs e)
        {
            if (textBoxName.Text != "" && textBoxKindOfSport.Text != "" && textBoxManufacturer.Text != "" &&
                textBoxPrice.Text != "" && textBoxQuantityInStock.Text != "")
            {
                DataBaseSQL dataBase = new DataBaseSQL();
                MySqlCommand command = new MySqlCommand("INSERT INTO product (Name, KindOfSport, Manufacturer, Price, QuantityInStock) VALUES (@Name, @KindOfSport, @Manufacturer, @Price, @QuantityInStock)", dataBase.getConnection());

                command.Parameters.Add("@Name", MySqlDbType.VarChar).Value = textBoxName.Text;
                command.Parameters.Add("@KindOfSport", MySqlDbType.VarChar).Value = textBoxKindOfSport.Text;
                command.Parameters.Add("@Manufacturer", MySqlDbType.VarChar).Value = textBoxManufacturer.Text;
                command.Parameters.Add("@Price", MySqlDbType.VarChar).Value = textBoxPrice.Text;
                command.Parameters.Add("@QuantityInStock", MySqlDbType.VarChar).Value = textBoxQuantityInStock.Text;

                dataBase.openConnection();

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Данные добавленны");
                }
                else MessageBox.Show("Данные не добавленны");

                dataBase.closeConnection();
            }
            else MessageBox.Show("Данные введены некорректно");
        }

        public void DeleteData(object sender, EventArgs e)
        {
            DataBaseSQL dataBase = new DataBaseSQL();
            dataBase.openConnection();

            MySqlCommand command = new MySqlCommand("DELETE FROM product WHERE id = @id", dataBase.getConnection());
            command.Parameters.Add("@id", MySqlDbType.VarChar).Value = DeleteTextBox1.Text;

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Данные удалены");
            }
            else MessageBox.Show("Данные не удалены");

            dataBase.closeConnection();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}

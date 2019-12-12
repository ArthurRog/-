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
            ButtonSearch.Click += SearchData;
            ButtonOrderBy.Click += OrderByData;
        }
        

        public void OrderByData(object sender, EventArgs e)
        {
            string sql = "";

            if (ListOrderBy.Text == "id")
            {
                sql = "SELECT* FROM product ORDER BY id";
            }

            if (ListOrderBy.Text == "Название")
            {
                sql = "SELECT* FROM product ORDER BY Name";
            }

            if (ListOrderBy.Text == "Вид спорта")
            {
                sql = "SELECT* FROM product ORDER BY KindOfSport";
            }

            if (ListOrderBy.Text == "Производитель")
            {
                sql = "SELECT* FROM product ORDER BY Manufacturer";
            }

            if (ListOrderBy.Text == "Цена")
            {
                sql = "SELECT* FROM product ORDER BY Price";
            }

            if (ListOrderBy.Text == "Количество")
            {
                sql = "SELECT* FROM product ORDER BY QuantityInStock";
            }

            DataBaseSQL dataBase = new DataBaseSQL();
            MySqlCommand command = new MySqlCommand(sql, dataBase.getConnection());

            dataBase.openConnection();

            MySqlDataReader reader = command.ExecuteReader();
            OutputData.Clear();
            while (reader.Read())
            {
                OutputData.AppendText("id: " + reader[0].ToString()
                    + "\nНазвание: " + reader[1].ToString()
                    + "\nВид спорта: " + reader[2].ToString()
                    + "\nПроизводитель: " + reader[3].ToString()
                    + "\nЦена: " + reader[4].ToString()
                    + "\nКоличество : " + reader[5].ToString() + "\n\n");
            }
            dataBase.closeConnection();

        }

        public void SearchData(object sender, EventArgs e)
        {
            if (ListFields.Text != "" && SearchTextBox.Text != "")
            {

                string sql = "";

                if (ListFields.Text == "id")
                {
                    sql = "SELECT * FROM product WHERE id = @Search";
                }

                if (ListFields.Text == "Название")
                {
                    sql = "SELECT * FROM product WHERE Name = @Search";
                }

                if (ListFields.Text == "Вид спорта")
                {
                    sql = "SELECT * FROM product WHERE KindOfSport = @Search";
                }

                if (ListFields.Text == "Производитель")
                {
                    sql = "SELECT * FROM product WHERE Manufacturer = @Search";
                }

                if (ListFields.Text == "Цена")
                {
                    sql = "SELECT * FROM product WHERE Price = @Search";
                }

                if (ListFields.Text == "Количество")
                {
                    sql = "SELECT * FROM product WHERE QuantityInStock = @Search";
                }

                GetInfo(sql);
            }
            else
                MessageBox.Show("Введите данные");

        }

        public void GetInfo(string sql)
        {
            DataBaseSQL dataBase = new DataBaseSQL();
            MySqlCommand command = new MySqlCommand(sql, dataBase.getConnection());

            command.Parameters.Add("@Search", MySqlDbType.VarChar).Value = SearchTextBox.Text;

            dataBase.openConnection();

            MySqlDataReader reader = command.ExecuteReader();
            OutputData.Clear();
            while (reader.Read())
            {
                OutputData.AppendText("id: " + reader[0].ToString()
                    + "\nНазвание: " + reader[1].ToString()
                    + "\nВид спорта: " + reader[2].ToString()
                    + "\nПроизводитель: " + reader[3].ToString()
                    + "\nЦена: " + reader[4].ToString()
                    + "\nКоличество : " + reader[5].ToString() + "\n\n");
            }
            dataBase.closeConnection();
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
            GetInfo("SELECT * FROM product");
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            GetInfo("SELECT * FROM product");
        }
    }
}

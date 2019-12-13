using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace УП_13
{
    class BinaryFileAccessor
    {
        string _fileName;
        List<User> users;

        public BinaryFileAccessor(string fileName)
        {
            _fileName = fileName;
        }

        private void ReadUserFile()
        {
            using (FileStream sr = new FileStream(_fileName, FileMode.OpenOrCreate))
            {
                try
                {
                    users = new BinaryFormatter().Deserialize(sr) as List<User>;
                }
                catch
                {
                    users = new List<User>();
                }
            }
        }

        private void Serializer()
        {
            using (FileStream sr = new FileStream(_fileName, FileMode.Open))
            {
                new BinaryFormatter().Serialize(sr, users);
            }
        }

        public bool AuthorizateUser(string login, string password)
        {
            ReadUserFile();
            if (users.Count(x => x.Login == login) != 0)
            {
                User checkedUser = users.Where(x => x.Login == login).First();
                if (checkedUser.FirstPassword.Equals(password) || checkedUser.SecondPassword.Equals(password) ||
                    checkedUser.ThirdPassword.Equals(password))
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool RegistrateUser(string fio, string login, string firstPassword, string secondPassword, string thirdPassword, int phoneNumber)
        {
            ReadUserFile();
            if (users.Count(x => x.Login == login) == 0)
            {
                User checkedUser = new User(fio, login, firstPassword, secondPassword, thirdPassword, phoneNumber);
                users.Add(checkedUser);
                Serializer();
                return true;
            }
            return false;
        }

        public void GetPhoneNumber(string phoneNumber)
        {
            ReadUserFile();
            for (int i = 0; i < phoneNumber.Length; i++)
            {
                if (!Char.IsDigit(phoneNumber[i]))
                {
                    System.Windows.Forms.MessageBox.Show("Неверный формат номера");
                    return;
                }
            }
            if (users.Count(x => x.Phone.Equals(Convert.ToInt32(phoneNumber))) != 0)
            {
                User checkedUser = users.Where(x => x.Phone.Equals(Convert.ToInt32(phoneNumber))).First();
                //System.Windows.Forms.MessageBox.Show($"{checkedUser.FirstPassword}\n{checkedUser.SecondPassword}\n{checkedUser.ThirdPassword}");
                System.Windows.Forms.MessageBox.Show($"{checkedUser.Login}");
            }
        }
    }
}

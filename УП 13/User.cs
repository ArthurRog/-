using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace УП_13
{
    [Serializable]
    class User
    {
        private string _fio;
        private string _login;
        private string _firstPassword;
        private string _secondPassword;
        private string _thirdPassword;
        private int _phone;

        public User(string fio, string login, string firstPassword, string secondPassword, string thirdPassword, int phone)
        {
            _fio = fio;
            _login = login;
            _firstPassword = firstPassword;
            _secondPassword = secondPassword;
            _thirdPassword = thirdPassword;
            _phone = phone;
        }

        public string Fio { get => _fio; set => _fio = value; }
        public string Login { get => _login; set => _login = value; }
        public string FirstPassword { get => _firstPassword; set => _firstPassword = value; }
        public string SecondPassword { get => _secondPassword; set => _secondPassword = value; }
        public string ThirdPassword { get => _thirdPassword; set => _thirdPassword = value; }
        public int Phone { get => _phone; set => _phone = value; }

        public override string ToString()
        {
            return $@"ФИО: {_fio}
Login {_login}
FirstPassword {_firstPassword}
SecondPassword {_secondPassword}
ThirdPassword {_thirdPassword}
Phone Number {_phone}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoBidder.Requests
{
    class Login
    {
        private const string LOGIN_URL = "";

        public string Username = "";
        public string Password = "";
        public string SecretQuestionHash = "";

        public Login(string un, string pw, string sqh)
        {
            Username = un;
            Password = pw;
            SecretQuestionHash = sqh;
        }


    }
}

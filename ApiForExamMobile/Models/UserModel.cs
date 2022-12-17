using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiForExamMobile.Models
{
    public class UserModel
    {
        public UserModel(Users users)
        {
            ID = users.ID;
            Name = users.Name ?? throw new ArgumentNullException(nameof(users.Name));
            Login = users.Login ?? throw new ArgumentNullException(nameof(users.Login));
            Password = users.Password ?? throw new ArgumentNullException(nameof(users.Password));
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
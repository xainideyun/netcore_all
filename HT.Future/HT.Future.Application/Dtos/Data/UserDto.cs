using HT.Future.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HT.Future.Application
{
    public class UserDto
    {
        public int User_id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public string Avator { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public DateTime? LastLoginTime { get; set; }

    }
}

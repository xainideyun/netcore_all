﻿using HT.Future.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HT.Future.Application
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

    }
}

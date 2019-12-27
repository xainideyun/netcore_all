using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HT.Future.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HT.Future.TxApi.Controllers
{
    public class UserController : BaseController
    {
        public UserController(IMapper mapper) : base(mapper)
        {
        }



    }
}
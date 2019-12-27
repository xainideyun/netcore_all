using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace HT.Future.Application
{
    [Produces("application/json")]
    [ApiController]
    [ApiResultFilter]
    //[Route("api/v{version:apiVersion}/[controller]")]// api/v1/[controller]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        protected IMapper _mapper;
        public BaseController(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}

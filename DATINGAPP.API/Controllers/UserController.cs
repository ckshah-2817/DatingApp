using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DATINGAPP.API.Data;
using DATINGAPP.API.Dtos;
using DATINGAPP.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DATINGAPP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IDatingRepository _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public UserController(IDatingRepository repository,IConfiguration config,IMapper mapper)
        {
            _repo=repository;
            _config = config;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers(){

           var users= await this._repo.GetUsers();
           var usersForList = this._mapper.Map<IEnumerable<UserForListDtos>>(users);
           return Ok(usersForList);
        }
        [HttpGet ("{id}")]
        public async Task<IActionResult> GetUser(int id){
            
              var user= await this._repo.GetUser(id);
              var usersForDetails = this._mapper.Map<UserDetailsForDtos>(user);
              return Ok(usersForDetails);
        }

    }
}
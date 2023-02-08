using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using magix_api.Dtos.Player;
using magix_api.Models;
using magix_api.Services.PlayerService;
using Microsoft.AspNetCore.Mvc;

namespace magix_api.Controllers
{
    [ApiController] //This means this is an API so it implements all the api's tools
    [Route("api/[controller]")] // This means the controller can be accessed at the route api/The_name_of_this_controller
    public class PlayerController : ControllerBase //It must extends the ControllerBase's class from MVC 
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<GetPlayerDto>>> Login(LoginPlayerDto userInfos)
        {
            return Ok(await _playerService.Login(userInfos));
        }

        [HttpGet("GetAll")] // Create a sub-route at api/Player/GetAll
        // Could also, instead of adding ("GetAll") in the httpGet attribute, I could add [Route("GetAll")] on line under to define a "sub-route"
        public async Task<ActionResult<ServiceResponse<List<GetPlayerDto>>>> Get()
        {
            return Ok(await _playerService.GetAllPlayers());
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<ServiceResponse<GetPlayerDto>>> GetSingle(string username)
        {
            return Ok(await _playerService.GetPlayerByUsername(username));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetPlayerDto>>>> AddPlayer(AddPlayerDto newPlayer)
        {
            return Ok(await _playerService.AddPlayer(newPlayer));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetPlayerDto>>>> UpdatePlayer(UpdatePlayerDto updatedPlayer)
        {
            var response = await _playerService.UpdatePlayer(updatedPlayer);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetPlayerDto>>>> DeletePlayer(int id)
        {
            var response = await _playerService.DeletePlayer(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

    }
}
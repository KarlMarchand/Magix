﻿using magix_api.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace magix_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        [HttpGet]
        [Authorize(Policy = "ValidateKey")]
        public ActionResult<string> GetChat()
        {
            string key = User.GetPlayerKey();
            return Ok("https://magix.apps-de-cours.com/server/#/chat/" + key);
        }
    }
}

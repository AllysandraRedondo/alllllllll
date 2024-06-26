using Microsoft.AspNetCore.Mvc;
using BUSINESSLOGICdb;
using MODELSdb;
using System.Collections.Generic;


namespace OwnerAPIController.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OwnerAPIController : ControllerBase
    {
        private readonly VerifyingUser _verifyingUser;
        private readonly AnimeProcess _animeProcess;

        public OwnerAPIController(VerifyingUser verifyingUser, AnimeProcess animeProcess)
        {
            _verifyingUser = verifyingUser;
            _animeProcess = animeProcess;
        }

        [HttpGet("users")]
        public IEnumerable<OwnerContent> GetAllUsers()
        {
            return _verifyingUser.GetAllUsers();
        }

        [HttpGet("anime")]
        public IEnumerable<AnimeContent> GetAllAnime()
        {
            return _animeProcess.GetAllAnime();
        }

        [HttpPost("login")]
        public ActionResult<OwnerContent> Login([FromBody] OwnerContent ownerContent)
        {
            var user = _verifyingUser.VerifysUser(ownerContent.username, ownerContent.password);
            if (user != null)
            {
                return Ok(user);
            }
            return Unauthorized();
        }

        [HttpPost("addAnime")]
        public ActionResult<int> AddAnime([FromBody] AnimeContent animeContent)
        {
            var result = _animeProcess.AddAnime(animeContent);
            return Ok(result);
        }

        [HttpDelete("removeAnime/{title}")]
        public ActionResult<int> RemoveAnime(string title)
        {
            var result = _animeProcess.RemoveAnime(title);
            return Ok(result);
        }

        [HttpGet("searchAnime")]
        public IEnumerable<AnimeContent> SearchAnime([FromQuery] string keyword)
        {
            return _animeProcess.SearchAnime(keyword);
        }
    }
}

using derivco.Data.Models;
using derivco.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace derivco.Controllers.PlayerController
{
    [ApiController]
    [Route("[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly ILogger<PlayersController> _logger;
        private readonly IPlayerRepository playerRepository;

        public PlayersController(ILogger<PlayersController> logger, IPlayerRepository playerRepository)
        {
            _logger = logger;
            this.playerRepository = playerRepository;
        }

        [HttpGet]
        public async Task<List<Player>> Get()
        {
            return await this.playerRepository.GetAll();
        }

        [HttpGet]
        public async Task<List<Player>> GetById(Guid id)
        {
            return await this.playerRepository.GetAll(id);
        }

        [HttpGet]
        public Player Create()
        {
            return new Player()
            {
                CreatedAt = DateTime.Now,
                Id = Guid.NewGuid()
            };
        }

        [HttpPost]
        public async Task Add(Player Player)
        {
            await this.playerRepository.Insert(Player);
        }
    }
}

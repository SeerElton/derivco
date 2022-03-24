using derivco.Data.Models;
using derivco.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace derivco.Controllers.BetController
{
    [ApiController]
    [Route("[controller]")]
    public class BetsController : ControllerBase
    {
        private readonly ILogger<BetsController> _logger;
        private readonly IBetRepository betRepository;

        public BetsController(ILogger<BetsController> logger, IBetRepository betRepository)
        {
            _logger = logger;
            this.betRepository = betRepository;
        }

        [HttpGet]
        public async Task<List<Bet>> Get()
        {
            return await this.betRepository.GetAll();
        }

        [HttpGet]
        public async Task<List<Bet>> GetById(Guid id)
        {
            return await this.betRepository.GetAll(id);
        }

        [HttpPost]
        public async Task Add(Bet Bet)
        {
            Bet.Id = Guid.NewGuid();
            await this.betRepository.Insert(Bet);
        }
    }
}

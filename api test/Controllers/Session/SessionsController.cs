using derivco.Data.Models;
using derivco.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace derivco.Controllers.SessionController
{
    [ApiController]
    [Route("[controller]")]
    public class SessionsController : ControllerBase
    {
        private readonly ILogger<SessionsController> _logger;
        private readonly ISessionRepository sessionRepository;

        public SessionsController(ILogger<SessionsController> logger, ISessionRepository sessionRepository)
        {
            _logger = logger;
            this.sessionRepository = sessionRepository;
        }

        [HttpGet]
        public async Task<List<Session>> Get()
        {
            return await this.sessionRepository.GetAll();
        }

        [HttpGet]
        public async Task<List<Session>> GetById(Guid id)
        {
            return await this.sessionRepository.GetAll(id);
        }

        [HttpGet]
        public Session Create()
        {
            return new Session()
            {
                CreatedAt = DateTime.Now,
                Id = Guid.NewGuid()
            };
        }

        [HttpGet]
        public async Task<List<Payout>> GetPayouts()
        {
            return await this.sessionRepository.Payouts();
        }

        [HttpGet]
        public async Task<List<Payout>> GetPayoutById(Guid id)
        {
            return await this.sessionRepository.Payouts(id);
        }

        [HttpPost]
        public async Task Add(Session session)
        {
            await this.sessionRepository.Insert(session);
        }
    }
}

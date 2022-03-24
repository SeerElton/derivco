using derivco.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace derivco.Data.Repositories
{
    public interface ISessionRepository
    {
        public Task<List<Session>> GetAll(Guid? id = null);

        public Task Insert(Session session);

        public Task<List<Payout>> Payouts(Guid? id = null);
    }
}

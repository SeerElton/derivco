using derivco.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace derivco.Data.Repositories
{
    public interface IBetRepository
    {
        public Task<List<Bet>> GetAll(Guid? id = null);

        public Task Insert(Bet bet);
    }
}

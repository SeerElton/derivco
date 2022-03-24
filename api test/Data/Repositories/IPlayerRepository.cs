using derivco.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace derivco.Data.Repositories
{
    public interface IPlayerRepository
    {
        public Task<List<Player>> GetAll(Guid? id = null);

        public Task Insert(Player bet);
    }
}

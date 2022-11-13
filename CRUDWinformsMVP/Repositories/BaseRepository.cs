using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDWinFormsMVP.Repositories
{
    public abstract class BaseRepository
    {
        protected string connectionString;

        public BaseRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
    }
}

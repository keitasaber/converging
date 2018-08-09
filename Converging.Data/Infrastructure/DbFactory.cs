using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converging.Data.Infrastructure
{
    public class DbFactory : IDbFactory
    {
        private ConveringDbContext dbContext;

        public void Dispose()
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
        }

        public ConveringDbContext Init()
        {
            return dbContext ?? (dbContext = new ConveringDbContext());
        }
    }
}

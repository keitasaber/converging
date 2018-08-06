using Converging.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converging.Data.Repositories
{
    public interface IMenuGroup : IRepositoryBase<MenuGroup>
    {

    }
    public class MenuGroup : RepositoryBase<MenuGroup>, IMenuGroup
    {
        public MenuGroup(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}

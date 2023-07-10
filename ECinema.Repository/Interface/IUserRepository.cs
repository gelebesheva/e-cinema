using ECinema.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECinema.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<ShoppApplicationUser> GetAll();
        ShoppApplicationUser Get(string id);
        void Insert(ShoppApplicationUser entity);
        void Update(ShoppApplicationUser entity);
        void Delete(ShoppApplicationUser entity);
    }
}

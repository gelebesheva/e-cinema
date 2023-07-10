using ECinema.Domain.Identity;
using ECinema.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECinema.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<ShoppApplicationUser> entities;
        string errorMessage = string.Empty;
        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<ShoppApplicationUser>();
        }

        public void Delete(ShoppApplicationUser entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public ShoppApplicationUser Get(string id)
        {
            var user = entities.Include(z => z.UserShoppingCart).
                Include("UserShoppingCart.TicketsInShoppingCart").
                Include("UserShoppingCart.TicketsInShoppingCart.Ticket").
                SingleOrDefault(x => x.Id == id);
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return user;
        }

        public IEnumerable<ShoppApplicationUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public void Insert(ShoppApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(ShoppApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }
    }
}

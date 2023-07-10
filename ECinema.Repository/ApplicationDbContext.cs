using ECinema.Domain.Domain_models;
using ECinema.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECinema.Repository
{
    public class ApplicationDbContext : IdentityDbContext<ShoppApplicationUser>
    {
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<TicketsInShoppingCart> TicketsInShoppingCart { get; set; }
        public virtual DbSet<ShoppApplicationUser> ShoppApplicationUsers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<TicketsInOrder> TicketsInOrder { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<TicketsInShoppingCart>().HasKey(c => new { c.CartId, c.TicketId });
            builder.Entity<TicketsInOrder>().HasKey(c => new { c.OrderId, c.TicketId });
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}

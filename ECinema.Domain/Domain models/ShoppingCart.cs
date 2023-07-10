using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECinema.Domain.Domain_models
{
    public class ShoppingCart : BaseEntity
    {
        public string ApplicationUserId { get; set; }
        public ICollection<TicketsInShoppingCart> TicketsInShoppingCart { get; set; }
    }
}

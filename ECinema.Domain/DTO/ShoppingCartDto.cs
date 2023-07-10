using ECinema.Domain.Domain_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECinema.Domain.DTO
{
    public class ShoppingCartDto
    {
        public List<TicketsInShoppingCart> TicketsInShoppingCart { get; set; }
        public float TotalPrice { get; set; }
    }
}

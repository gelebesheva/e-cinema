using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECinema.Domain.Domain_models
{
    public class Ticket : BaseEntity
    {
        [Required]
        public string MovieTitle { get; set; }
        [Required]
        public string Poster { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [Required]
        public float Price { get; set; }
        public ICollection<TicketsInShoppingCart> TicketsInShoppingCart { get; set; }
    }
}

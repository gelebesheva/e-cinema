using ECinema.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECinema.Domain.Domain_models
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public ShoppApplicationUser OrderedBy { get; set; }
        public List<TicketsInOrder> Tickets { get; set; }
    }
}

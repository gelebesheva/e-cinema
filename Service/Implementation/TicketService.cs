using ECinema.Domain.Domain_models;
using ECinema.Domain.DTO;
using ECinema.Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Implementation
{
    public class TicketService : ITicketService
    {
        public readonly IUserRepository _userRepository;
        public readonly IRepository<TicketsInShoppingCart> _ticketInShoppingCartRepository;
        public readonly IRepository<Ticket> _ticketRepository;

        public TicketService(IRepository<Ticket> ticketRepository, IUserRepository userRepository,
            IRepository<TicketsInShoppingCart> ticketInShoppingCartRepository)
        {
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
            _ticketInShoppingCartRepository = ticketInShoppingCartRepository;
        }

        public bool AddToShoppingCart(AddToShoppingCartDto item, string userID)
        {
            var user = this._userRepository.Get(userID);

            var userShoppingCart = user.UserShoppingCart;

            if (userShoppingCart != null)
            {
                var ticket = this.GetDetailsForTicket(item.TicketId);

                if (ticket != null)
                {
                    TicketsInShoppingCart itemToAdd = new TicketsInShoppingCart
                    {
                        Ticket = ticket,
                        TicketId = ticket.Id,
                        ShoppingCart = userShoppingCart,
                        CartId = userShoppingCart.Id,
                        Quantity = item.Quantity
                    };

                    _ticketInShoppingCartRepository.Insert(itemToAdd);
                    return true;
                }
                return false;
            }
            return false;
        }

        public void CreateNewMovie(Ticket t)
        {
            this._ticketRepository.Insert(t);
        }

        public void DeleteMovie(int id)
        {
            var movie = _ticketRepository.Get(id);
            this._ticketRepository.Delete(movie);
        }

        public List<Ticket> GetAllMovies()
        {
            return _ticketRepository.GetAll().ToList();
        }

        public Ticket GetDetailsForTicket(int id)
        {
            return _ticketRepository.Get(id);
        }

        public ShoppingCartDto GetShoppingCartInfo(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateExistingMovie(Ticket t)
        {
            _ticketRepository.Update(t);
        }
    }
}

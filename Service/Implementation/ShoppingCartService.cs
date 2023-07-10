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
    public class ShoppingCartService : IShoppingCartService
    {
        public readonly IUserRepository _userRepository;
        public readonly IRepository<TicketsInOrder> _ticketsInOrderRepository;
        public readonly IRepository<ShoppingCart> _shoppingCartRepository;
        public readonly IRepository<Order> _orderRepository;

        public ShoppingCartService(IUserRepository userRepository, IRepository<TicketsInOrder> ticketsInOrderRepository,
            IRepository<ShoppingCart> shoppingCartRepository, IRepository<Order> orderRepository)
        {
            _userRepository = userRepository;
            _ticketsInOrderRepository = ticketsInOrderRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _orderRepository = orderRepository;
    }

        public bool deleteTicketFromShoppingCart(string userId, int ticketId)
        {
            if(!string.IsNullOrEmpty(userId) && ticketId != null)
            {
                var loggInUser = _userRepository.Get(userId);
                var userShoppingCart = loggInUser.UserShoppingCart;
                var itemToDelete = userShoppingCart.TicketsInShoppingCart.Where(z => z.TicketId == ticketId).FirstOrDefault();
                userShoppingCart.TicketsInShoppingCart.Remove(itemToDelete);
                _shoppingCartRepository.Update(userShoppingCart);
                return true;
            }
            else {
                return false;
            }
        }

        public ShoppingCartDto getShoppingCartInfo(string userId)
        {
            var user = _userRepository.Get(userId);

            var userShoppingCart = user.UserShoppingCart;

            var ticketList = userShoppingCart.TicketsInShoppingCart.Select(z => new
            {
                Quantity = z.Quantity,
                TicketPrice = z.Ticket.Price
            });

            float totalPrice = 0;
            foreach (var item in ticketList)
            {
                totalPrice += item.TicketPrice * item.Quantity;
            }

            ShoppingCartDto model = new ShoppingCartDto
            {
                TotalPrice = totalPrice,
                TicketsInShoppingCart = userShoppingCart.TicketsInShoppingCart.ToList()
            };

            return model;

        }

        public bool orderNow(string userId)
        {
            var user = _userRepository.Get(userId);

            var userShoppingCart = user.UserShoppingCart;

            Order newOrder = new Order
            {
                UserId = user.Id,
                OrderedBy = user
            };
            _orderRepository.Insert(newOrder);

            List<TicketsInOrder> ticketsInOrder = userShoppingCart.TicketsInShoppingCart.Select(z => new TicketsInOrder
            {
                Ticket = z.Ticket,
                TicketId = z.TicketId,
                Order = newOrder,
                OrderId = newOrder.Id,
                Quantity = z.Quantity
            }).ToList();

            foreach (var item in ticketsInOrder)
            {
                _ticketsInOrderRepository.Insert(item);
            }
            user.UserShoppingCart.TicketsInShoppingCart.Clear();
            _userRepository.Update(user);
            return true;
        }
    }
}

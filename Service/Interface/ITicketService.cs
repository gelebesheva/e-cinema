using ECinema.Domain.Domain_models;
using ECinema.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface
{
    public interface ITicketService
    {
        List<Ticket> GetAllMovies();
        Ticket GetDetailsForTicket(int id);
        void CreateNewMovie(Ticket t);
        void UpdateExistingMovie(Ticket t);
        ShoppingCartDto GetShoppingCartInfo(int id);
        void DeleteMovie(int id);
        bool AddToShoppingCart(AddToShoppingCartDto item, string userID);
    }
}

using Application.Dtos.Books;
using Domain.Entities.Reservations;

namespace Application.Features.Definitions.Books
{
    public interface IReservationBookService
    {
        
            Task<string> AddReservationAsync(ReservationDto reservationDto);
            Task<string> Remove(long id);
            Task<List<ReservationDto>> GetAllReservations();
            Task<IEnumerable<Reservation>> GetAllBooksAsync(CancellationToken cancellationToken = default);
            Task AutoReleaseExpiredReservationsAsync();
        

    }
}

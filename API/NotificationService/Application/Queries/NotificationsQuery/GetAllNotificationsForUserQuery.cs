using MediatR;
using Domain.Models;

namespace Application.Queries.NotificationsQuery
{
    public class GetAllNotificationsForUserQuery(Guid userID) : IRequest<List<Notification>>
    {
        public Guid UserID { get; } = userID;
    }
}

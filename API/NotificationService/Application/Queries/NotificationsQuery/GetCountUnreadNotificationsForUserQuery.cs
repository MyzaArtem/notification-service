using MediatR;

namespace Application.Queries.NotificationsQuery
{
    public class GetCountUnreadNotificationsForUserQuery(Guid userID) : IRequest<int>
    {
        public Guid UserID { get; } = userID;
    }
}
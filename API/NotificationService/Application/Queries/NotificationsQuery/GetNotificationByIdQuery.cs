using MediatR;
using Domain.Models;

namespace Application.Queries.NotificationsQuery
{
    public class GetNotificationByIdQuery(Guid id) : IRequest<Notification>
    {
        public Guid Id { get; } = id;
    }
}

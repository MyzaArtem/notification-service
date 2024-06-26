using Application.Queries.NotificationsQuery;
using MediatR;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Enums;

namespace Infrastructure.Handlers.NotificationHandler
{
    public class GetAllNotificationsForUserHandler : IRequestHandler<GetAllNotificationsForUserQuery, List<Notification>>
    {
        private readonly AppDbContext _appDbContext;
        public GetAllNotificationsForUserHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<Notification>> Handle(GetAllNotificationsForUserQuery request, CancellationToken cancellationToken)
            => await _appDbContext.Notifications.Where(n => n.UserId == request.UserID && n.Status != (short)Status.Deleted).ToListAsync<Notification>();
    }
}

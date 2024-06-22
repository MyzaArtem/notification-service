using Application.Queries.NotificationsQuery;
using MediatR;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Handlers.NotificationHandler
{
    public class GetNotificationByIdHandler : IRequestHandler<GetNotificationByIdQuery, Notification>
    {
        private readonly AppDbContext _appDbContext;
        public GetNotificationByIdHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Notification> Handle(GetNotificationByIdQuery request, CancellationToken cancellationToken)
             => await _appDbContext.Notifications.FirstOrDefaultAsync(x => x.Id == request.Id && x.Status != -1, cancellationToken);
    }
}

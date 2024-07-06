using Application.DTOs;
using Application.Queries.NotificationsQuery;
using Domain.Enums;
using Domain.Models;
using Infrastructure.Data;
using MediatR;
using Infrastructure.Extensions;

namespace Infrastructure.Handlers.NotificationHandler
{
    public class GetNotificationsForUserHandler : IRequestHandler<GetNotificationsForUserQuery, PagedResult<Notification>>
    {
        private readonly AppDbContext _appDbContext;
        public GetNotificationsForUserHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<PagedResult<Notification>> Handle(GetNotificationsForUserQuery request, CancellationToken cancellationToken)
        {
            var query = _appDbContext.Notifications
                .Where(n => n.UserId == request.UserID && n.Status != (short)Status.Deleted)
                .AsQueryable();

            return await query.ToPagedResultAsync(request.Page, request.PageSize);
        }
    }
}


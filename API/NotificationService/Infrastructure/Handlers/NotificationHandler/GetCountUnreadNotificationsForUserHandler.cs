using Application.Queries.NotificationsQuery;
using Domain.Enums;
using Domain.Models;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.NotificationHandler
{
    public class GetCountUnreadNotificationsForUserHandler : IRequestHandler<GetCountUnreadNotificationsForUserQuery, int>
    {
        private readonly AppDbContext _appDbContext;
        public GetCountUnreadNotificationsForUserHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<int> Handle(GetCountUnreadNotificationsForUserQuery request, CancellationToken cancellationToken)
            => await _appDbContext.Notifications.Where(n => n.UserId == request.UserID && n.Status != (short)Status.Deleted).CountAsync();
    }
}
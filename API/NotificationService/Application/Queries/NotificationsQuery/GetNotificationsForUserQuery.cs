using MediatR;
using Domain.Models;
using Application.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.NotificationsQuery
{
    public class GetNotificationsForUserQuery(Guid userID, int page, int pageSize) : IRequest<PagedResult<Notification>>
    {
        public Guid UserID { get; } = userID;
        public int Page { get; } = page;
        public int PageSize { get; } = pageSize;
    }
}

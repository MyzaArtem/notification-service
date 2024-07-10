using API.Controllers;
using Application.DTOs;
using Application.Queries.NotificationsQuery;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace API.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<NotificationHub> _logger;

        public NotificationHub(IMediator mediator, IMapper mapper, ILogger<NotificationHub> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task SendNewNotifications(string userId)
        {
            //await Clients.User(userId).SendAsync("ReceiveNewNotifications", notifications);
            var result = new List<Notification>();
            await Clients.Clients(this.Context.ConnectionId).SendAsync("ReceiveNewNotifications", userId, result);
        }

        public async Task SendUnreadNotificationCount(string userId, int count = 0)
        {
            int result;
            try
            {
                _logger.LogInformation($"Fetching notifications for user with ID: {userId}", userId);

                Guid GuidUserId;
                Guid.TryParse(userId, out GuidUserId);

                var notifications = await _mediator.Send(new GetCountUnreadNotificationsForUserQuery(GuidUserId));
                if (notifications == null)
                {
                    notifications = 0;
                }

                result = notifications;
            }
            catch (ArgumentException ex)
            {
                _logger.LogError($"User ID is invalid: {ex.Message}", ex.Message);
                return;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server error: {ex.Message}", ex.Message);
                return;
            }
            await Clients.Clients(this.Context.ConnectionId).SendAsync("ReceiveUnreadNotificationCount", userId, result);
        }

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine(Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            Console.WriteLine(Context.ConnectionId);
            await base.OnDisconnectedAsync(ex);
        }
    }

}
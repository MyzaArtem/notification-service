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
        private readonly ILogger<NotificationController> _logger;

        public NotificationHub(IMediator mediator, IMapper mapper, ILogger<NotificationController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task SendNewNotifications(string userId)
        {
            //await Clients.User(userId).SendAsync("ReceiveNewNotifications", notifications);
            var result = new List<Notification>();
            await Clients.All.SendAsync("ReceiveNewNotifications", result);
        }

        public async Task SendUnreadNotificationCount(string userId, int count)
        {
            IEnumerable<NotificationReadDto> result;
            try
            {
                _logger.LogInformation($"Fetching notifications for user with ID: {userId}", userId);

                Guid GuidUserId;
                Guid.TryParse(userId, out GuidUserId);

                var notifications = await _mediator.Send(new GetCountUnreadNotificationsForUserQuery(GuidUserId));
                if (notifications == null)
                {
                    _logger.LogWarning($"Notifications not found for user with ID: {userId}", userId);
                    return;
                }

                result = _mapper.Map<IEnumerable<NotificationReadDto>>(notifications);
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
            //await Clients.User(userId).SendAsync("ReceiveUnreadNotificationCount", userId, count);
            await Clients.All.SendAsync("ReceiveUnreadNotificationCount", userId, count);
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

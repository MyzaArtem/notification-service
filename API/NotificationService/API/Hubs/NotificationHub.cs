﻿using API.Controllers;
using Application.DTOs;
using Application.Queries.NotificationsQuery;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

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

        /*public async Task SendNewNotifications(NotificationCreateDto notificationCreateDto)
        {
            string json = JsonConvert.SerializeObject(notificationCreateDto);
            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveNewNotifications", notificationCreateDto.UserId.ToString(), json);
        }*/

        /*public async Task SendNewNotifications(string userId)
        {
            var notifications = new List<string> { "Notification 1", "Notification 2", "Notification 3" };
            string json = JsonConvert.SerializeObject(notifications);
            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveNewNotifications", userId, json);
        }*/

        public async Task SendUnreadNotificationCount(string userId)
        {
            int result;
            try
            {
                _logger.LogInformation($"Fetching notifications for user with ID: {userId}");

                Guid GuidUserId;
                Guid.TryParse(userId, out GuidUserId);

                var notifications = await _mediator.Send(new GetCountUnreadNotificationsForUserQuery(GuidUserId));

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
            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveUnreadNotificationCount", userId, result);
        }

    }

}
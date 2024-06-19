using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Repositories;
using NotificationService.Dtos;
using NotificationService.Models;
using NotificationService.Services;


namespace NotificationService.Controllers
{
    [Route("notifications/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _service;
        private readonly IMapper _mapper;

        public NotificationController(
            INotificationService service,
            IMapper mapper
            )
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("foruser/{userID}", Name = "GetAllNotificationsForUser")]
        public async Task<ActionResult<IEnumerable<NotificationReadDto>>> GetNotifications(int userID)
        {
            var notifications = await _service.GetAllNotificationsForUserAsync(userID);
            if (notifications == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<NotificationReadDto>>(notifications));
        }

        [HttpGet("get/{id}", Name = "GetNotificationById")]
        public async Task<ActionResult<NotificationReadDto>> GetNotificationById(int id)
        {
            var notification = await _service.GetNotificationByIdAsync(id);
            if (notification == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<NotificationReadDto>(notification));
        }

        [HttpPost]
        public async Task<ActionResult<NotificationReadDto>> CreateNotifications(NotificationCreateDto notificationCreateDto)
        {
            var notificationModel = _mapper.Map<Notification>(notificationCreateDto);
            await _service.CreateNotificationAsync(notificationModel);
            await _service.SaveChangesAsync();

            var notificationReadDto = _mapper.Map<NotificationReadDto>(notificationModel);

            return CreatedAtRoute(nameof(GetNotificationById), new { id = notificationReadDto.Id }, notificationReadDto);
        }
    }
}

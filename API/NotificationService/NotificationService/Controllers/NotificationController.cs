using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Data;
using NotificationService.Dtos;
using NotificationService.Models;

namespace NotificationService.Controllers
{
    [Route("notifications/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationRepo _repository;
        private readonly IMapper _mapper;

        public NotificationController(
            INotificationRepo repository,
            IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet("foruser/{userID}", Name = "GetAllNotificationsForUser")]
        public ActionResult<IEnumerable<NotificationReadDto>> GetNotifications(int userID)
        {
            var notificationItem = _repository.GetAllNotificationsForUser(userID);

            return Ok(_mapper.Map<IEnumerable<NotificationReadDto>>(notificationItem));
        }

        [HttpGet("get/{id}", Name = "GetNotificationById")]
        public ActionResult<NotificationReadDto> GetPlatformById(int id)
        {
            var notificationItem = _repository.GetNotificationById(id);
            if (notificationItem != null)
            {
                return Ok(_mapper.Map<NotificationReadDto>(notificationItem));
            }

            return NotFound();
        }

        // TODO : дописать метод
        [HttpPost]
        public async Task<ActionResult<NotificationReadDto>> CreateNotifications(NotificationCreateDto notificationCreateDto)
        {
            var notificationModel = _mapper.Map<Notification>(notificationCreateDto);
            _repository.CreateNotification(notificationModel);
            _repository.SaveChanges();

            var notificationReadDto = _mapper.Map<NotificationReadDto>(notificationModel);

            return CreatedAtRoute(nameof(GetPlatformById), new { Id = notificationReadDto.Id }, notificationReadDto);
        }
    }
}

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
        public ActionResult<IEnumerable<NotificationReadDto>> GetNotifications(int userID)
        {

        }

        [HttpGet("get/{id}", Name = "GetNotificationById")]
        public ActionResult<NotificationReadDto> GetPlatformById(int id)
        {
        }

        // TODO : дописать метод
        [HttpPost]
        public async Task<ActionResult<NotificationReadDto>> CreateNotifications(NotificationCreateDto notificationCreateDto)
        {

        }
    }
}

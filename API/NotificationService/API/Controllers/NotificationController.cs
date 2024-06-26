using Application.DTOs;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Application.Queries.NotificationsQuery;
using Application.Commands.NotificationsCommands;

namespace API.Controllers
{
    [Route("notifications/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<NotificationController> _logger;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="NotificationController"/>.
        /// </summary>
        /// <param name="mediator">Экземпляр медиатора.</param>
        /// <param name="mapper">Экземпляр маппера.</param>
        /// <param name="logger">Экземпляр логгера.</param>
        public NotificationController(IMediator mediator, IMapper mapper, ILogger<NotificationController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Получает все уведомления для конкретного пользователя.
        /// </summary>
        /// <param name="userId">ID пользователя.</param>
        /// <returns>Список уведомлений для пользователя.</returns>
        [HttpGet("foruser/{userId}", Name = "GetAllNotificationsForUser")]
        public async Task<ActionResult<IEnumerable<NotificationReadDto>>> GetAllNotificationsForUser(Guid userId)
        {
            try
            {
                _logger.LogInformation($"Получение уведомлений для пользователя с ID: {userId}");
                var notifications = await _mediator.Send(new GetAllNotificationsForUserQuery(userId));
                if (notifications == null)
                {
                    _logger.LogWarning($"Уведомления не найдены для пользователя с ID: {userId}");
                    return NotFound();
                }

                return Ok(_mapper.Map<IEnumerable<NotificationReadDto>>(notifications));
            }
            catch (ArgumentException ex)
            {
                _logger.LogError($"Неверный ID пользователя: {ex.Message}");
                return BadRequest("Неверный ID пользователя");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Внутренняя ошибка сервера: {ex.Message}");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        /// <summary>
        /// Получает уведомление по его ID.
        /// </summary>
        /// <param name="id">ID уведомления.</param>
        /// <returns>Уведомление с указанным ID.</returns>
        [HttpGet("{id}", Name = "GetNotificationById")]
        public async Task<ActionResult<NotificationReadDto>> GetNotificationById(Guid id)
        {
            try
            {
                _logger.LogInformation($"Получение уведомления с ID: {id}");

                var notification = await _mediator.Send(new GetNotificationByIdQuery(id));
                if (notification == null)
                {
                    _logger.LogWarning($"Уведомление с ID {id} не найдено");
                    return NotFound();
                }

                return Ok(_mapper.Map<NotificationReadDto>(notification));
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError($"Произошла ошибка конкурентного доступа при получении уведомления с ID {id}: {ex.Message}");
                return StatusCode(500, "Произошла ошибка конкурентного доступа");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Внутренняя ошибка сервера: {ex.Message}");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        /// <summary>
        /// Создает новое уведомление.
        /// </summary>
        /// <param name="notificationCreateDto">DTO с деталями уведомления.</param>
        /// <returns>ID созданного уведомления.</returns>
        [HttpPost]
        public async Task<ActionResult<NotificationReadDto>> CreateNotifications(NotificationCreateDto notificationCreateDto)
        {
            try
            {
                _logger.LogInformation("Создание нового уведомления");

                var notificationModel = _mapper.Map<Notification>(notificationCreateDto);
                var id = await _mediator.Send(new CreateNotificationCommand(notificationModel));
                var notificationReadDto = _mapper.Map<NotificationReadDto>(notificationModel);

                return Ok(id);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError($"Уведомление равно null: {ex.Message}");
                return BadRequest("Уведомление равно null");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Внутренняя ошибка сервера: {ex.Message}");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        /// <summary>
        /// Обновляет существующее уведомление.
        /// </summary>
        /// <param name="notificationUpdateDto">DTO с обновленными деталями уведомления.</param>
        /// <returns>Ответ с флагом и сообщением.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateNotification(NotificationUpdateDto notificationUpdateDto)
        {
            try
            {
                _logger.LogInformation($"Обновление уведомления с ID: {notificationUpdateDto.Id}");

                return Ok(await _mediator.Send(new UpdateNotificationCommand(notificationUpdateDto)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Внутренняя ошибка сервера: {ex.Message}");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        /// <summary>
        /// Удаляет уведомление по его ID.
        /// </summary>
        /// <param name="id">ID удаляемого уведомления.</param>
        /// <returns>Ответ с флагом и сообщением.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(Guid id)
        {
            try
            {
                _logger.LogInformation($"Удаление уведомления с ID: {id}");

                return Ok(await _mediator.Send(new DeleteNotificationCommand(id)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Внутренняя ошибка сервера: {ex.Message}");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }
    }
}

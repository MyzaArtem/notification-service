using Application.DTOs;
using AutoMapper;
using Domain.Models;
using Infrastructure.Implemenation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("notifications/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationRepository _service;
        private readonly IMapper _mapper;
        private readonly ILogger<NotificationController> _logger;

        public NotificationController(NotificationRepository service, IMapper mapper, ILogger<NotificationController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("foruser/{userId}", Name = "GetAllNotificationsForUser")]
        public async Task<ActionResult<IEnumerable<NotificationReadDto>>> GetAllNotificationsForUser(Guid userId)
        {
            try
            {
                _logger.LogInformation($"Fetching notifications for user with ID: {userId}");

                var notifications = await _service.GetAllNotificationsForUserAsync(userId);
                if (notifications == null)
                {
                    _logger.LogWarning($"Notifications not found for user with ID: {userId}");
                    return NotFound();
                }

                return Ok(_mapper.Map<IEnumerable<NotificationReadDto>>(notifications));
            }
            catch (ArgumentException ex)
            {
                _logger.LogError($"User ID is invalid: {ex.Message}");
                return BadRequest("User ID is invalid");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "GetNotificationById")]
        public async Task<ActionResult<NotificationReadDto>> GetNotificationById(Guid id)
        {
            try
            {
                _logger.LogInformation($"Fetching notification with ID: {id}");

                var notification = await _service.GetAsync(id);
                if (notification == null)
                {
                    _logger.LogWarning($"Notification with ID {id} not found");
                    return NotFound();
                }

                return Ok(_mapper.Map<NotificationReadDto>(notification));
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError($"Concurrency error occurred while fetching notification with ID {id}: {ex.Message}");
                return StatusCode(500, "Concurrency error occurred");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<NotificationReadDto>> CreateNotifications(NotificationCreateDto notificationCreateDto)
        {
            try
            {
                _logger.LogInformation($"Creating new notification");

                var notificationModel = _mapper.Map<Notification>(notificationCreateDto);
                await _service.CreateAsync(notificationModel);

                var notificationReadDto = _mapper.Map<NotificationReadDto>(notificationModel);

                return CreatedAtRoute(nameof(GetNotificationById), new { id = notificationReadDto.Id }, notificationReadDto);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError($"Notification is null: {ex.Message}");
                return BadRequest("Notification is null");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotification(int id, NotificationUpdateDto notificationUpdateDto)
        {
            try
            {
                _logger.LogInformation($"Updating notification with ID: {id}");

                var notificationModel = _mapper.Map<Notification>(notificationUpdateDto);
                await _service.UpdateAsync(notificationModel);

                return Ok();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError($"Concurrency error occurred while updating notification with ID {id}: {ex.Message}");
                return StatusCode(500, "Concurrency error occurred");
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError($"Notification is null: {ex.Message}");
                return BadRequest("Notification is null");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(Guid id)
        {
            try
            {
                _logger.LogInformation($"Deleting notification with ID: {id}");

                await _service.DeleteAsync(id);
                return Ok();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError($"Concurrency error occurred while deleting notification with ID {id}: {ex.Message}");
                return StatusCode(500, "Concurrency error occurred");
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError($"Notification is null: {ex.Message}");
                return BadRequest("Notification is null");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

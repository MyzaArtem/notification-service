using Application.Commands.NotificationsCommands;
using Serilog;
using Infrastructure.Data;
using MediatR;

namespace Infrastructure.Handlers.NotificationHandler
{
    public class CreateNotificationHandler : IRequestHandler<CreateNotificationCommand, Guid>
    {
        private readonly AppDbContext _appDbContext;
        public CreateNotificationHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Guid> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var id = Guid.NewGuid();
            if (request.notification == null)
            {
                throw new ArgumentNullException(nameof(request.notification));
            }
            request.notification.Id = id;

            try
            {
                await _appDbContext.Notifications.AddAsync(request.notification, cancellationToken);
                await _appDbContext.SaveChangesAsync(cancellationToken);
                Log.Information($"Notification created successfully with ID: {id}");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Failed to create notification with ID: {id}");
                throw ex;
            }

            return id;
        }
    }
}

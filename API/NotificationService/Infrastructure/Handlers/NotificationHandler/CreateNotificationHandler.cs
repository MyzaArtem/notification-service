using Application.Commands.NotificationsCommands;
using Domain.Models;
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
            await _appDbContext.Notifications.AddAsync(request.notification);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return id;
        }
    }
}

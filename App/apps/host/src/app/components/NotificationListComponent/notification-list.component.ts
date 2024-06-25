import { Component, OnInit } from '@angular/core';
import { SignalRService } from '../../../services/signalr.service';
import { MyNotification } from 'apps/host/src/models/notifications/notifications';

@Component({
  selector: 'app-notification-list',
  templateUrl: './notification-list.component.html',
  styleUrls: ['./notification-list.component.css']
})
export class NotificationListComponent implements OnInit {
  notifications: MyNotification[] = [];
  unreadCount = 0;

  constructor(private notificationService: SignalRService) {}

  ngOnInit() {
    this.notificationService.newNotifications$.subscribe(notifications => {
      this.notifications = notifications;
    });

    this.notificationService.unreadCount$.subscribe(count => {
      this.unreadCount = count;
    });
  }
}

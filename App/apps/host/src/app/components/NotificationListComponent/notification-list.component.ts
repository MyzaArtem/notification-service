import { Component, OnInit } from '@angular/core';
import { SignalRService } from '../../../services/signalr.service';
import { MyNotification } from '../../../models/notifications/notifications';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-notification-list',
  templateUrl: './notification-list.component.html',
  styleUrls: ['./notification-list.component.css']
})
export class NotificationListComponent implements OnInit {
  newNotifications$!: Observable<MyNotification[]>;
  unreadCount$!: Observable<number>;

  constructor(private notificationService: SignalRService) {}

  ngOnInit() {
    this.newNotifications$ = this.notificationService.newNotifications$;
    this.unreadCount$ = this.notificationService.unreadCount$;
  }
}

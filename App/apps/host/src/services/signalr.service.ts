import { MyNotification } from '../models/notifications/notifications';
import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import {Observable, Subject} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection?: signalR.HubConnection;
  private pendingNotificationUpdatedSubject = new Subject<MyNotification[]>();
  notificationsUpdated$: Observable<MyNotification[]> = this.pendingNotificationUpdatedSubject.asObservable();

  private pendingCountNotificationsUpdatedSubject = new Subject<number>();
  countNotificationsUpdated$: Observable<number> = this.pendingCountNotificationsUpdatedSubject.asObservable();

  constructor() {

  }

  connect(){
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:8080/hubs/notifications', {
        withCredentials: sessionStorage.getItem('token') != null,
        accessTokenFactory: () => {
          const token = sessionStorage.getItem('token');
            return token ?? '';
        },
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,})
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connected to SignalR hub'))
      .catch(err => console.error('Error connecting to SignalR hub:', err));

    this.hubConnection.on('ReceiveNewNotifications', (orders: MyNotification[]) => {
      this.pendingNotificationUpdatedSubject.next(orders);
    });

    this.hubConnection.on('ReceiveUnreadNotificationCount', (count: number) => {
        this.pendingCountNotificationsUpdatedSubject.next(count);
      });
  }

  async updateUnreadNotificationCount(userId: string, count: number) {
    await this.hubConnection?.invoke('SendUnreadNotificationCount', userId, count);
  }

}
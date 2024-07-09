import { MyNotification } from '../models/notifications/notifications';
import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import {Observable, Subject, BehaviorSubject} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection: signalR.HubConnection;
  private newNotificationsSource = new BehaviorSubject<MyNotification[]>([]);
  private unreadCountSource = new BehaviorSubject<number>(0);

  newNotifications$ = this.newNotificationsSource.asObservable();
  unreadCount$ = this.unreadCountSource.asObservable();

  //header
  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:8080/hubs/notifications'
        // , {
        //   skipNegotiation: true,
        //   transport: signalR.HttpTransportType.WebSockets
        // }
        // , {
        // withCredentials: sessionStorage.getItem('token') != null,
        // accessTokenFactory: () => {
        //   const token = sessionStorage.getItem('token');
        //     return token ?? '';
        // },
        // skipNegotiation: true,
        // transport: signalR.HttpTransportType.WebSockets,}
      )
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.hubConnection.start()
      .catch(err => console.error('Ошибка при подключении к SignalR хабу: ' + err));
    // this.hubConnection
    //   .start()
    //   .then(() => console.log('Connected to SignalR hub'))
    //   .catch(err => console.error('Error connecting to SignalR hub:', err));

    // this.hubConnection.on('ReceiveNewNotifications', (orders: MyNotification[]) => {
    //   this.newNotificationsSource.next(orders);
    // });

    // this.hubConnection.on('ReceiveUnreadNotificationCount', (count: number) => {
    //     this.unreadCountSource.next(count);
    //   });

  }

  // public startConnection() {
  //   this.hubConnection
  //     .start()
  //     .then(() => console.log('Connection started'))
  //     .catch(err => console.log('Error while starting connection: ' + err));
  // }

  // async updateUnreadNotificationCount(userId: string, count: number) {
  //   await this.hubConnection?.invoke('SendUnreadNotificationCount', userId, count);
  // }
}
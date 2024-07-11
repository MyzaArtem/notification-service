import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import {Observable, Subject, BehaviorSubject} from "rxjs";
import { MyNotification } from '../models/home/myNotification.model';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  hubConnection: signalR.HubConnection;
  newNotificationsSource = new BehaviorSubject<string>("");
  private unreadCountSource = new BehaviorSubject<number>(0);

  newNotifications$ = this.newNotificationsSource.asObservable();
  unreadCount$ = this.unreadCountSource.asObservable();

  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:8080/notification-hub'
        , {
          skipNegotiation: true,
          transport: signalR.HttpTransportType.WebSockets
        }
      )
      .configureLogging(signalR.LogLevel.Information)
      .build();
  }

  startConnection = () => 
  {
    this.hubConnection = new signalR.HubConnectionBuilder()
        .withUrl('http://localhost:8080/notification-hub', {
          skipNegotiation: true,
          transport: signalR.HttpTransportType.WebSockets
        })
        .build()

    this.hubConnection
    .start()
    .then(() => 
    {
        console.log('Hub Connection Started!')
    })
    .catch(err => console.log('Error while start connection:' + err))
  }

  askServer(guid: string) {
    this.hubConnection.invoke('SendUnreadNotificationCount', guid);
    //this.hubConnection.invoke('SendNewNotifications', guid);
  }

  askServerListener() {
    this.hubConnection.on('ReceiveUnreadNotificationCount', (userGuid, count) => {
      // console.log('UNREAD:' + userGuid)
      console.log('UNREAD:' + count)
    })

    this.hubConnection.on('ReceiveNewNotifications', (notifications) => {
      // console.log('NEWNOT:' + userGuid)
      console.log('NEWNOT:' + notifications)
      this.newNotificationsSource.next(notifications);
    })

    this.hubConnection.on('ReceiveServices', (count) => {
      // console.log('NEWNOT:' + userGuid)
      console.log('SERVICES:' + count)
    })

    this.hubConnection.on('ReceiveUsers', (count) => {
      // console.log('NEWNOT:' + userGuid)
      console.log('USERS:' + count)
    })
  }

  
}
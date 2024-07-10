import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import {Observable, Subject, BehaviorSubject} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  hubConnection: signalR.HubConnection;
//   private newNotificationsSource = new BehaviorSubject<MyNotification[]>([]);
  private unreadCountSource = new BehaviorSubject<number>(0);

//   newNotifications$ = this.newNotificationsSource.asObservable();
  unreadCount$ = this.unreadCountSource.asObservable();

  //header
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
        .withUrl('https://localhost:8080/notification-hub', {
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
  }

  askServerListener() {
    this.hubConnection.on('ReceiveUnreadNotificationCount', (someText) => {
        console.log(someText)
    })
  }

  
}
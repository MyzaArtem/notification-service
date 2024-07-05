import { Component } from '@angular/core';
//import { DialogService } from '@progress/kendo-angular-dialog';

@Component({
  selector: 'app-notification-dialog',
  templateUrl: './notification-dialog.component.html',
  styleUrls: ['./notification-dialog.component.scss']
})
export class NotificationDialogComponent  {
  public people = [
    { id: 1, name: 'John Doe' },
    { id: 2, name: 'Jane Smith' },
  ];

  public messages = [
    { id: 1, from: 'John Doe', text: 'Hello!', timestamp: new Date() },
    { id: 2, from: 'Jane Smith', text: 'Hi there!', timestamp: new Date() },
  ];


}

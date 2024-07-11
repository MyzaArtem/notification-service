import { Pipe, PipeTransform } from '@angular/core';
import { formatDate } from '@angular/common';

@Pipe({
  name: 'customDate'
})
export class CustomDatePipe implements PipeTransform {

  transform(value: any): string {
    if (!value) return '';

    const date = new Date(value);
    const today = new Date();
    const yesterday = new Date(today);
    yesterday.setDate(today.getDate() - 1);

    const isToday = date.toDateString() === today.toDateString();
    const isYesterday = date.toDateString() === yesterday.toDateString();

    const time = formatDate(date, 'HH:mm', 'en-US');

    if (isToday) {
      return `Сегодня в ${time}`;
    } else if (isYesterday) {
      return `Вчера в ${time}`;
    } else {
      return formatDate(date, 'dd.MM.yyyy', 'en-US');
    }
  }
}

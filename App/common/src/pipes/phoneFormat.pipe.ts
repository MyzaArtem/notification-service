import {Pipe, PipeTransform} from '@angular/core';
@Pipe({
  name: 'phoneFormat',
})
export class PhoneFormatPipe implements PipeTransform {
  transform(input: string) {
    return input.replace(/^\+?(\d)(\d{3})(\d{3})(\d{2})(\d{2})$/, '+$1 ($2) $3-$4-$5');
  }
}

import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'values'
})
export class ValuesPipe implements PipeTransform {

  transform(value: any): any {
    if (value !== null && typeof value === 'object') {
      if (value !== null && typeof value === 'object') {
        return Object.values(value);
      }
      return [];
    }

}}

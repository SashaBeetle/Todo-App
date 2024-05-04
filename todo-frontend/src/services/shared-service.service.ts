import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedServiceService {
  
  private isVisible  = new BehaviorSubject<boolean>(false);

  isVisible$ = this.isVisible.asObservable();

  toggleIsVisible() {
    this.isVisible.next(!this.isVisible.value);
  }
  constructor() { }
}

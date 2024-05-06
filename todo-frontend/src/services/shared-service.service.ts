import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedServiceService {
  
  private isVisibleHistory  = new BehaviorSubject<boolean>(false);
  private isVisibleCard = new BehaviorSubject<boolean>(false);
  private isVisibleCreateList = new BehaviorSubject<boolean>(false);
  private isVisibleEditCard = new BehaviorSubject<boolean>(false);

  isVisibleHistory$ = this.isVisibleHistory.asObservable();
  isVisibleCard$ = this.isVisibleCard.asObservable();
  isVisibleCreateList$ = this.isVisibleCreateList.asObservable();
  isVisibleEditCard$ = this.isVisibleEditCard.asObservable();

  toggleIsVisibleHistory() {
    this.isVisibleHistory.next(!this.isVisibleHistory.value);
  }

  toggleIsVisibleCard(){
    this.isVisibleCard.next(!this.isVisibleCard.value);
  }

  toggleIsVisibleCreateList(){
    this.isVisibleCreateList.next(!this.isVisibleCreateList.value);
  }

  toggleIsVisibleEditCard(){
    this.isVisibleEditCard.next(!this.isVisibleEditCard.value);
  }
  
  constructor() { }
}

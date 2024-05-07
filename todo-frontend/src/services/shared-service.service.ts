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
  
  private listId = new BehaviorSubject<number | null>(null);
  listId$ = this.listId.asObservable();
  

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


  setListId(id: number) {
    this.listId.next(id);
  }

  getListId(): number | null {
    return this.listId.getValue();
  }
  constructor() { }
}

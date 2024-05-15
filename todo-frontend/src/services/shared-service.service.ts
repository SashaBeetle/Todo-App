import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
  
  private isVisibleHistory  = new BehaviorSubject<boolean>(false);
  private isVisibleCard = new BehaviorSubject<boolean>(false);
  private isVisibleCreateList = new BehaviorSubject<boolean>(false);
  private isVisibleEditCard = new BehaviorSubject<boolean>(false);
  private isVisibleCreateBoard = new BehaviorSubject<boolean>(false);
  private isVisibleBoard = new BehaviorSubject<boolean>(false);
  private isChooseCardHistory = new BehaviorSubject<boolean>(false)
  private isEditableList = new BehaviorSubject<boolean>(false);
  private isEditableCard = new BehaviorSubject<boolean>(false);
  private isEditableBoard = new BehaviorSubject<boolean>(false);
  
  
  private list = new BehaviorSubject<number | null>(null);
  list$ = this.list.asObservable();

  private card = new BehaviorSubject<any | null>(null);
  card$ = this.card.asObservable();

  private data = new BehaviorSubject<any | null>(null);
  data$ = this.data.asObservable();

  private history = new BehaviorSubject<any | null>(null);
  history$ = this.history.asObservable();

  private lists = new BehaviorSubject<any | null>(null);
  lists$ = this.lists.asObservable();
  
  isVisibleBoard$ = this.isVisibleBoard.asObservable()
  isVisibleCreateBoard$ = this.isVisibleCreateBoard.asObservable();
  isVisibleHistory$ = this.isVisibleHistory.asObservable();
  isVisibleCard$ = this.isVisibleCard.asObservable();
  isVisibleCreateList$ = this.isVisibleCreateList.asObservable();
  isVisibleEditCard$ = this.isVisibleEditCard.asObservable();
  isChooseCardHistory$ = this.isChooseCardHistory.asObservable();
  isEditableList$ = this.isEditableList.asObservable();
  isEditableCard$ = this.isEditableCard.asObservable();
  isEditableBoard$ = this.isEditableBoard.asObservable();

toggleisEditableBoard(){
  this.isEditableBoard.next(!this.isEditableBoard.value)
}

toggleisVisibleBoard(){
  this.isVisibleBoard.next(!this.isVisibleBoard.value)
}

toggleisVisibleCreateBoard(){
  this.isVisibleCreateBoard.next(!this.isVisibleCreateBoard.value)
}

toggleisEditableCard(){
  this.isEditableCard.next(!this.isEditableCard.value)
}

  toggleisEditableList(){
    this.isEditableList.next(!this.isEditableList.value)
  }

  toggleisChooseCardHistory(){
    this.isChooseCardHistory.next(!this.isChooseCardHistory.value)
  }

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


  setList(data: any) {
    this.list.next(data);
  }

  getList(): any | null {
    return this.list.getValue();
  }

  setCard(data:any){
    this.card.next(data)
  }

  getCard(): any | null{
    return this.card.getValue();
  }

  setData(data: any){
    this.data.next(data)
  }

  getData(){
    return this.data.getValue();
  }

  setHistory(data: any){
    this.history.next(data)
  }

  getHistory(){
    return this.history.getValue(); 
  }

  setLists(data:any){
    this.lists.next(data);
  }

  getLists(){
    return this.lists.getValue();
  }

  constructor() { }
}

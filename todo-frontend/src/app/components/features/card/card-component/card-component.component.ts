import { Component, inject, Input, Output } from '@angular/core';
import { SharedService } from '../../../../services/shared-service.service';
import { OpenCardComponent } from '../open-card/open-card.component';
import { ApiService } from '../../../../services/api.service';
import { CommonModule } from '@angular/common';
import { Store } from '@ngrx/store';
import { BoardState } from '../../../../ngrx/board/board.reducer';
import { selectBoard } from '../../../../ngrx/board/board.selectors';

@Component({
  selector: 'app-card-component',
  standalone: true,
  imports: [OpenCardComponent, CommonModule],
  templateUrl: './card-component.component.html',
  styleUrl: './card-component.component.scss'
})
export class CardComponentComponent {
  private readonly store:Store<BoardState> = inject(Store);

  @Input() cardId: any;
  @Input() list: any;
  @Input() lists: any;
  @Input() history:any;
  currentBoard: any;

  sharedData: any;
  card: any;
    


  constructor(
    private sharedService: SharedService, 
    private apiService: ApiService,
  ){}

  onClickOpenCard() {
    this.sharedService.toggleIsVisibleCard();
    this.sharedService.setCard(this.card);
    this.sharedService.setData(this.lists);
    this.sharedService.setList(this.list);

    this.apiService.getData(`https://localhost:7247/api/HistoryItem/ForCard${this.card.id}`)
    .subscribe(response => {
      this.history = response;
      this.sharedService.setHistory(response);
      console.log('Get request successful!', this.history);
    }, error => {
      console.error('Error Getting data:', error);
    });



    
    
  }

  onClickDelete(){
    this.apiService.deleteDataByIdManual(`https://localhost:7247/api/cards/${this.cardId}?boardId=${this.currentBoard.id}`,).subscribe(res=>{
      this.removeFromList(this.cardId)
    })
  }

  onClickPatch(anotherList: any){
    this.apiService.patchData(`https://localhost:7247/api/catalog/MoveCard?catalogId_1=${this.list.id}&catalogId_2=${anotherList.id}&cardId=${this.cardId}&boardId=${this.currentBoard.id}`, 1)
      .subscribe(response => {
        this.swapCard(this.cardId, anotherList);
        console.log('Patch request successful!', response);
      }, error => {
        console.error('Error patching data:', error);
      });

  }

  swapCard(card: number, anotherList: any) {
    this.removeFromList(card);
    anotherList.cardsId.push(this.cardId);
    }
  



  ngOnInit(){
      this.apiService.getData(`https://localhost:7247/api/cards/${this.cardId}`).subscribe(res =>{
        this.card = res;
      });

      this.store.select(selectBoard).subscribe(board => {
        this.currentBoard = board;
      });
    }

removeFromList(cardId: number) {
  const index = this.list.cardsId.findIndex((item: number) => item === cardId);
  if (index !== -1) {
    this.list.cardsId.splice(index, 1);
  } else {
    console.warn('Card not found in local list:', cardId);
  }
}
}


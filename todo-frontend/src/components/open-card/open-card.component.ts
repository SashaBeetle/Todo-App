import { Component, Input, Output, SimpleChanges } from '@angular/core';
import { SharedServiceService } from '../../services/shared-service.service';
import { ApiService } from '../../services/api.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-open-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './open-card.component.html',
  styleUrl: './open-card.component.scss'
})
export class OpenCardComponent {

  constructor(private sharedService: SharedServiceService, private apiService: ApiService){}
  @Input() isChoose: boolean = false;
  @Input() isVisible: boolean = true;
  @Input() card: any;
  @Input() data: any;
  @Input() list: any;
  @Output() history: any;

  
  onClick() {
    this.sharedService.toggleIsVisibleCard();
  }


  onClickEditCard(){
    this.sharedService.toggleIsVisibleEditCard();
    this.sharedService.toggleIsVisibleCard();
    this.sharedService.toggleisEditableCard();
    this.sharedService.setCard(this.card);
    this.sharedService.setList(this.list);
  }
  
  onClickPatch(anotherList: any){
    this.apiService.patchData(`https://localhost:7247/api/catalog/MoveCard?catalogId_1=${this.list.id}&catalogId_2=${anotherList.id}&cardId=${this.card.id}`, 1)
      .subscribe(response => {
        this.swapCard(this.card.id, anotherList);
        console.log('Patch request successful!', response);
      }, error => {
        console.error('Error patching data:', error);
      });
  }

  swapCard(card: number, anotherList: any) {
    this.removeFromList(card);
    anotherList.cardsId.push(this.card.id);
    }
    removeFromList(cardId: number) {
      const index = this.list.cardsId.findIndex((item: number) => item === cardId);
      if (index !== -1) {
        this.list.cardsId.splice(index, 1);
      } else {
        console.warn('Card not found in local list:', cardId);
      }
    }

  ngOnInit() {
    this.sharedService.isVisibleCard$.subscribe(value => {
      this.isVisible = value; 
    });

    this.sharedService.isChooseCardHistory$.subscribe(value =>{
      this.isChoose = value;
    })

}


ngDoCheck(): void {
  if(this.isVisible){
    this.card = this.sharedService.getCard();
    this.data = this.sharedService.getData();
    this.list = this.sharedService.getList();
    this.history = this.sharedService.getHistory().slice().reverse();
  }
}


  
  
}


import { Component, Input } from '@angular/core';
import { SharedServiceService } from '../../services/shared-service.service';
import { OpenCardComponent } from '../open-card/open-card.component';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-card-component',
  standalone: true,
  imports: [OpenCardComponent],
  templateUrl: './card-component.component.html',
  styleUrl: './card-component.component.scss'
})
export class CardComponentComponent {
  @Input() cardId: any;
  @Input() list: any;
  @Input() data: any;

  constructor(private sharedService: SharedServiceService, private apiService: ApiService){}
  card: any;

  onClick() {
    this.sharedService.toggleIsVisibleCard();
  }

  onClickDelete(){
    this.apiService.deleteDataById(`https://localhost:7247/api/cards`, this.cardId).subscribe(res=>{
      this.removeFromList(this.cardId)
      console.log('List',this.list.cardsId);

    })
  }

  onClickPatch(){
    
  }


  async ngOnInit(){
    this.apiService.getData(`https://localhost:7247/api/cards/${this.cardId}`).subscribe(res =>{
      this.card = res;
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

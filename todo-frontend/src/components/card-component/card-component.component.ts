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

  constructor(private sharedService: SharedServiceService, private apiService: ApiService){}
  card: any;

  onClick() {
    this.sharedService.toggleIsVisibleCard();
  }

  onClickDelete(){
    this.apiService.deleteDataById(`https://localhost:7247/api/cards`, this.cardId).subscribe(res=>{
      console.log('deleted', this.cardId);
    })
  }


  async ngOnInit(){
    this.apiService.getData(`https://localhost:7247/api/cards/${this.cardId}`).subscribe(res =>{
      console.log(`https://localhost:7247/api/cards/${this.cardId}`);
      console.log('card',res); 
      this.card = res;
    });
}
}

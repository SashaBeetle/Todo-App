import { Component, Input } from '@angular/core';
import { SharedServiceService } from '../../services/shared-service.service';

@Component({
  selector: 'app-open-card',
  standalone: true,
  imports: [],
  templateUrl: './open-card.component.html',
  styleUrl: './open-card.component.scss'
})
export class OpenCardComponent {

  constructor(private sharedService: SharedServiceService){}

  @Input() isVisible: boolean = true;

  onClick() {
    this.sharedService.toggleIsVisibleCard();
  }

  onClickEditCard(){
    this.sharedService.toggleIsVisibleEditCard();
    this.sharedService.toggleIsVisibleCard();
  }
  
  ngOnInit() {
    this.sharedService.isVisibleCard$.subscribe(value => {
      this.isVisible = value; 
    });
}
}


import { Component } from '@angular/core';
import { SharedServiceService } from '../../services/shared-service.service';
import { OpenCardComponent } from '../open-card/open-card.component';

@Component({
  selector: 'app-card-component',
  standalone: true,
  imports: [OpenCardComponent],
  templateUrl: './card-component.component.html',
  styleUrl: './card-component.component.scss'
})
export class CardComponentComponent {
  constructor(private sharedService: SharedServiceService){}

  onClick() {
    this.sharedService.toggleIsVisibleCard();
  }
}

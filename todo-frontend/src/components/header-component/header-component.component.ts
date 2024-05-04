import { Component, EventEmitter, Input, Output, SimpleChanges } from '@angular/core';
import { HistoryComponentComponent } from '../history-component/history-component.component';
import { SharedServiceService } from '../../services/shared-service.service';

@Component({
  selector: 'app-header-component',
  standalone: true,
  imports: [HistoryComponentComponent],
  templateUrl: './header-component.component.html',
  styleUrl: './header-component.component.scss'
})
export class HeaderComponentComponent {
  constructor(private sharedService: SharedServiceService){}
  
  onClick() {
    this.sharedService.toggleIsVisible();
  }
  
}

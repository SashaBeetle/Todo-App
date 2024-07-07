import { Component, Input } from '@angular/core';
import { SharedService } from '../../../../services/shared-service.service';
import { CommonModule } from '@angular/common';
import { HistoryComponentComponent } from '../../history/history-component/history-component.component';

@Component({
  selector: 'app-header-component',
  standalone: true,
  imports: [CommonModule, HistoryComponentComponent],
  templateUrl: './header-component.component.html',
  styleUrl: './header-component.component.scss'
})
export class HeaderComponentComponent{

  constructor(private sharedService: SharedService){}
  @Input() currentBoard: any;

  isVisibleHistoryComponent: boolean = false;
  
  onClickOpenHistory() {
    this.isVisibleHistoryComponent = true;
  }

  handleOutputEventHistory(value: boolean) {
    this.isVisibleHistoryComponent = value;
  }
  
  onClickReturn(){
    this.sharedService.toggleisVisibleBoard();
  }
}

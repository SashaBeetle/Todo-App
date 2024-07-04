import { Component, Input } from '@angular/core';
import { SharedService } from '../../../../services/shared-service.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-header-component',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './header-component.component.html',
  styleUrl: './header-component.component.scss'
})
export class HeaderComponentComponent{

  constructor(private sharedService: SharedService){}
  @Input() history: any;
  @Input() currentBoard: any;
  
  onClickOpenHistory() {
    this.sharedService.toggleIsVisibleHistory();
  }

  onClickReturn(){
    this.sharedService.toggleisVisibleBoard();
  }// Тут є кнопка яка спрацьовує на відкривання історії, сама є історія знаходиться на дошці потрібно зробити функціонал.
  
}

import { Component, Input } from '@angular/core';
import { HistoryComponentComponent } from '../../history/history-component/history-component.component';
import { SharedService } from '../../../../services/shared-service.service';
import { ApiService } from '../../../../services/api.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-header-component',
  standalone: true,
  imports: [HistoryComponentComponent, CommonModule],
  templateUrl: './header-component.component.html',
  styleUrl: './header-component.component.scss'
})
export class HeaderComponentComponent{

  constructor(private sharedService: SharedService, private apiService: ApiService){}
  @Input() history: any;
  @Input() currentBoard: any;
  
  onClickOpenHistory() {
    this.sharedService.toggleIsVisibleHistory();
    this.sharedService.setHistory(this.history);
    
    this.apiService.getData(`https://localhost:7247/api/v1/historyitem/ForBoard${this.currentBoard.id}`)
    .subscribe(response => {
      this.history = response;
      this.sharedService.setHistory(response);
      console.log('Get request successful!', this.history);
    }, error => {
      console.error('Error Getting data:', error);
    });
  }

  onClickReturn(){
    this.sharedService.toggleisVisibleBoard();
  }
  
}

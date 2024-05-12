import { Component, Input } from '@angular/core';
import { HistoryComponentComponent } from '../history-component/history-component.component';
import { SharedService } from '../../services/shared-service.service';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-header-component',
  standalone: true,
  imports: [HistoryComponentComponent],
  templateUrl: './header-component.component.html',
  styleUrl: './header-component.component.scss'
})
export class HeaderComponentComponent {
  constructor(private sharedService: SharedService, private apiService: ApiService){}
  @Input() history: any;
  
  onClick() {
    this.sharedService.toggleIsVisibleHistory();
    this.sharedService.setHistory(this.history);
    
    this.apiService.getData(`https://localhost:7247/api/HistoryItem`)
    .subscribe(response => {
      this.history = response;
      this.sharedService.setHistory(response);
      console.log('Get request successful!', this.history);
    }, error => {
      console.error('Error Getting data:', error);
    });
  }
  
}

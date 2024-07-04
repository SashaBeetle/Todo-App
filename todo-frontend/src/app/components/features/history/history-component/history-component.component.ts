import { Component,EventEmitter,Input, Output} from '@angular/core';
import { SharedService } from '../../../../services/shared-service.service';
import { ApiService } from "../../../../services/api.service";
import { CommonModule } from '@angular/common';
import {ScrollPanelModule} from 'primeng/scrollpanel';



@Component({
  selector: 'app-history-component',
  standalone: true,
  imports: [
    CommonModule,
    ScrollPanelModule
    ],
  templateUrl: './history-component.component.html',
  styleUrl: './history-component.component.scss'
})
export class HistoryComponentComponent {

  constructor(private sharedService: SharedService, private apiService: ApiService){ }

  @Input() isVisible: boolean = false;
  @Output() history: any;
  showMore: boolean = false;

  @Output() outputEvent = new EventEmitter<boolean>();


  
  onClick() {
    this.sharedService.toggleIsVisibleHistory();
    this.showMore = false;
    this.outputEvent.emit(false);
  }

  onClickShowMore(){
    this.showMore = true;
  }
  
  ngOnInit() {
    this.sharedService.isVisibleHistory$.subscribe(value => {
      this.isVisible = value; 
    });

    this.apiService.getDataById(`https://localhost:7247/api/v1/historyitems/board`, 77)
    .subscribe(response => {
      this.history = response;
      this.history = this.history.slice().reverse();
      console.log('Get request successful!', this.history);
    }, error => {
      console.error('Error Getting data:', error);
    });
  } 
}


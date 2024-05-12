import { Component,Input, Output} from '@angular/core';
import { SharedService } from '../../services/shared-service.service';
import { CommonModule } from '@angular/common';
import {ScrollPanelModule} from 'primeng/scrollpanel';



@Component({
  selector: 'app-history-component',
  standalone: true,
  imports: [CommonModule, ScrollPanelModule],
  templateUrl: './history-component.component.html',
  styleUrl: './history-component.component.scss'
})
export class HistoryComponentComponent {

  constructor(private sharedService: SharedService){ }

  @Input() isVisible: boolean = false;
  @Output() history: any;
  showMore: boolean = false;

  
  onClick() {
    this.sharedService.toggleIsVisibleHistory();
    this.showMore = false;
  }

  onClickShowMore(){
    this.showMore = true;
  }
  
  ngOnInit() {
    this.sharedService.isVisibleHistory$.subscribe(value => {
      this.isVisible = value; 
    });
  }
  ngDoCheck(): void {
   if(this.isVisible){
    this.history = this.sharedService.getHistory().slice().reverse();;
   }
    
  }

}

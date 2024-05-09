import { Component,Input, Output} from '@angular/core';
import { SharedServiceService } from '../../services/shared-service.service';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-history-component',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './history-component.component.html',
  styleUrl: './history-component.component.scss'
})
export class HistoryComponentComponent {

  constructor(private sharedService: SharedServiceService){ }

  @Input() isVisible: boolean = false;
  @Output() history: any;

  
  onClick() {
    this.sharedService.toggleIsVisibleHistory();
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

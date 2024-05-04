import { Component, EventEmitter, Input, Output, SimpleChanges, ViewChild } from '@angular/core';
import { HeaderComponentComponent } from '../header-component/header-component.component';
import { SharedServiceService } from '../../services/shared-service.service';


@Component({
  selector: 'app-history-component',
  standalone: true,
  imports: [HeaderComponentComponent],
  templateUrl: './history-component.component.html',
  styleUrl: './history-component.component.scss'
})
export class HistoryComponentComponent {

  constructor(private sharedService: SharedServiceService){ }

  @Input() isVisible: boolean = false;

  
  onClick() {
    this.sharedService.toggleIsVisible();
  }
  
  ngOnInit() {
    this.sharedService.isVisible$.subscribe(value => {
      this.isVisible = value; 
    });
  }

}

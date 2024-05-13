import { Component, Input } from '@angular/core';
import { AddCardComponent } from '../add-card/add-card.component';
import { OpenCardComponent } from '../open-card/open-card.component';
import { HeaderComponentComponent } from '../header-component/header-component.component';
import { ListComponentComponent } from '../list-component/list-component.component';
import { SharedService } from '../../services/shared-service.service';

@Component({
  selector: 'app-board',
  standalone: true,
  imports: [
    AddCardComponent,
    OpenCardComponent,
    HeaderComponentComponent,
    ListComponentComponent
    ],
  templateUrl: './board.component.html',
  styleUrl: './board.component.scss'
})
export class BoardComponent {
  constructor(private sharedService: SharedService){

}

@Input() isCardVisible: boolean = false;

ngOnInit(){
  this.sharedService.isVisibleEditCard$.subscribe(value => {
    this.isCardVisible = value; 
  });}
  

}

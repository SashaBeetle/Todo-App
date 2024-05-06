import { Component, Input } from '@angular/core';
import { CardComponentComponent } from '../card-component/card-component.component';
import { AddListComponent } from '../add-list/add-list.component';
import { SharedServiceService } from '../../services/shared-service.service';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-list-component',
  standalone: true,
  imports: [
    CardComponentComponent,
    AddListComponent,
  ],
  templateUrl: './list-component.component.html',
  styleUrl: './list-component.component.scss'
})
export class ListComponentComponent {

  constructor(
    private sharedService: SharedServiceService,
    private apiService: ApiService
  ){}

  @Input() isVisible: boolean = false;

  onClick() {
    this.sharedService.toggleIsVisibleCreateList();
  }

  onClickAddCard() {
    this.sharedService.toggleIsVisibleEditCard();
  }
  
  async ngOnInit(){
    this.sharedService.isVisibleCreateList$.subscribe(value => {
      this.isVisible = value; 
    });

    this.apiService.GetData().subscribe(res => console.log(res));
  }




  items = ['1','2','3','4'];
}



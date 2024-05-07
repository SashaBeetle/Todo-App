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
  
  data: any;

  constructor(
    private sharedService: SharedServiceService,
    private apiService: ApiService
  ){}

  @Input() isVisible: boolean = false;

  onClick() {
    this.sharedService.toggleIsVisibleCreateList();
  }

  onClickAddCard(listId: number) {
    this.sharedService.toggleIsVisibleEditCard();
    this.sharedService.setListId(listId);
  }
  
  async ngOnInit(){
    this.sharedService.isVisibleCreateList$.subscribe(value => {
      this.isVisible = value; 
    });

    this.apiService.getData("https://localhost:7247/api/catalog").subscribe(res =>{
      console.log(res); 
      this.data = res;
      
    });


    
  }





}



import { Component, Input, OnChanges, Output, SimpleChanges } from '@angular/core';
import { CardComponentComponent } from '../card-component/card-component.component';
import { AddListComponent } from '../add-list/add-list.component';
import { SharedService } from '../../services/shared-service.service';
import { ApiService } from '../../services/api.service';
import { AddCardComponent } from '../add-card/add-card.component';
import { OpenCardComponent } from '../open-card/open-card.component';


@Component({
  selector: 'app-list-component',
  standalone: true,
  imports: [
    CardComponentComponent,
    AddListComponent,
    OpenCardComponent,
    AddCardComponent
  ],
  templateUrl: './list-component.component.html',
  styleUrl: './list-component.component.scss'
})
export class ListComponentComponent{

  @Input() isVisible: boolean = false;
  @Output() lists: any;
  @Input() currentBoard: any;

  isAddListVisible: boolean = true;


  constructor(
    private sharedService: SharedService,
    private apiService: ApiService
  ){}



  onClickEdit(list: any) {
    this.sharedService.toggleIsVisibleCreateList();
    this.sharedService.toggleisEditableList();
    this.sharedService.setList(list);
  }

  onClickAddList(){
    this.sharedService.toggleIsVisibleCreateList();
  }

  onClickAddCard(list: any) {
    this.sharedService.toggleIsVisibleEditCard();
    this.sharedService.setList(list);
  }

  onClickDeleteList(listId: number){
    debugger;
    console.log()
    this.apiService.deleteDataById("https://localhost:7247/api/catalog",listId).subscribe(res=>{
      console.log('ListN:', listId);
      const index = this.lists.findIndex((item: { id: number; }) => item.id === listId);
        if (index !== -1) {
          this.lists.splice(index, 1);
          console.log(this.lists);
        }
    })
  }

  
  ngOnInit(){
    // this.data = this.currentBoard;
    console.log('ng', this.lists)
    this.sharedService.isVisibleCreateList$.subscribe(value => {
      this.isVisible = value; 
    });

    this.apiService.getDataById("https://localhost:7247/api/catalog/ForBoard", this.currentBoard.id).subscribe(res =>{
        this.lists = res;
        console.log('t', this.lists)
        this.sortDataByTitle(this.lists);
        this.sharedService.setLists(this.lists)
      }); 


  }

  ngDoCheck(): void {
    if(this.lists.length == 4){
      this.isAddListVisible = false;
      console.log('List',this.isAddListVisible)
    }else{      
      this.isAddListVisible = true;
    }  
  }

  sortDataByTitle(data: any[]): any[] {
    return data.sort((a, b) => {
      if (a.title.toLowerCase() < b.title.toLowerCase()) {
        return -1;
      } else if (a.title.toLowerCase() > b.title.toLowerCase()) {
        return 1;
      } else {
        return 0;
      }
    });
  }
}



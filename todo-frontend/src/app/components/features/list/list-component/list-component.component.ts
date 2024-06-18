import { Component, inject, Input, OnChanges, Output, SimpleChanges } from '@angular/core';
import { CardComponentComponent } from '../../card/card-component/card-component.component';
import { AddListComponent } from '../add-list/add-list.component';
import { SharedService } from '../../../../services/shared-service.service';
import { ApiService } from '../../../../services/api.service';
import { AddCardComponent } from '../../card/add-card/add-card.component';
import { OpenCardComponent } from '../../card/open-card/open-card.component';
import { Store } from '@ngrx/store';
import { BoardState } from '../../../../ngrx/board/board.reducer';
import * as PostActions from '../../../../ngrx/list/list.actions'
import { selectBoard } from '../../../../ngrx/board/board.selectors';



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
  private readonly store:Store<BoardState> = inject(Store)

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
    console.log()
    this.store.dispatch(PostActions.deleteListApi({listId: listId, boardId: this.currentBoard.id}))
  }

  
  ngOnInit(){
    console.log('ng', this.lists)
    this.sharedService.isVisibleCreateList$.subscribe(value => {
      this.isVisible = value; 
    });

    // this.store.select(selectBoard).subscribe(board => {
    //   this.currentBoard = board;
    // });

    // this.apiService.getDataById("https://localhost:7247/api/catalog/ForBoard", this.currentBoard.id).subscribe(res =>{
    //     this.lists = res;
    //     this.sortDataByTitle(this.lists);
    //     this.sharedService.setLists(this.lists)
    //   }); 


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



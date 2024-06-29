import { Component, inject, input, Input, Output } from '@angular/core';
import { CardComponentComponent } from '../../card/card-component/card-component.component';
import { AddListComponent } from '../add-list/add-list.component';
import { SharedService } from '../../../../services/shared-service.service';
import { AddCardComponent } from '../../card/add-card/add-card.component';
import { OpenCardComponent } from '../../card/open-card/open-card.component';
import { Store } from '@ngrx/store';
import { BoardState } from '../../../../ngrx/board/board.reducer';
import * as PostActions from '../../../../ngrx/list/list.actions'
import {checkListLength } from '../../../../utils/list.utilities'
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
  @Output() currentBoard: any;

  @Input() isAddListVisible: boolean = false;


  constructor(
    private sharedService: SharedService,
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
    this.store.dispatch(PostActions.deleteListApi({listId: listId, boardId: this.currentBoard.id}))

    this.sharedService.toggleisAddListVisible(checkListLength((this.currentBoard.catalogs).length - 1))
  }

  ngOnInit(){

    this.sharedService.isAddListVisible$.subscribe(value => {
      this.isAddListVisible = value;
    })

    this.sharedService.isVisibleCreateList$.subscribe(value => {
      this.isVisible = value; 
    });

    this.store.select(selectBoard).subscribe(board => {
      this.currentBoard = board;
      this.isAddListVisible = checkListLength(board.catalogs.length)
    });


    
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



import { Component, inject, input, Input, Output } from '@angular/core';
import { CardComponentComponent } from '../../card/card-component/card-component.component';
import { SharedService } from '../../../../services/shared-service.service';
import { AddCardComponent } from '../../card/add-card/add-card.component';
import { OpenCardComponent } from '../../card/open-card/open-card.component';
import { Store } from '@ngrx/store';
import { BoardState } from '../../../../ngrx/board/board.reducer';
import * as PostActions from '../../../../ngrx/list/list.actions'
import {checkListLength } from '../../../../utils/list.utilities'
import { selectBoard } from '../../../../ngrx/board/board.selectors';
import { AddListComponent } from '../add-list/add-list.component';



@Component({
  selector: 'app-list-component',
  standalone: true,
  imports: [
    CardComponentComponent,
    OpenCardComponent,
    AddCardComponent,
    AddListComponent
  ],
  templateUrl: './list-component.component.html',
  styleUrl: './list-component.component.scss'
})
export class ListComponentComponent{
  private readonly store:Store<BoardState> = inject(Store)

  isCardVisible: boolean = false;
  isListEditable: boolean = false;
  @Input() isVisible: boolean = false;
  @Input() currentList: any;
  @Output() lists: any;
  @Output() currentBoard: any;
  @Output() isAddCardVisible: boolean = false;

  constructor(
    private sharedService: SharedService,
  ){}


  onClickEdit() {
    this.isListEditable = true;
  }

  onClickAddCard() {
    this.isCardVisible = true;
  }

  onClickDeleteList(listId: number){
    this.store.dispatch(PostActions.deleteListApi({listId: listId, boardId: this.currentBoard.id}))
    this.sharedService.toggleisAddListVisible(checkListLength((this.currentBoard.catalogs).length - 1))
  }

  handleOutputAddCardEvent(value: boolean) {
    this.isCardVisible = value;
  }

  handleOutputAddListisVisibleEvent(value: boolean) {
    this.isListEditable = value;
  }

  ngOnInit(){
    this.sharedService.isVisibleCreateList$.subscribe(value => {
      this.isVisible = value; 
    });

    this.store.select(selectBoard).subscribe(board => {
      this.currentBoard = board;
    });
  }
}



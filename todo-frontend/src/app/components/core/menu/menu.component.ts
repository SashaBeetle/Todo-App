import { Component, inject, Input } from '@angular/core';
import { AddBoardComponent } from '../../features/board/add-board/add-board.component';
import { Store } from '@ngrx/store';
import { CommonModule } from '@angular/common';
import * as PostActions from '../../../ngrx/board/board.actions'
import { BoardState } from '../../../ngrx/board/board.reducer';
import { selectBoards } from '../../../ngrx/board/board.selectors';
import { SharedService } from '../../../services/shared-service.service';

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [
    AddBoardComponent,
    CommonModule
  ],
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.scss'
})
export class MenuComponent {
  private readonly store:Store<BoardState> = inject(Store)

  constructor(
    private sharedService: SharedService,
  ){
  
  }
  @Input() isVisible = false;
  @Input() lists: any;
  boards: any;
  board: any;


  onOpenBoard(board: any){ 
    this.sharedService.toggleisVisibleBoard();
    this.store.dispatch(PostActions.getBoardApi({boardId: board.id}))
  }

  onCreateBoard(){
    this.sharedService.toggleisVisibleCreateBoard();
  }

  onRefresh(){
    this.store.dispatch(PostActions.getBoardsApi())
  }

  onEditBoard(board: any){
    this.isVisible = true;
    this.board = board
    this.sharedService.toggleisEditableBoard();
    this.sharedService.toggleisVisibleCreateBoard();
    this.store.dispatch(PostActions.AddCurrentBoard({currentBoard: board}));
  }

  onDeleteBoard(boardId: number){
    this.store.dispatch(PostActions.deleteBoardApi({boardId: boardId}))
  }

  ngOnInit(): void{
    this.sharedService.isVisibleCreateBoard$.subscribe(value => {
      this.isVisible = value; 
    });
    
    this.store.dispatch(PostActions.getBoardsApi())
    
    this.store.select(selectBoards).subscribe(boards => {
      this.boards = boards;
    });
  }
}

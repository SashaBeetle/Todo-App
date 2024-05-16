import { Component, inject, Input } from '@angular/core';
import { ApiService } from '../../../services/api.service';
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
    private apiService: ApiService,
    private sharedService: SharedService,
  ){
  
  }
  @Input() isVisible = false;
  @Input() lists: any;
  boards: any;
  board: any;


  onOpenBoard(board: any){
    this.sharedService.toggleisVisibleBoard();
    this.store.dispatch(PostActions.AddCurrentBoard({currentBoard: board}));
  }

  onCreateBoard(){
    this.sharedService.toggleisVisibleCreateBoard();
  }

  onEditBoard(board: any){
    this.isVisible = true;
    this.board = board
    this.sharedService.toggleisEditableBoard();
    this.sharedService.toggleisVisibleCreateBoard();
    this.store.dispatch(PostActions.AddCurrentBoard({currentBoard: board}));
  }

  onDeleteBoard(boardId: number){
    this.apiService.deleteDataById("https://localhost:7247/api/Boards",boardId).subscribe(res=>{
      const index = this.boards.findIndex((item: { id: number; }) => item.id === boardId);
        if (index !== -1) {
          this.boards.splice(index, 1);
          console.log(this.boards);
        }
    })
  }

  ngOnInit(): void{
    this.sharedService.isVisibleCreateBoard$.subscribe(value => {
      this.isVisible = value; 
    });
    
    this.store.dispatch(PostActions.getBoardsTest())
    
    this.store.select(selectBoards).subscribe(boards => {
      this.boards = boards;
    });
    this.sortDataByTitle(this.boards)



    

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

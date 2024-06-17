import { CommonModule } from '@angular/common';
import { Component, inject, Input, Output } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { SharedService } from '../../../../services/shared-service.service';
import { Store } from '@ngrx/store';
import { BoardState } from '../../../../ngrx/board/board.reducer';
import * as PostActions from '../../../../ngrx/board/board.actions'


@Component({
  selector: 'app-add-board',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    CommonModule
  ],
  templateUrl: './add-board.component.html',
  styleUrl: './add-board.component.scss'
})
export class AddBoardComponent {
  private readonly store:Store<BoardState> = inject(Store);
  
  constructor(
    private sharedService: SharedService
  )
    {
    this.boardForm = new FormGroup({
      title: new FormControl("", [Validators.required, Validators.maxLength(12)]),
      catalogsId: new FormControl([])
      })
    }
  @Input() boards : any;
  @Input() isEditable = false;
  @Output() boardsChange: any;
  @Input() currentBoard: any;
  boardForm: FormGroup;


  onSubmitCreateBoard(){
    if(this.boardForm.valid){ 
      const jsonData = JSON.stringify(this.boardForm.value);
      this.store.dispatch(PostActions.addBoardApi({board: jsonData}))
      this.sharedService.toggleisVisibleCreateBoard();
    }    
  }

  onSubmitEditBoard(){
    if(this.boardForm.valid){ 
      this.store.dispatch(PostActions.changeBoardApi({boardId: this.currentBoard.id, boardTitle: this.boardForm.get('title')?.value}))

      this.sharedService.toggleisVisibleCreateBoard();
      this.sharedService.toggleisEditableBoard();
    }    
  }

  onClick(){
    this.sharedService.toggleisVisibleCreateBoard();
    console.log(this.isEditable)

    if(this.isEditable){
      this.sharedService.toggleisEditableBoard();
    }
  }

  ngOnInit(){
    this.sharedService.isEditableBoard$.subscribe(value => {
      this.isEditable = value;
    })

    this.boardsChange = this.boards
  }
}

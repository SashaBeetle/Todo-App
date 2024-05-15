import { CommonModule, JsonPipe } from '@angular/common';
import { Component, inject, Input, Output } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { SharedService } from '../../../../../services/shared-service.service';
import { ApiService } from '../../../../../services/api.service';
import { Store } from '@ngrx/store';
import { BoardState } from '../../../../ngrx/board/board.reducer';
import { selectBoard } from '../../../../ngrx/board/board.selectors';

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

  constructor(private sharedService: SharedService, private apiService: ApiService){
    this.boardForm = new FormGroup({
      title: new FormControl("", [Validators.required, Validators.maxLength(12)]),
      catalogsId: new FormControl([])
    })
  }
  @Input() boards : any;
  @Input() isEditable = false;
  @Output() boardsChange: any;
  currentBoard: any;
  boardForm: FormGroup;


  onSubmitCreateBoard(){
    if(this.boardForm.valid){ 
      const jsonData = JSON.stringify(this.boardForm.value);
      this.apiService.postData('https://localhost:7247/api/Boards', jsonData) 
        .subscribe(response => {
          this.boardForm.value.id = response.id;
          this.boards.push(this.boardForm.value)
          console.log('Form submitted successfully!', jsonData);
        }, error => {
          console.error('Error submitting form:', error, jsonData);
        });
        this.sharedService.toggleisVisibleCreateBoard();
    }    
  }

  onSubmitEditBoard(){
    if(this.boardForm.valid){ 
      this.apiService.patchData(`https://localhost:7247/api/Boards/${this.currentBoard.id}?title=${this.boardForm.get('title')?.value}`, 1) 
        .subscribe(response => {
          debugger;
          const currentIndex = this.boards.findIndex((a: { title: any; }) => a.title === this.currentBoard.title);
          
          this.boards[currentIndex].title = this.boardForm.get('title')?.value;
          console.log('Succ');
          console.log('Form submitted successfully!', response);
        }, error => {
          console.error('Error submitting form:', error);
        });
        
        this.sharedService.toggleisVisibleCreateBoard();
        this.sharedService.toggleisEditableBoard();
    }    
  }

  onClick(){
    this.sharedService.toggleisVisibleCreateBoard();
    if(this.isEditable){
      this.sharedService.toggleisEditableBoard();
    }
  }

  ngOnInit(){
    this.sharedService.isEditableBoard$.subscribe(value => {
      this.isEditable = value;
    })

    this.store.select(selectBoard).subscribe(board => {
      this.currentBoard = board;
    });

    this.boardsChange = this.boards


    
  }
}

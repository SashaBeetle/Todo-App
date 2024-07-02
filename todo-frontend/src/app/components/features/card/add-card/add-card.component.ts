import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { SharedService } from '../../../../services/shared-service.service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiService } from '../../../../services/api.service';
import { PriorityConstants } from '../../../../constants/priorityConstants';
import { CommonModule } from '@angular/common';
import { Store } from '@ngrx/store';
import { BoardState } from '../../../../ngrx/board/board.reducer';
import * as PostActions from '../../../../ngrx/card/card.actions'


@Component({
  selector: 'app-add-card',
  standalone: true,
  imports: [
    ReactiveFormsModule, 
    CommonModule
  ],
  templateUrl: './add-card.component.html',
  styleUrl: './add-card.component.scss'
})
export class AddCardComponent {
  private readonly store:Store<BoardState> = inject(Store);

  constructor(private sharedService: SharedService, private apiService: ApiService){
    this.cardForm = new FormGroup({})
  }

  @Input() isVisible: boolean = false;
  @Input() currentBoard: any;
  @Input() currentList: any;

  @Output() outputEvent = new EventEmitter<boolean>();
  
  priority: any = PriorityConstants.priority;
  lists: any;
  card: any;
  cardForm: FormGroup;

  onCreate(){
    if(this.cardForm.valid){
      this.store.dispatch(PostActions.postCardApi({card: this.cardForm.value, boardId: this.currentBoard.id}))
  
      this.onClickClose();
    }
  }


  onClickClose() {
    this.outputEvent.emit(false);
  }
  
  ngOnInit() {
    this.sharedService.isVisibleEditCard$.subscribe(value => {
      this.isVisible = value; 
    });
    
    this.lists = this.currentBoard.catalogs;
    
      this.cardForm = new FormGroup({
        title: new FormControl("New Card", [Validators.required, Validators.maxLength(12)]),
        description: new FormControl("", [Validators.required, Validators.maxLength(256)]),
        priority: new FormControl("", [Validators.required]),
        DueDate: new FormControl('', Validators.required),
        catalogId: new FormControl('', Validators.required)
      })
  }
}
  

  





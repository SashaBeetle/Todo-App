import { Component, inject, Input } from '@angular/core';
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
  @Input() isEditable: boolean = false;
  @Input() currentBoard: any;
  @Input() currentList: any;

  priority: any = PriorityConstants.priority;
  lists: any;
  card: any;
  cardForm: FormGroup;

  onCreate(){
    if(this.cardForm.valid){
      this.store.dispatch(PostActions.postCardApi({card: this.cardForm.value, boardId: this.currentBoard.id}))
  
      this.onClick();
    }
    
  }

  onUpdate(){
    if(this.cardForm.valid){
      this.card.title = this.cardForm.get('title')?.value;
      this.card.description = this.cardForm.get('description')?.value;
      this.card.dueDate = this.cardForm.get('DueDate')?.value;
      this.card.priority = this.cardForm.get('priority')?.value;
  
  
      this.apiService.patchData(`https://localhost:7247/api/cards?boardId=2`, this.card)
        .subscribe(response => {
          console.log('Form submitted successfully!');
        }, error => {
          console.error('Error submitting form:', error);
        });
        console.log('Data',this.card);
  
        this.sharedService.toggleisEditableCard();
        this.onClick();
    }
    
  }

  onClick() {
    this.sharedService.toggleIsVisibleEditCard();
  }
  
  ngOnInit() {
    this.sharedService.isVisibleEditCard$.subscribe(value => {
      this.isVisible = value; 
    });

    this.sharedService.isEditableCard$.subscribe(value => {
      this.isEditable = value; 
    });
    
    this.lists = this.currentBoard.catalogs;

    if(this.isEditable){
      this.cardForm = new FormGroup({
        title: new FormControl(this.card.title, [Validators.required, Validators.maxLength(12)]),
        description: new FormControl(this.card.description, [Validators.required, Validators.maxLength(256)]),
        priority: new FormControl(this.card.priority, [Validators.required]),
        DueDate: new FormControl(this.card.dueDate, Validators.required),
        catalogId: new FormControl(this.card.catalogId, Validators.required)
      })
    }else{
      this.cardForm = new FormGroup({
        title: new FormControl("New Card", [Validators.required, Validators.maxLength(12)]),
        description: new FormControl("", [Validators.required, Validators.maxLength(256)]),
        priority: new FormControl("", [Validators.required]),
        DueDate: new FormControl('', Validators.required),
        catalogId: new FormControl('', Validators.required)
      })
    }
  }
}
  

  





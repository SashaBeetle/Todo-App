import { Component, inject, Input } from '@angular/core';
import { SharedService } from '../../../../services/shared-service.service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiService } from '../../../../services/api.service';
import { PriorityConstants } from '../../../../constants/priorityConstants';
import { CommonModule } from '@angular/common';
import { Store } from '@ngrx/store';
import { BoardState } from '../../../../ngrx/board/board.reducer';

@Component({
  selector: 'app-add-card',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './add-card.component.html',
  styleUrl: './add-card.component.scss'
})
export class AddCardComponent {
  private readonly store:Store<BoardState> = inject(Store);

  constructor(private sharedService: SharedService, private apiService: ApiService){
    this.cardForm = new FormGroup({
      title: new FormControl("New Card", [Validators.required, Validators.maxLength(12)]),
      description: new FormControl("", [Validators.required, Validators.maxLength(256)]),
      priority: new FormControl("", [Validators.required]),
      DueDate: new FormControl("", Validators.required),
      listId: new FormControl('', Validators.required)
    })
  }

  @Input() isVisible: boolean = false;
  @Input() isEditable: boolean = false;
  @Input() currentBoard: any;

  priority: any = PriorityConstants.priority;
  list: any;
  lists: any;
  card: any;
  cardForm: FormGroup;

  onCreate(){
    if(this.cardForm.valid){
      const formData = this.cardForm.value;
      const jsonData = JSON.stringify(formData);
      
      this.apiService.postData('https://localhost:7247/api/cards'+`?listId=${formData.listId}&boardId=${this.currentBoard.id}`, jsonData) //
        .subscribe(response => {
          this.cardForm.value.id = response.id;
          this.sharedService.getList().cardsId.push(response.id)
          console.log('Form submitted successfully!', jsonData);
        }, error => {
          console.error('Error submitting form:', error, jsonData);
        });
        
  
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
    
    this.apiService.getData(`https://localhost:7247/api/catalog/ForBoard/${this.currentBoard.id}`).subscribe(res =>{
      this.lists = res;  
    })  
  }

  ngDoCheck() {
   if(this.isVisible){
    this.card = this.sharedService.getCard();
   }
  }
}
  

  





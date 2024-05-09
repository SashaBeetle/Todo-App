import { Component, Input, SimpleChanges } from '@angular/core';
import { SharedServiceService } from '../../services/shared-service.service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiService } from '../../services/api.service';
import { PriorityConstants } from '../../Constants/priorityConstants';

@Component({
  selector: 'app-add-card',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './add-card.component.html',
  styleUrl: './add-card.component.scss'
})
export class AddCardComponent {
  constructor(private sharedService: SharedServiceService, private apiService: ApiService){
    this.cardForm = new FormGroup({
      title: new FormControl("New Card", [Validators.required, Validators.maxLength(12)]),
      description: new FormControl(""),
      priority: new FormControl(""),
      DueDate: new FormControl(""),
      listId: new FormControl('')
    })
  }

  @Input() isVisible: boolean = false;
  priority: any = PriorityConstants.priority;
  list: any;
  lists: any;
  card: any;
  cardForm: FormGroup;

  onSubmit(){
    const formData = this.cardForm.value;
    const jsonData = JSON.stringify(formData);
    
    this.apiService.postData('https://localhost:7247/api/cards'+`?listId=${formData.listId}`, jsonData) //
      .subscribe(response => {
        this.cardForm.value.id = response.id;
        this.sharedService.getList().cardsId.push(response.id)
        console.log('Form submitted successfully!', jsonData);
      }, error => {
        console.error('Error submitting form:', error, jsonData);
      });
      

    this.onClick();
  }

  onUpdate(){
    this.card.title = this.cardForm.get('title')?.value;
    this.card.description = this.cardForm.get('description')?.value;
    this.card.dueDate = this.cardForm.get('DueDate')?.value;
    this.card.priority = this.cardForm.get('priority')?.value;


    this.apiService.patchData(`https://localhost:7247/api/cards`, this.card)
      .subscribe(response => {
        console.log('Form submitted successfully!');
      }, error => {
        console.error('Error submitting form:', error);
      });
      console.log('Data',this.card);
  }

  onClick() {
    this.sharedService.toggleIsVisibleEditCard();
  }
  
  ngOnInit() {
    this.sharedService.isVisibleEditCard$.subscribe(value => {
      this.isVisible = value; 
    });

    this.apiService.getData("https://localhost:7247/api/catalog").subscribe(res =>{
      this.lists = res;  
    })  
  }

  ngDoCheck(): void {
   if(this.isVisible){
    this.card = this.sharedService.getCard();
   }
  }
}
  

  





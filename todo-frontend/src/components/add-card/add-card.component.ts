import { Component, Input, SimpleChanges } from '@angular/core';
import { SharedServiceService } from '../../services/shared-service.service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiService } from '../../services/api.service';

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
      dateDue: new FormControl("")
    })
  }


  @Input() isVisible: boolean = false;
 
  list: any;

  cardForm: FormGroup;

  onSubmit(){
    const formData = this.cardForm.value;
    const jsonData = JSON.stringify(formData);
    this.apiService.postData('https://localhost:7247/api/cards'+`?listId=${this.sharedService.getList().id}`, jsonData) 
      .subscribe(response => {
        this.cardForm.value.id = response.id;
        this.sharedService.getList().cardsId.push(response.id)
        console.log('Form submitted successfully!', jsonData);
      }, error => {
        console.error('Error submitting form:', error, jsonData);
      });

    this.onClick();
  }

  onClick() {
    this.sharedService.toggleIsVisibleEditCard();
  }
  
  ngOnInit() {
    this.sharedService.isVisibleEditCard$.subscribe(value => {
      this.isVisible = value; 
    });
  
  }

  ngOnChanges(changes: SimpleChanges): void {  
  }


}

import { Component, Input } from '@angular/core';
import { SharedServiceService } from '../../services/shared-service.service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-add-list',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './add-list.component.html',
  styleUrl: './add-list.component.scss'
})
export class AddListComponent {
  
  @Input() data: any;
  listForm: FormGroup;

  constructor(private sharedService: SharedServiceService, private apiService: ApiService){
    this.listForm = new FormGroup({
      title: new FormControl("", [Validators.required, Validators.maxLength(12)]),
      cardsId: new FormControl([])
    })
  }

  onClick() {
    this.sharedService.toggleIsVisibleCreateList();
  }

  onSubmitCreateList(){
    debugger;
    const jsonData = JSON.stringify(this.listForm.value);
    this.apiService.postData('https://localhost:7247/api/catalog', jsonData) 
      .subscribe(response => {
        this.listForm.value.id = response.id;
        this.data.push(this.listForm.value)
        console.log('Form submitted successfully!', jsonData);
      }, error => {
        console.error('Error submitting form:', error, jsonData);
      });
  }
}

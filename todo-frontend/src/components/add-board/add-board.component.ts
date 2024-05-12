import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { SharedService } from '../../services/shared-service.service';
import { ApiService } from '../../services/api.service';

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
  constructor(private sharedService: SharedService, private apiService: ApiService){
    this.boardForm = new FormGroup({
      title: new FormControl("", [Validators.required, Validators.maxLength(12)]),
      catalogsId: new FormControl([])
    })
  }
  @Input() boards : any;
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

  onClick(){
    this.sharedService.toggleisVisibleCreateBoard();
  }
}

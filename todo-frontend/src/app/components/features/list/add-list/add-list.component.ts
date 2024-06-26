import { Component, inject, Input } from '@angular/core';
import { SharedService } from '../../../../services/shared-service.service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Store } from '@ngrx/store';
import { BoardState } from '../../../../ngrx/board/board.reducer';
import * as PostActions from '../../../../ngrx/list/list.actions'
import {checkListLength } from '../../../../utils/list.utilities'



@Component({
  selector: 'app-add-list',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './add-list.component.html',
  styleUrl: './add-list.component.scss'
})
export class AddListComponent {
  private readonly store:Store<BoardState> = inject(Store)

  @Input() lists: any;
  @Input() editable: boolean = false;
  @Input() list: any;
  @Input() board: any;
  listForm: FormGroup;

  constructor(
    private sharedService: SharedService, 
    )
    {
    this.listForm = new FormGroup({
      title: new FormControl("", [Validators.required, Validators.maxLength(15)]),
    })
  }

  onClick() {
    if(this.editable){
      this.sharedService.toggleisEditableList();
    }
    this.sharedService.toggleIsVisibleCreateList();
  }

  onSubmitCreateList(){
    if(this.listForm.valid){
      const formData = this.listForm.value;
      formData['boardId'] = this.board.id;
      const jsonData = JSON.stringify(formData);

      this.store.dispatch(PostActions.postListApi({ boardId: this.board.id, list: jsonData }))

      this.sharedService.toggleisAddListVisible(checkListLength(this.lists.length))
      console.warn(this.lists.length)
    }    
  }

  onSubmitEditList(){
    if(this.listForm.valid){
      this.list = this.sharedService.getList();
            
      this.store.dispatch(PostActions.patchListApi({
        list: this.list,
        boardId: this.board.id,
        newListTitle: this.listForm.get('title')?.value
      }))
  
      this.sharedService.toggleisEditableList();
      this.sharedService.toggleIsVisibleCreateList();
    }
    
  }

  ngOnInit() {
    this.sharedService.isEditableList$.subscribe(value => {
      this.editable = value; 
    });
  }
  
}

import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { SharedService } from '../../../../services/shared-service.service';
import { CommonModule } from '@angular/common';
import { BannerComponent } from '../../../core/banner/banner/banner.component';
import { PriorityConstants } from '../../../../constants/priorityConstants';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { BoardState } from '../../../../ngrx/board/board.reducer';
import * as PostActions from '../../../../ngrx/card/card.actions'

@Component({
  selector: 'app-open-card',
  standalone: true,
  imports: [
    BannerComponent,
    CommonModule,
    ReactiveFormsModule
    ],
  templateUrl: './open-card.component.html',
  styleUrl: './open-card.component.scss'
})
export class OpenCardComponent {
  private readonly store:Store<BoardState> = inject(Store);

  constructor(private sharedService: SharedService){
    this.cardForm = new FormGroup({})
  }
  @Input() isChoose: boolean = false;
  isEditable: boolean = false
  @Input() card: any;
  @Input() currentBoard: any;
  @Input() currentList: any;

  cardForm: FormGroup;
  history: any;
  @Output() outputEvent = new EventEmitter<boolean>();

  priority: any = PriorityConstants.priority;

  
  onClickClose() {
    this.outputEvent.emit(false);
  }

  onClickEditCard(){
    this.isEditable = !this.isEditable
  }
  
  onClickPatchCard(){
    if(this.cardForm.valid){
      this.cardForm.value["id"] = this.card.id
      this.store.dispatch(PostActions.patchCardApi({card: this.cardForm.value, boardId: this.currentBoard.id}))
      this.onClickEditCard();
    }
  }

  ngOnInit() {
    this.sharedService.isChooseCardHistory$.subscribe(value =>{
      this.isChoose = value;
    })

    this.cardForm = new FormGroup({
      title: new FormControl(this.card.title, [Validators.required, Validators.maxLength(12)]),
      description: new FormControl(this.card.description, [Validators.required, Validators.maxLength(256)]),
      priority: new FormControl(this.card.priority, [Validators.required]),
      DueDate: new FormControl('', Validators.required),
      catalogId: new FormControl('', Validators.required)
    })
}
}


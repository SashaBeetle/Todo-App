import { Component, inject, Input, Output } from '@angular/core';
import { SharedService } from '../../../../services/shared-service.service';
import { OpenCardComponent } from '../open-card/open-card.component';
import { ApiService } from '../../../../services/api.service';
import { CommonModule } from '@angular/common';
import { Store } from '@ngrx/store';
import { BoardState } from '../../../../ngrx/board/board.reducer';
import * as PostActions from '../../../../ngrx/card/card.actions'
import { AddCardComponent } from "../add-card/add-card.component";
import { BannerComponent } from '../../../core/banner/banner/banner.component';

@Component({
    selector: 'app-card-component',
    standalone: true,
    templateUrl: './card-component.component.html',
    styleUrl: './card-component.component.scss',
    imports: [
      OpenCardComponent,
      BannerComponent,
      CommonModule,
      AddCardComponent
    ]
})
export class CardComponentComponent {
  private readonly store:Store<BoardState> = inject(Store);

  @Input() card: any;
  @Input() list: any;
  @Input() history:any;
  @Input() currentBoard: any;
  @Input() isCardVisible: boolean = false;
  isOpenCardVisible: boolean = false;
  
  @Output() currentList: any;

  sharedData: any;
    
  constructor(
    private sharedService: SharedService, 
    private apiService: ApiService,
  ){}

  onClickOpenCard() {
    this.isOpenCardVisible = true;

    this.apiService.getData(`https://localhost:7247/api/v1/historyitem/ForCard${this.card.id}`)
    .subscribe(response => {
      this.history = response;
      this.sharedService.setHistory(response);
      console.log('Get request successful!', this.history);
    }, error => {
      console.error('Error Getting data:', error);
    });
 
  }
  ngOnInit(): void {
    this.currentList = this.list;

    this.sharedService.isVisibleCard$.subscribe(value => {
      this.isCardVisible = value; 
    });
  }

  onClickDelete(){
    this.store.dispatch(PostActions.deleteCardApi({cardId: this.card.id, boardId: this.currentBoard.id}))
  }

  onClickPatch(listId: number){
    this.store.dispatch(PostActions.patchCardApi({card: this.createCardDTO(listId), boardId: this.currentBoard.id}))
  }

  handleOutputEvent(value: boolean) {
    this.isOpenCardVisible = value;
  }

  createCardDTO(newListId: number): any {
    const updatedCard = {
      ...this.card,
      catalogId: newListId
    };

    return updatedCard
  }
}

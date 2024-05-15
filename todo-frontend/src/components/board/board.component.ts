import { Component, inject, Input, Output } from '@angular/core';
import { AddCardComponent } from '../add-card/add-card.component';
import { OpenCardComponent } from '../open-card/open-card.component';
import { HeaderComponentComponent } from '../header-component/header-component.component';
import { ListComponentComponent } from '../list-component/list-component.component';
import { SharedService } from '../../services/shared-service.service';
import { select, Store } from '@ngrx/store';
import { BoardState } from '../../app/ngrx/board/board.reducer';
import { CommonModule } from '@angular/common';
import * as PostActions from '../../app/ngrx/board/board.actions'
import { selectBoard } from '../../app/ngrx/board/board.selectors';

@Component({
  selector: 'app-board',
  standalone: true,
  imports: [
    AddCardComponent,
    OpenCardComponent,
    HeaderComponentComponent,
    ListComponentComponent,
    CommonModule
    ],
  templateUrl: './board.component.html',
  styleUrl: './board.component.scss'
})
export class BoardComponent {
  private readonly store:Store<BoardState> = inject(Store);

  constructor(private sharedService: SharedService){

}

@Input() isCardVisible: boolean = false;
@Output() currentBoard: any;

ngOnInit(){
  this.sharedService.isVisibleEditCard$.subscribe(value => {
    this.isCardVisible = value;
  });

  this.store.select(selectBoard).subscribe(board => {
    this.currentBoard = board;
  });

  console.log('board', this.currentBoard)
}
}

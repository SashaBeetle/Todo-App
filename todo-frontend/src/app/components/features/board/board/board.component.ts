import { Component, inject, Input, Output } from '@angular/core';
import { AddCardComponent } from '../../card/add-card/add-card.component';
import { OpenCardComponent } from '../../card/open-card/open-card.component';
import { HeaderComponentComponent } from '../header-component/header-component.component';
import { ListComponentComponent } from '../../list/list-component/list-component.component';
import { SharedService } from '../../../../services/shared-service.service';
import { Store } from '@ngrx/store';
import { BoardState } from '../../../../ngrx/board/board.reducer';
import { CommonModule } from '@angular/common';
import { selectBoard } from '../../../../ngrx/board/board.selectors';
import {checkListLength } from '../../../../utils/list.utilities'
import { AddListComponent } from '../../list/add-list/add-list.component';
import { HistoryComponentComponent } from '../../history/history-component/history-component.component';

@Component({
  selector: 'app-board',
  standalone: true,
  imports: [
    AddCardComponent,
    OpenCardComponent,
    AddListComponent,
    HeaderComponentComponent,
    HistoryComponentComponent,
    ListComponentComponent,
    CommonModule
    ],
  templateUrl: './board.component.html',
  styleUrl: './board.component.scss'
})
export class BoardComponent {
private readonly store:Store<BoardState> = inject(Store);

constructor(){}

isAddListVisibleComponent: boolean = false;
isHistoryVisibleComponent: boolean = false;
@Input() isAddListVisible: boolean = false;
@Output() currentBoard: any;

onClickAddList(){
  this.isAddListVisibleComponent = true;
}

handleOutputEvent(value: boolean) {
  this.isAddListVisibleComponent = value;
}

handleOutputEventHistory(value: boolean) {
  this.isHistoryVisibleComponent = value;
}

ngOnInit(){
  this.store.select(selectBoard).subscribe(board => {
    this.currentBoard = board;
    this.isAddListVisible = checkListLength(board.catalogs.length)
  });
}
}

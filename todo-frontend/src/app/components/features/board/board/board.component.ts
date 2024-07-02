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

@Component({
  selector: 'app-board',
  standalone: true,
  imports: [
    AddCardComponent,
    OpenCardComponent,
    AddListComponent,
    HeaderComponentComponent,
    ListComponentComponent,
    CommonModule
    ],
  templateUrl: './board.component.html',
  styleUrl: './board.component.scss'
})
export class BoardComponent {
  private readonly store:Store<BoardState> = inject(Store);

  constructor(private sharedService: SharedService){}

isAddListVisibleComponent: boolean = false;
@Input() isAddListVisible: boolean = false;
@Input() isCardVisible: boolean = false;
@Output() currentBoard: any;

onClickAddList(){
  this.isAddListVisibleComponent = true;
}

handleOutputEvent(value: boolean) {
  this.isAddListVisibleComponent = value;
}

ngOnInit(){
  this.sharedService.isVisibleEditCard$.subscribe(value => {
    this.isCardVisible = value;
  });

  this.sharedService.isAddListVisible$.subscribe(value => {
    this.isAddListVisible = value;
  })

  this.store.select(selectBoard).subscribe(board => {
    this.currentBoard = board;
    this.isAddListVisible = checkListLength(board.catalogs.length)
  });
}
}

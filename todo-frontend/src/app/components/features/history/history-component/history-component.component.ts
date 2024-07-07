import { Component,EventEmitter,inject,Input, Output} from '@angular/core';
import { selectBoard } from '../../../../ngrx/board/board.selectors';
import { ApiService } from "../../../../services/api.service";
import { CommonModule } from '@angular/common';
import {ScrollPanelModule} from 'primeng/scrollpanel';
import { Store } from '@ngrx/store';
import { BoardState } from '../../../../ngrx/board/board.reducer';



@Component({
  selector: 'app-history-component',
  standalone: true,
  imports: [
    CommonModule,
    ScrollPanelModule
    ],
  templateUrl: './history-component.component.html',
  styleUrl: './history-component.component.scss'
})
export class HistoryComponentComponent {
  private readonly store:Store<BoardState> = inject(Store);

  constructor(private apiService: ApiService){ }

  history: any;
  showMore: boolean = false;
  currentBoard: any;

  @Output() outputEvent = new EventEmitter<boolean>();


  onClick() {
    this.showMore = false;
    this.outputEvent.emit(false);
  }

  onClickShowMore(){
    this.showMore = true;
  }
  
  ngOnInit() {
    this.store.select(selectBoard).subscribe(board => {
      this.currentBoard = board;
    });

    this.apiService.getDataById(`https://localhost:7247/api/v1/historyitems/board`, this.currentBoard.id)
    .subscribe(response => {
      this.history = response;
      this.history = this.history.slice().reverse();
      console.log('Get request successful!', this.history);
    }, error => {
      console.error('Error Getting data:', error);
    });
  } 
}


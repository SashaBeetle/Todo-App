import { Component, ComponentRef, Input, Output, ViewChild, ViewContainerRef } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { SharedService } from '../../services/shared-service.service';
import { AddBoardComponent } from '../add-board/add-board.component';
import { BoardComponent } from '../board/board.component';

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [
    AddBoardComponent
  ],
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.scss'
})
export class MenuComponent {
  constructor(
    private apiService: ApiService,
    private sharedService: SharedService,

  ){
  
  }
  @Input() isVisible = false;


  boards: any;



  onCreateBoard(){
    this.sharedService.toggleisVisibleCreateBoard();
  }

  onDeleteBoard(boardId: number){
    this.apiService.deleteDataById("https://localhost:7247/api/Boards",boardId).subscribe(res=>{
      const index = this.boards.findIndex((item: { id: number; }) => item.id === boardId);
        if (index !== -1) {
          this.boards.splice(index, 1);
          console.log(this.boards);
        }
    })
  }

  ngOnInit(){
    this.sharedService.isVisibleCreateBoard$.subscribe(value => {
      this.isVisible = value; 
    });

    this.apiService.getData("https://localhost:7247/api/Boards").subscribe(res =>{
      console.log(res); 
      this.boards = res;
      this.sortDataByTitle(this.boards);
    }); 
  }


  sortDataByTitle(data: any[]): any[] {
    return data.sort((a, b) => {
      if (a.title.toLowerCase() < b.title.toLowerCase()) {
        return -1;
      } else if (a.title.toLowerCase() > b.title.toLowerCase()) {
        return 1;
      } else {
        return 0;
      }
    });
  }
}

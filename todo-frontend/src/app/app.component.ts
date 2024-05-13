import { Component, Input } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CardComponentComponent } from '../components/card-component/card-component.component';
import { ListComponentComponent } from '../components/list-component/list-component.component';
import { HeaderComponentComponent } from '../components/header-component/header-component.component';
import { HistoryComponentComponent } from '../components/history-component/history-component.component';
import { AddCardComponent } from '../components/add-card/add-card.component';
import { OpenCardComponent } from '../components/open-card/open-card.component';
import { MenuComponent } from '../components/menu/menu.component';
import { BoardComponent } from '../components/board/board.component';
import { SharedService } from '../services/shared-service.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CardComponentComponent,
    ListComponentComponent,
    HeaderComponentComponent,
    HistoryComponentComponent,
    AddCardComponent,
    OpenCardComponent,
    MenuComponent,
    BoardComponent,
    RouterOutlet
  ], 
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  constructor(private sharedService: SharedService){}
  title = 'To Do';

  @Input() isVisible: boolean = false;

  ngOnInit(){
    this.sharedService.isVisibleBoard$.subscribe(value => {
      this.isVisible = value; 
    });

  }
}

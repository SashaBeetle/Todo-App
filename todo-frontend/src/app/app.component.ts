import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CardComponentComponent } from '../components/card-component/card-component.component';
import { ListComponentComponent } from '../components/list-component/list-component.component';
import { HeaderComponentComponent } from '../components/header-component/header-component.component';
import { HistoryComponentComponent } from '../components/history-component/history-component.component';
import { AddCardComponent } from '../components/add-card/add-card.component';
import { OpenCardComponent } from '../components/open-card/open-card.component';

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
    RouterOutlet
  ], 
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'To Do';
}

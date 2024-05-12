import { Component } from '@angular/core';
import { AddCardComponent } from '../add-card/add-card.component';
import { OpenCardComponent } from '../open-card/open-card.component';
import { HeaderComponentComponent } from '../header-component/header-component.component';
import { ListComponentComponent } from '../list-component/list-component.component';

@Component({
  selector: 'app-board',
  standalone: true,
  imports: [
    AddCardComponent,
    OpenCardComponent,
    HeaderComponentComponent,
    ListComponentComponent
    ],
  templateUrl: './board.component.html',
  styleUrl: './board.component.scss'
})
export class BoardComponent {

}

import { Component } from '@angular/core';
import { CardComponentComponent } from '../card-component/card-component.component';

@Component({
  selector: 'app-list-component',
  standalone: true,
  imports: [
    CardComponentComponent,
  ],
  templateUrl: './list-component.component.html',
  styleUrl: './list-component.component.scss'
})
export class ListComponentComponent {
  items = ['1','2','3','4'];
}

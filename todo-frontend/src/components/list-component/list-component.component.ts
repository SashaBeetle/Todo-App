import { Component } from '@angular/core';

@Component({
  selector: 'app-list-component',
  standalone: true,
  imports: [],
  templateUrl: './list-component.component.html',
  styleUrl: './list-component.component.scss'
})
export class ListComponentComponent {
  items = ['1','2','3','4'];
}

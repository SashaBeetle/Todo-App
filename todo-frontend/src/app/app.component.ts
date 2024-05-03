import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CardComponentComponent } from '../components/card-component/card-component.component'
import { ListComponentComponent } from '../components/list-component/list-component.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CardComponentComponent,
    ListComponentComponent,
    RouterOutlet
  ], 
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'To Do';
}

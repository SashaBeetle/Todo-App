import { Component } from '@angular/core';
import { SharedServiceService } from '../../services/shared-service.service';

@Component({
  selector: 'app-add-list',
  standalone: true,
  imports: [],
  templateUrl: './add-list.component.html',
  styleUrl: './add-list.component.scss'
})
export class AddListComponent {
  constructor(private sharedService: SharedServiceService){}

  onClick() {
    this.sharedService.toggleIsVisibleCreateList();
  }
}

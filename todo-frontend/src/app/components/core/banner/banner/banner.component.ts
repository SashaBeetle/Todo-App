import { Component, Input } from '@angular/core';
import { PriorityConstants } from '../../../../constants/priorityConstants';

@Component({
  selector: 'app-banner',
  standalone: true,
  imports: [],
  templateUrl: './banner.component.html',
  styleUrl: './banner.component.scss'
})
export class BannerComponent {

  @Input() card: any;
  priority: any = PriorityConstants.priority 
}

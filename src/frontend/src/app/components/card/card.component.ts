import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-card',
  imports: [],
  templateUrl: './card.component.html',
  styleUrl: './card.component.scss'
})
export class CardComponent {
  @Input() title!: string;
  @Input() details!: string;
  @Input() createdAt!: Date;
  @Input() updatedAt!: Date;
  @Input() completed!: boolean;
}

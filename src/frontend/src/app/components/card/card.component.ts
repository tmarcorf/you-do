import { Component, Input } from '@angular/core';
import { ToDoService } from '../../services/ToDoService';
import { NotificationService } from '../../services/NotificationService';
import { Router } from '@angular/router';

@Component({
  selector: 'app-card',
  imports: [],
  templateUrl: './card.component.html',
  styleUrl: './card.component.scss'
})
export class CardComponent {
  @Input() id!: string
  @Input() title!: string;
  @Input() details!: string;
  @Input() createdAt!: Date;
  @Input() updatedAt!: Date;
  @Input() completed!: boolean;

  constructor(
    private toDoService: ToDoService,
    private notificationService: NotificationService,
    private router: Router) {

    }

  deleteCard(id: string) {
    console.log("id: " + id);
    var deleteResponse = this.toDoService.delete(id);

    deleteResponse.subscribe({
      next: (response: any) => {
        if (response.isSuccess) {
          this.notificationService.success("Task deleted successfully");

          this.router.navigate(['/home']);
        }
      },
      error: (response: any) => {
        this.notificationService.error(response.message);
      }
    });
  }
}

import { Component, effect, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotificationService, Notification } from '../../services/NotificationService';
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrl: './notification.component.scss',
  standalone: true,
  imports: [NgClass]
})
export class NotificationComponent {
  notifications = signal<Notification[]>([]);

  constructor(private notificationService: NotificationService) {
    effect(() => {
      this.notificationService.notification$.subscribe((notification) => {
        this.notifications.update((n) => [...n, notification]);

        setTimeout(() => {
          this.notifications.update((n) => n.slice(1));
        }, 3000);
      });
    });
  }
}
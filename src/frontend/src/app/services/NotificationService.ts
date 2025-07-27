import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

export type NotificationType = 'success' | 'error';

export interface Notification {
  message: string;
  type: NotificationType;
}

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  private _notification = new Subject<Notification>();
  notification$ = this._notification.asObservable();

  success(message: string) {
    this._notification.next({ message, type: 'success' });
  }

  error(message: string) {
    this._notification.next({ message, type: 'error' });
  }
}
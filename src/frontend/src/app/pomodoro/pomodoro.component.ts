import { Component } from '@angular/core';

@Component({
  selector: 'app-pomodoro',
  imports: [],
  templateUrl: './pomodoro.component.html',
  styleUrl: './pomodoro.component.scss'
})
export class PomodoroComponent {
  currentTimer = '25:00';

  startTimer() {
    throw new Error('Method not implemented.');
  }

  pauseTimer() {
    throw new Error('Method not implemented.');
  }

  resetTimer() {
    throw new Error('Method not implemented.');
  }
}

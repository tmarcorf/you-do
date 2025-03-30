import { Component, OnInit } from '@angular/core';
import { PomodoroComponent } from "./pomodoro/pomodoro.component";

@Component({
  selector: 'app-root',
  imports: [PomodoroComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'You-Do App';
}

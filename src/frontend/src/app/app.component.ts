import { Component, OnInit } from '@angular/core';
import { HomeComponent } from './components/home/home.component';

@Component({
  selector: 'app-root',
  imports: [HomeComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'You-Do App';

  submit(event: any){
    console.log(event)
  }

  logar(event: string) {
    console.log(event)
  }

}

import { Component, inject, OnInit } from '@angular/core';
import { ToDo } from '../../models/toDo/ToDo';
import { LoginService } from '../../services/LoginService'; 
import { ToDoService } from '../../services/ToDoService';
import { Router } from '@angular/router';
import { AppConstants } from '../../shared/AppConstants';
import { NavbarComponent } from '../navbar/navbar.component';

@Component({
  selector: 'app-home',
  imports: [NavbarComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {
  private loginService = inject(LoginService);
  private toDoService = inject(ToDoService);
  toDos: ToDo[] = [];

  constructor(private router: Router){

  }
  ngOnInit(): void {
    var token = localStorage.getItem(AppConstants.TOKEN_KEY);

    if (token == '') {
      this.router.navigate(['/login']);
    }
  }

}

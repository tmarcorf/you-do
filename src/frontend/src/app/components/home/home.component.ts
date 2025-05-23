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
export class HomeComponent {
  private loginService = inject(LoginService);
  private toDoService = inject(ToDoService);
  toDos: ToDo[] = [
    {
      id: 'string',
      title: 'string',
      details: 'string',
      createdAt: new Date,
      updatedAt: new Date,
      completed: false
    }
  ];

  constructor(private router: Router){
    
  }
  

}

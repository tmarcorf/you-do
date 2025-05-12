import { Component, inject, Input, Output } from '@angular/core';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';
import { ToDo } from '../../models/toDo/ToDo';
import { ApiResponse } from '../../models/response/ApiResponse';
import {MatCardModule} from '@angular/material/card';
import { LoginService } from '../../services/LoginService'; 
import { isPlatformBrowser } from '@angular/common';
import { ToDoService } from '../../services/ToDoService';
import { UserToken } from '../../models/login/UserToken';

@Component({
  selector: 'app-home',
  imports: [],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  private _snackBar = inject(MatSnackBar);
  private loginService = inject(LoginService);
  private toDoService = inject(ToDoService);
  toDos: ToDo[] = [];

  constructor(){
    // this.getToDos();
    this.testarLogin();
  }

  showSnackBar() {
    var config = new MatSnackBarConfig();

    config.duration = 1000;
    config.announcementMessage = "teste";

    var retorno = this._snackBar.open('teste', "Ok", config);
    console.log(retorno);
  }

  testarLogin() {
    var userToken = this.loginService.autenticar('xxxxxxxx@gmail.com', 'xxxxxxxxxx');
    
    userToken.subscribe({
      next: (response: any) => {
        const toDos = this.toDoService.getAllFromUser(0, 10, response.data.token).subscribe({
          next: (data: any) => {
            this.mapToDoResponse(data.data);
          }
        });
        console.log(toDos);
      }
    })
  }

  mapToDoResponse(response: any[]) {

    for(let i = 0; i < response.length; i++) {
      var toDo = new ToDo(
        response[i].id,
        response[i].title,
        response[i].details,
        new Date(response[i].createdAt),
        new Date(response[i].updatedAt),
        response[i].completed
      );

      this.toDos.push(toDo);
    }
  }

  getToDos() {
    var count;

    for (count = 0; count < 10; count++){
      this.toDos.push(
      {
        id: '1',
        title: 'Título 1',
        details: 'Details 1',
        createdAt: new Date(),
        updatedAt: new Date(),
        completed: false
      },
      {
        id: '2',
        title: 'Título 2',
        details: 'Details 2',
        createdAt: new Date(),
        updatedAt: new Date(),
        completed: false
      },
      {
        id: '3',
        title: 'Título 3',
        details: 'Details 3',
        createdAt: new Date(),
        updatedAt: new Date(),
        completed: false
      },
      {
        id: '4',
        title: 'Título 4',
        details: 'Details 4',
        createdAt: new Date(),
        updatedAt: new Date(),
        completed: false
      },
      {
        id: '5',
        title: 'Título 5',
        details: 'Details 5',
        createdAt: new Date(),
        updatedAt: new Date(),
        completed: false
      })
    }
  }
}

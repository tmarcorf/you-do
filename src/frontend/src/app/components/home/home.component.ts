import { Component, inject, Input, Output } from '@angular/core';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';
import { ToDo } from '../../models/toDo/ToDo';
import {MatCardModule} from '@angular/material/card';
import { LoginService } from '../../services/LoginService'; 
import { isPlatformBrowser } from '@angular/common';

@Component({
  selector: 'app-home',
  imports: [],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  private _snackBar = inject(MatSnackBar);
  private loginService = inject(LoginService);
  toDos: ToDo[] = [];

  constructor(){
    this.getToDos();
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
    this.loginService.autenticar('xxxxxxx@gmail.com', 'xxxxxxxx').subscribe({
      next: (userToken) => {
        console.log('Login sucesso:', userToken);
        localStorage.setItem('YOU_DO_AUTH_TOKEN', userToken.token);
      },
      error: (error) => {
        console.error('Erro ao logar:', error);
      }
    });
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

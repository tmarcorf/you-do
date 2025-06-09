import { Component, inject, OnInit } from '@angular/core';
import { ToDo } from '../../models/toDo/ToDo';
import { LoginService } from '../../services/LoginService'; 
import { ToDoService } from '../../services/ToDoService';
import { Router } from '@angular/router';
import { AppConstants } from '../../shared/AppConstants';
import { NavbarComponent } from '../navbar/navbar.component';
import { NotificationService } from '../../services/NotificationService';
import { StorageService } from '../../services/StorageService';
import { json } from 'stream/consumers';
import { CardComponent } from "../card/card.component";

@Component({
  selector: 'app-home',
  imports: [NavbarComponent, CardComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {
  private loginService = inject(LoginService);
  private toDoService = inject(ToDoService);
  userName: string = '';

  toDos: ToDo[] = [];

  constructor(
    private router: Router,
    private notificationService: NotificationService,
    private storageService: StorageService
  ){
    
  }
  ngOnInit(): void {
    var baseUrl = AppConstants.BASE_URL + '/home';

    this.configureUserInfo();

    const userNameStored = this.storageService.get(AppConstants.YD_USERNAME) ?? '';
    this.userName =  userNameStored != '' ? userNameStored?.toString() : '';
    this.getAllFromUser();
  }

  configureUserInfo() {
    const userInfo = this.loginService.getUserInfo();

    userInfo.subscribe({
        next: (response: any) => {
          this.storageService.set(AppConstants.YD_USERNAME, response.data.firstName);
        },
        error: (response: any) => {
          this.router.navigate(['/login']);
        }
    })
  }
  
  getAllFromUser() {
    var toDosResponse = this.toDoService.getAllFromUser(0, 30);

    toDosResponse.subscribe({
      next: (response: any) => {
        this.toDos = response.data;
      },
      error: (response: any) => {
        this.notificationService.error(response.message);
      }
    });
  }
}

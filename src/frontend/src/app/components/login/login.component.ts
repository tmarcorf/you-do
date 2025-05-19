import { Component, OnInit } from '@angular/core';
import { NotificationService } from '../../services/NotificationService';
import { FormsModule } from '@angular/forms';
import { LoginService } from '../../services/LoginService';
import { Router } from '@angular/router';
import { AppConstants } from '../../shared/AppConstants';
import { NavbarComponent } from '../navbar/navbar.component';

@Component({
  selector: 'app-login',
  imports: [FormsModule, NavbarComponent],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit {
  
  email = '';
  password = '';

  constructor(
    private notificationService: NotificationService,
    private loginService: LoginService
  ) { }

  ngOnInit(): void {
    
  }

  login() {
    if(this.isValidLoginData()) {
      var userToken = this.loginService.autenticar(this.email, this.password);

      userToken.subscribe({
        next: (response: any) => {
          this.notificationService.success("Login efetuado com sucesso!");
          localStorage.setItem(AppConstants.TOKEN_KEY, response.data.token);
        },
        error: (response: any) => {
          this.notificationService.error(response.error.message);
        }
      })
    }
  }

  isValidLoginData(): boolean {
    if(!this.isValidEmail(this.email)) {
      this.notificationService.error("Please, enter a valid email!");
      return false;
    }
    
    if(this.password == '') {
      this.notificationService.error("Please, enter a password!");
      return false;
    }

    return true;
  }

  isValidEmail(email: string): boolean {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
  }
}


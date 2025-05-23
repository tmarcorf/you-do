import { Component } from '@angular/core';
import { AppConstants } from '../../shared/AppConstants';
import { Router } from '@angular/router';
import { NgIf } from '@angular/common';
import { LoginService } from '../../services/LoginService';
import { StorageService } from '../../services/StorageService';

@Component({
  selector: 'app-navbar',
  imports: [],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {
  baseUrl = '';
  userName: string = '';

  constructor(
    public router: Router,
    private loginService: LoginService,
    private storageService: StorageService
  ) {
    this.baseUrl = AppConstants.BASE_URL + '/home';

    this.configureUserInfo();

    const userNameStored = this.storageService.get(AppConstants.YD_USERNAME) ?? '';
    this.userName =  userNameStored != '' ? userNameStored?.toString() : '';
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

  clearData() {
    debugger;
    this.storageService.clearAll();
  }
}

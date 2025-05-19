import { Component } from '@angular/core';
import { AppConstants } from '../../shared/AppConstants';
import { Router } from '@angular/router';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-navbar',
  imports: [],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {
  baseUrl = '';
  userName: string = '';

  constructor(public router: Router) {
    this.baseUrl = AppConstants.BASE_URL + '/home';
    const userNameStored = localStorage.getItem(AppConstants.YD_USERNAME) ?? '';
    this.userName =  userNameStored != '' ? userNameStored?.toString() : '';
  }
}

import { Component, OnInit } from '@angular/core';
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
export class NavbarComponent implements OnInit {
  userName = '';

  constructor(
    public router: Router,
    private loginService: LoginService,
    public storageService: StorageService
  ) {
    
  }
  ngOnInit(): void {
    this.userName = this.storageService.get(AppConstants.YD_USERNAME) ?? '';
  }

  clearData() {
    debugger;
    this.storageService.clearAll();
  }
}

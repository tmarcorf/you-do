import { Component, inject, Input, Output } from '@angular/core';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';
import { LoginService } from '../../services/LoginService';

@Component({
  selector: 'app-home',
  imports: [],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  private _snackBar = inject(MatSnackBar);

  constructor(){
    
  }

  showSnackBar() {
    var config = new MatSnackBarConfig();

    config.duration = 1000;
    config.announcementMessage = "annoucement";


    var retorno = this._snackBar.open('teste', "Ok", config);
    console.log(retorno);
  }
}

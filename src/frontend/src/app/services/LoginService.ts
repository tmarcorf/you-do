import { Observable } from "rxjs";
import { UserToken } from "../models/login/UserToken";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { ApiResponse } from "../models/response/ApiResponse";
import { AppConstants } from "../shared/AppConstants";
import { UserInfo } from "../models/login/UserInfo";
import { appConfig } from "../app.config";
import { NotificationService } from "./NotificationService";
import { StorageService } from "./StorageService";

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(
    private httpClient: HttpClient,
    private notificationService: NotificationService,
    private storageService: StorageService
  ) {

  }

  autenticar(email: string, senha: string): Observable<UserToken> {
    const loginData = { email, password: senha };

    return this.httpClient.post<UserToken>(`${AppConstants.BASE_URL}/auth/login`, loginData);
  }

  getUserInfo(): Observable<UserInfo> {
    const token = this.storageService.get(AppConstants.TOKEN_KEY);

    if (token == '') {
      this.notificationService.error("You are not logged in. Please, click the 'Login' button to enter the app.");
      return new Observable<UserInfo>();
    }

    const headers = new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        });

    return this.httpClient.get<UserInfo>(`${AppConstants.BASE_URL}/auth/user-info`, {headers});
  }
}
import { Observable } from "rxjs";
import { UserToken } from "../models/login/UserToken";
import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private BASE_URL = 'http://localhost:7069/api';

  constructor(private httpClient: HttpClient) {

  }

  autenticar(email: string, senha: string): Observable<UserToken> {
    const loginData = { email, password: senha };

    return this.httpClient.post<UserToken>(`${this.BASE_URL}/auth/login`, loginData);
  }
}
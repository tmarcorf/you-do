import { Observable } from "rxjs";
import { UserToken } from "../models/login/UserToken";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { ToDo } from "../models/toDo/ToDo";
import { StorageService } from "./StorageService";
import { AppConstants } from "../shared/AppConstants";

@Injectable({
  providedIn: 'root'
})
export class ToDoService {
    private BASE_URL = 'http://localhost:7069/api';

    constructor(
        private http: HttpClient,
        private storageService: StorageService){}

    getAllFromUser(skip: number, take: number) : any {
        const token = this.storageService.get(AppConstants.TOKEN_KEY);

        const headers = new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        });
        
        return this.http.get(AppConstants.BASE_URL + '/todo/' + skip + '/' + take, {headers});
    }
}
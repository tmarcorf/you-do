import { Observable } from "rxjs";
import { UserToken } from "../models/login/UserToken";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { ToDo } from "../models/toDo/ToDo";

@Injectable({
  providedIn: 'root'
})
export class ToDoService {
    private BASE_URL = 'http://localhost:7069/api';

    constructor(private http: HttpClient){}

    getAllFromUser(skip: number, take: number, token: string) : any {
        const headers = new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        });
        
        return this.http.get(this.BASE_URL + '/todo/' + skip + '/' + take, {headers});
    }
}
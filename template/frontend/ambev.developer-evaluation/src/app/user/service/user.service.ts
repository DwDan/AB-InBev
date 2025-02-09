import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';
import { environment } from '../../environment';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private apiUrl = `${environment.apiBaseUrl}/Users`;

  constructor(private http: HttpClient) {}

  createUser(user: User): Observable<User> {
    return this.http.post<User>(this.apiUrl, user);
  }
}

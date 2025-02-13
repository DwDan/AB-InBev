import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { User } from '../models/user.model';
import { ApiService } from '../../core/services/api.service';
import { PaginatedResponse } from '../../core/shared/models/paginated-response.model';

@Injectable({
  providedIn: 'root',
})
export class UserService {

  constructor(private apiService: ApiService) {}

  getUsers(): Observable<User[]> {
    return this.apiService.get<PaginatedResponse<User>>('users').pipe(
      map(response => response?.data || []) 
    );
  }

  addUser(user: User): Observable<User> {
    return this.apiService.post<User>('users', user);
  }

  updateUser(updatedUser: User): Observable<User> {
    return this.apiService.put<User>('users', updatedUser.id, updatedUser);
  }

  deleteUser(id: number): Observable<boolean> {
    return this.apiService.delete('users', id);
  }

  getUserById(id: number): Observable<User | undefined> {
    return this.apiService.getById<User>('users', id).pipe(
      map(response => response || []) 
    );
  }
}

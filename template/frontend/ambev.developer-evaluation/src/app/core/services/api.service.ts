import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from '../../environment';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private baseUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient) {}

  private createHeaders() {
    return new HttpHeaders({
      'Content-Type': 'application/json',
    });
  }

  get<T>(endpoint: string): Observable<T> {
    return this.http
      .get<{ data: T }>(`${this.baseUrl}/${endpoint}`, {
        headers: this.createHeaders(),
      })
      .pipe(
        map((response) => response.data),
        catchError(this.handleError)
      );
  }

  getById<T>(endpoint: string, id: number): Observable<T> {
    return this.http
      .get<{ data: T }>(`${this.baseUrl}/${endpoint}/${id}`, {
        headers: this.createHeaders(),
      })
      .pipe(
        map((response) => response.data),
        catchError(this.handleError)
      );
  }

  post<T>(endpoint: string, body: any): Observable<T> {
    return this.http
      .post<{ data: T }>(`${this.baseUrl}/${endpoint}`, body, {
        headers: this.createHeaders(),
      })
      .pipe(
        map((response) => response.data),
        catchError(this.handleError)
      );
  }

  put<T>(endpoint: string, id: number, body: any): Observable<T> {
    return this.http
      .put<{ data: T }>(`${this.baseUrl}/${endpoint}/${id}`, body, {
        headers: this.createHeaders(),
      })
      .pipe(
        map((response) => response.data),
        catchError(this.handleError)
      );
  }

  delete<T>(endpoint: string, id: number): Observable<T> {
    return this.http
      .delete<{ data: T }>(`${this.baseUrl}/${endpoint}/${id}`, {
        headers: this.createHeaders(),
      })
      .pipe(
        map((response) => response.data),
        catchError(this.handleError)
      );
  }

  handleError(error: any): Observable<never> {
    let errorMessage = 'Ocorreu um erro inesperado.';
  
    if (error.error) {
      if (Array.isArray(error.error)) {
        errorMessage = error.error
          .map((err: any) => `${err.propertyName}: ${err.errorMessage}`)
          .join(' | ');
      } else {
        errorMessage = error.error.message || JSON.stringify(error.error);
      }
    } else if (error.status) {
      errorMessage = `Código de erro: ${error.status}, Mensagem: ${error.statusText}`;
    }
  
    alert(errorMessage);
    return throwError(() => new Error(errorMessage));
  }
}

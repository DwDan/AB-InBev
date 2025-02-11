import { Component, inject } from '@angular/core';
import { Store } from '@ngrx/store';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { loginUser } from './store/actions/auth.actions';
import { Observable } from 'rxjs';
import { selectAuthLoading } from './store/auth.selectors';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatInputModule,
    MatButtonModule,
    MatDialogModule,
    FormsModule,
    MatProgressSpinnerModule,
  ],
  styleUrls: ['./login.component.scss'],
  templateUrl: './login.component.html',
})
export class LoginComponent {
  email = '';
  password = '';
  loading$: Observable<boolean>;

  private store = inject(Store);
  private router = inject(Router);

  constructor() {
    this.loading$ = this.store.select(selectAuthLoading);
    sessionStorage.removeItem('token');
  }

  login() {
    this.store.dispatch(
      loginUser({ email: this.email, password: this.password })
    );
  }

  createUser(): void {
    this.router.navigate(['users/add']);
  }
}

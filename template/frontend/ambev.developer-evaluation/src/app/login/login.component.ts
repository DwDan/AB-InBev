import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { CreateUserDialogComponent } from '../user/create-user-dialog/create-user-dialog.component';
import { loginUser } from './store/actions/auth.actions';
import { Observable } from 'rxjs';
import { selectAuthToken, selectAuthError, selectAuthLoading } from './store/auth.selectors';

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
  ],
  styleUrls: ['./login.component.scss'],
  templateUrl: './login.component.html',
})
export class LoginComponent {
  email = '';
  password = '';

  token$: Observable<string | null>;
  error$: Observable<string | null>;
  loading$: Observable<boolean>;

  constructor(private store: Store,
    private dialog: MatDialog) {
    this.token$ = this.store.select(selectAuthToken);
    this.error$ = this.store.select(selectAuthError);
    this.loading$ = this.store.select(selectAuthLoading);
  }

  login() {
    this.store.dispatch(loginUser({ email: this.email, password: this.password }));
  }

  openCreateUserDialog(): void {
    this.dialog.open(CreateUserDialogComponent);
  }
}

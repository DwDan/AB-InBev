import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, mergeMap, of, tap } from 'rxjs';
import { Router } from '@angular/router';
import { loginUser, loginSuccess, loginFailure } from '../actions/auth.actions';
import { AuthService } from '../../service/auth.service';

@Injectable()
export class AuthEffects {
  constructor(
    private actions$: Actions,
    private authService: AuthService,
    private router: Router
  ) {}

  login$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loginUser),
      mergeMap(({ email, password }) =>
        this.authService.login(email, password).pipe(
          map((response) => {
            sessionStorage.setItem('token', response.token);
            return loginSuccess({ token: response.token });
          }),
          catchError((error) => {
            alert(error.message);
            return of(loginFailure({ error: error.message }));
          })
        )
      )
    )
  );

  redirectAfterLogin$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(loginSuccess),
        tap(() => this.router.navigate(['/home']))
      ),
    { dispatch: false }
  );
}

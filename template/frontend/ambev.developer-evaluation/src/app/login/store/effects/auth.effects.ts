import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, mergeMap, of } from 'rxjs';
import { loginUser, loginSuccess, loginFailure } from '../actions/auth.actions';
import { AuthService } from '../../service/auth.service';

@Injectable()
export class AuthEffects {
  constructor(
    private actions$: Actions,
    private authService: AuthService,
  ) {}

  login$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loginUser),
      mergeMap(({ email, password }) =>
        this.authService.login(email, password).pipe(
          map((response) => {
            return loginSuccess({ token: response.token });
          }),
          catchError((error) => of(loginFailure({ error: error.message })))
        )
      )
    )
  );
}

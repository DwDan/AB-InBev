import { createSelector, createFeatureSelector } from '@ngrx/store';
import { AuthState } from './reducers/auth.reducer';

export const selectAuthState = createFeatureSelector<AuthState>('auth');

export const selectAuthToken = createSelector(
  selectAuthState,
  (state) => state.token
);

export const selectAuthError = createSelector(
  selectAuthState,
  (state) => state.error
);

export const selectAuthLoading = createSelector(
  selectAuthState,
  (state) => state.loading
);

import { createReducer, on } from '@ngrx/store';
import { loginFailure, loginSuccess, loginUser } from '../actions/auth.actions';

export interface AuthState {
  token: string | null;
  error: string | null;
  loading: boolean;
}

const initialState: AuthState = {
  token: null,
  error: null,
  loading: false
};

export const authReducer = createReducer(
  initialState,
  on(loginUser, (state) => ({
    ...state,
    loading: true,
    error: null
  })),
  on(loginSuccess, (state, { token }) => ({
    ...state,
    token,
    loading: false
  })),
  on(loginFailure, (state, { error }) => ({
    ...state,
    error,
    loading: false
  }))
);

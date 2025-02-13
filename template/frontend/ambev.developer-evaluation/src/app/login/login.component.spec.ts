import { ComponentFixture, TestBed } from '@angular/core/testing';
import { LoginComponent } from './login.component';
import { Store, StoreModule } from '@ngrx/store';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { of } from 'rxjs';
import { loginUser } from './store/actions/auth.actions';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let storeSpy: jasmine.SpyObj<Store>;
  let routerSpy: jasmine.SpyObj<Router>;

  beforeEach(async () => {
    storeSpy = jasmine.createSpyObj('Store', ['dispatch', 'select']);
    routerSpy = jasmine.createSpyObj('Router', ['navigate']);

    storeSpy.select.and.returnValue(of(false));

    await TestBed.configureTestingModule({
      imports: [
        LoginComponent,
        FormsModule,
        MatCardModule,
        MatInputModule,
        MatButtonModule,
        MatDialogModule,
        MatProgressSpinnerModule,
        StoreModule.forRoot({}),
        BrowserAnimationsModule
      ],
      providers: [
        { provide: Store, useValue: storeSpy },
        { provide: Router, useValue: routerSpy },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should dispatch loginUser action when login is called', () => {
    component.email = 'test@example.com';
    component.password = 'password123';

    component.login();

    expect(storeSpy.dispatch).toHaveBeenCalledWith(
      loginUser({ email: 'test@example.com', password: 'password123' })
    );
  });

  it('should navigate to user registration page when createUser is called', () => {
    component.createUser();
    expect(routerSpy.navigate).toHaveBeenCalledWith(['users/add']);
  });
});

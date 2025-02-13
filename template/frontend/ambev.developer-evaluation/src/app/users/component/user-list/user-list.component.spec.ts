import { ComponentFixture, TestBed } from '@angular/core/testing';
import { UserListComponent } from './user-list.component';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';
import { of } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { MaterialModule } from '../../../core/shared/modules/material.module';
import { User } from '../../models/user.model';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('UserListComponent', () => {
  let component: UserListComponent;
  let fixture: ComponentFixture<UserListComponent>;
  let userService: jasmine.SpyObj<UserService>;
  let routerSpy: jasmine.SpyObj<Router>;
  let dialogSpy: jasmine.SpyObj<MatDialog>;

  beforeEach(async () => {
    userService = jasmine.createSpyObj('UserService', [
      'getUsers',
      'deleteUser',
    ]);

    userService.getUsers.and.returnValue(of([
      <User>{
        id: 1,
        username: 'testuser',
        email: 'test@example.com',
        phone: '123456789',
        status: 1,
        role: 2,
        name: { firstname: 'John', lastname: 'Doe' },
        address: {
          city: 'Sample City',
          street: 'Sample Street',
          number: '10',
          zipcode: '12345',
          geolocation: { latitude: '10.123', longitude: '-20.123' },
        },
      },
      <User>{
        id: 2,
        username: 'testuser2',
        email: 'test2@example.com',
        phone: '987654321',
        status: 1,
        role: 2,
        name: { firstname: 'Jane', lastname: 'Smith' },
        address: {
          city: 'Another City',
          street: 'Another Street',
          number: '20',
          zipcode: '54321',
          geolocation: { latitude: '15.678', longitude: '-25.678' },
        },
      },
    ]));

    routerSpy = jasmine.createSpyObj('Router', ['navigate']);
    dialogSpy = jasmine.createSpyObj('MatDialog', ['open']);

    await TestBed.configureTestingModule({
      imports: [UserListComponent, HttpClientTestingModule, MaterialModule, BrowserAnimationsModule],
      providers: [
        { provide: UserService, useValue: userService },
        { provide: Router, useValue: routerSpy },
        { provide: MatDialog, useValue: dialogSpy },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(UserListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  }); 

  it('should navigate to add user page', () => {
    component.addUser();
    expect(routerSpy.navigate).toHaveBeenCalledWith(['users/add']);
  });

  it('should navigate to edit user page with correct ID', () => {
    const userId = 1;
    component.editUser(userId);
    expect(routerSpy.navigate).toHaveBeenCalledWith(['users/edit', userId]);
  });

  it('should call deleteUser and reload users when confirmed', () => {
    spyOn(window, 'confirm').and.returnValue(true);
    userService.deleteUser.and.returnValue(of(true));
    userService.getUsers.and.returnValue(of([]));

    component.deleteUser(1);

    expect(userService.deleteUser).toHaveBeenCalledWith(1);
    expect(userService.getUsers).toHaveBeenCalled();
  });

  it('should not delete user if confirmation is denied', () => {
    spyOn(window, 'confirm').and.returnValue(false);
    component.deleteUser(1);
    expect(userService.deleteUser).not.toHaveBeenCalled();
  });

  it('should navigate to home page when goToHome is called', () => {
    component.goToHome();
    expect(routerSpy.navigate).toHaveBeenCalledWith(['/home']);
  });
});

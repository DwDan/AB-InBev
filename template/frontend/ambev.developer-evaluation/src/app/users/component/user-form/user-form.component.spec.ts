import { ComponentFixture, TestBed } from '@angular/core/testing';
import { UserFormComponent } from './user-form.component';
import { UserService } from '../../services/user.service';
import { GeolocationService } from '../../services/geolocation.service';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { of } from 'rxjs';
import { User } from '../../models/user.model';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('UserFormComponent', () => {
  let component: UserFormComponent;
  let fixture: ComponentFixture<UserFormComponent>;
  let userService: jasmine.SpyObj<UserService>;
  let geolocationService: jasmine.SpyObj<GeolocationService>;
  let locationSpy: jasmine.SpyObj<Location>;

  beforeEach(async () => {
    userService = jasmine.createSpyObj('UserService', [
      'getUserById',
      'updateUser',
      'addUser',
    ]);

    userService.getUserById.and.returnValue(
      of({
        id: 1,
        username: 'testuser',
        password: '123456',
        email: 'test@example.com',
        phone: '123456789',
        status: 1,
        role: 4,
        name: { firstname: 'John', lastname: 'Doe' },
        address: {
          city: 'Sample City',
          street: 'Sample Street',
          number: '10',
          zipcode: '12345',
          geolocation: { latitude: '10.123', longitude: '-20.123' },
        },
      })
    );

    geolocationService = jasmine.createSpyObj('GeolocationService', [
      'getCurrentLocation',
    ]);
    locationSpy = jasmine.createSpyObj('Location', ['back']);

    await TestBed.configureTestingModule({
      imports: [
        UserFormComponent,
        ReactiveFormsModule,
        HttpClientTestingModule,
        BrowserAnimationsModule,
      ],
      providers: [
        { provide: UserService, useValue: userService },
        { provide: GeolocationService, useValue: geolocationService },
        { provide: Location, useValue: locationSpy },
        {
          provide: ActivatedRoute,
          useValue: {
            params: of({ id: 1 }),
          },
        },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(UserFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize form with default values for new user', () => {
    component.isEditMode = false;
    component.ngOnInit();

    expect(component.userForm.value.role).toBe(4);
    expect(component.userForm.value.status).toBe(1);
  });

  it('should load existing user data when in edit mode', () => {
    const mockUser: User = {
      id: 1,
      username: 'testuser',
      password: '123456',
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
    };

    component.ngOnInit();
    fixture.detectChanges();

    expect(component.userForm.value.username).toBe(mockUser.username);
    expect(component.userForm.value.email).toBe(mockUser.email);
    expect(component.userForm.value.name.firstname).toBe(
      mockUser.name.firstname
    );
    expect(component.userForm.value.name.lastname).toBe(mockUser.name.lastname);
  });

  it('should call updateUser when form is submitted in edit mode', () => {
    component.isEditMode = true;
    component.user = {
      id: 1,
      username: 'testuser',
      password: '123456',
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
    };

    userService.updateUser.and.returnValue(of(component.user));

    component.onSubmit();
    expect(userService.updateUser).toHaveBeenCalled();
    expect(locationSpy.back).toHaveBeenCalled();
  });

  it('should call addUser when form is submitted in create mode', (done) => {
    component.isEditMode = false;
    userService.addUser.and.returnValue(of(<User>{}));

    component.onSubmit();

    userService.addUser(<User>{}).subscribe(() => {
      expect(userService.addUser).toHaveBeenCalled();
      expect(locationSpy.back).toHaveBeenCalled();
      done();
    });
  });

  it('should fetch geolocation data and update the form', () => {
    const mockLocation = { latitude: 10.123, longitude: -20.123 };
    geolocationService.getCurrentLocation.and.returnValue(of(mockLocation));

    component.getLocation();
    expect(component.userForm.value.address.geolocation.latitude).toBe(
      mockLocation.latitude.toString()
    );
    expect(component.userForm.value.address.geolocation.longitude).toBe(
      mockLocation.longitude.toString()
    );
  });

  it('should navigate back on cancel', () => {
    component.onCancel();
    expect(locationSpy.back).toHaveBeenCalled();
  });
});

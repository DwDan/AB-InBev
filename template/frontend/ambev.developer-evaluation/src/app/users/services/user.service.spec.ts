import { TestBed } from '@angular/core/testing';
import { UserService } from './user.service';
import { ApiService } from '../../core/services/api.service';
import { of } from 'rxjs';
import { User } from '../models/user.model';

describe('UserService', () => {
  let service: UserService;
  let apiServiceSpy: jasmine.SpyObj<ApiService>;

  beforeEach(() => {
    const spy = jasmine.createSpyObj('ApiService', [
      'get',
      'post',
      'put',
      'delete',
      'getById',
    ]);

    TestBed.configureTestingModule({
      providers: [UserService, { provide: ApiService, useValue: spy }],
    });

    service = TestBed.inject(UserService);
    apiServiceSpy = TestBed.inject(ApiService) as jasmine.SpyObj<ApiService>;
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should retrieve a list of users', (done) => {
    const mockUsers: User[] = [
      <User>{
        id: 1,
        username: 'testUser',
        email: 'test@example.com',
        phone: '123456789',
        status: 1,
        role: 2,
        name: { firstname: 'John', lastname: 'Doe' },
        address: {
          city: 'NY',
          street: 'Main',
          number: '10',
          zipcode: '12345',
          geolocation: { latitude: '40.7128', longitude: '-74.0060' },
        },
      },
      <User>{
        id: 2,
        username: 'testUser2',
        email: 'test2@example.com',
        phone: '987654321',
        status: 1,
        role: 2,
        name: { firstname: 'Jane', lastname: 'Doe' },
        address: {
          city: 'LA',
          street: 'Broadway',
          number: '20',
          zipcode: '67890',
          geolocation: { latitude: '34.0522', longitude: '-118.2437' },
        },
      },
    ];

    apiServiceSpy.get.and.returnValue(of({ data: mockUsers }));

    service.getUsers().subscribe((users) => {
      expect(users.length).toBe(2);
      expect(users).toEqual(mockUsers);
      done();
    });

    expect(apiServiceSpy.get).toHaveBeenCalledOnceWith('users');
  });

  it('should add a user', (done) => {
    const newUser: User = <User>{
      id: 3,
      username: 'newUser',
      email: 'new@example.com',
      phone: '111222333',
      status: 1,
      role: 2,
      name: { firstname: 'New', lastname: 'User' },
      address: {
        city: 'SF',
        street: 'Market',
        number: '30',
        zipcode: '54321',
        geolocation: { latitude: '37.7749', longitude: '-122.4194' },
      },
    };

    apiServiceSpy.post.and.returnValue(of(newUser));

    service.addUser(newUser).subscribe((user) => {
      expect(user).toEqual(newUser);
      done();
    });

    expect(apiServiceSpy.post).toHaveBeenCalledOnceWith('users', newUser);
  });

  it('should update a user', (done) => {
    const updatedUser: User = <User>{
      id: 1,
      username: 'updatedUser',
      email: 'updated@example.com',
      phone: '000111222',
      status: 1,
      role: 2,
      name: { firstname: 'Updated', lastname: 'User' },
      address: {
        city: 'NY',
        street: 'Updated St',
        number: '50',
        zipcode: '99999',
        geolocation: { latitude: '40.7128', longitude: '-74.0060' },
      },
    };

    apiServiceSpy.put.and.returnValue(of(updatedUser));

    service.updateUser(updatedUser).subscribe((user) => {
      expect(user).toEqual(updatedUser);
      done();
    });

    expect(apiServiceSpy.put).toHaveBeenCalledOnceWith(
      'users',
      updatedUser.id,
      updatedUser
    );
  });

  it('should delete a user', (done) => {
    const userId = 1;
    apiServiceSpy.delete.and.returnValue(of(true));

    service.deleteUser(userId).subscribe((response) => {
      expect(response).toBeTrue();
      done();
    });

    expect(apiServiceSpy.delete).toHaveBeenCalledOnceWith('users', userId);
  });

  it('should retrieve a user by ID', (done) => {
    const mockUser: User = <User>{
      id: 1,
      username: 'testUser',
      email: 'test@example.com',
      phone: '123456789',
      status: 1,
      role: 2,
      name: { firstname: 'John', lastname: 'Doe' },
      address: {
        city: 'NY',
        street: 'Main',
        number: '10',
        zipcode: '12345',
        geolocation: { latitude: '40.7128', longitude: '-74.0060' },
      },
    };

    apiServiceSpy.getById.and.returnValue(of(mockUser));

    service.getUserById(mockUser.id).subscribe((user) => {
      expect(user).toEqual(mockUser);
      done();
    });

    expect(apiServiceSpy.getById).toHaveBeenCalledOnceWith(
      'users',
      mockUser.id
    );
  });
});

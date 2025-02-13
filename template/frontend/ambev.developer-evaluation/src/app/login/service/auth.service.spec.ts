import { TestBed } from '@angular/core/testing';
import { AuthService } from './auth.service';
import {
  HttpClientTestingModule,
  HttpTestingController,
} from '@angular/common/http/testing';
import { environment } from '../../environment';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('AuthService', () => {
  let service: AuthService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, BrowserAnimationsModule],
      providers: [AuthService],
    });

    service = TestBed.inject(AuthService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should send a POST request to login endpoint and return user data', () => {
    const mockResponse = { success: true, data: { token: 'mock-token' } };
    const email = 'test@example.com';
    const password = 'password123';

    service.login(email, password).subscribe((response) => {
      expect(response).toEqual(mockResponse.data);
    });

    const req = httpMock.expectOne(`${environment.apiBaseUrl}/Auth`);
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual({ email, password });

    req.flush(mockResponse);
  });

  it('should handle errors gracefully', () => {
    const email = 'test@example.com';
    const password = 'password123';

    service.login(email, password).subscribe({
      next: () => fail('should have failed with 401 error'),
      error: (error) => {
        expect(error.status).toBe(401);
      },
    });

    const req = httpMock.expectOne(`${environment.apiBaseUrl}/Auth`);
    req.flush(
      { message: 'Unauthorized' },
      { status: 401, statusText: 'Unauthorized' }
    );
  });
});

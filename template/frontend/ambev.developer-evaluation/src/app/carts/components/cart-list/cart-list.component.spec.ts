import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CartListComponent } from './cart-list.component';
import { CartService } from '../../services/cart.service';
import { Router } from '@angular/router';
import { of } from 'rxjs';
import { MaterialModule } from '../../../core/shared/modules/material.module';
import { Cart } from '../../models/cart.model';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('CartListComponent', () => {
  let component: CartListComponent;
  let fixture: ComponentFixture<CartListComponent>;
  let cartServiceMock: jasmine.SpyObj<CartService>;
  let routerMock: jasmine.SpyObj<Router>;

  beforeEach(async () => {
    cartServiceMock = jasmine.createSpyObj('CartService', ['getCarts']);
    routerMock = jasmine.createSpyObj('Router', ['navigate']);

    await TestBed.configureTestingModule({
      imports: [CartListComponent, MaterialModule, BrowserAnimationsModule],
      providers: [
        { provide: CartService, useValue: cartServiceMock },
        { provide: Router, useValue: routerMock },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(CartListComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load carts on init', () => {
    const mockCarts: Cart[] = [
      {
        id: 1,
        date: new Date(),
        userId: 1,
        price: 100,
        totalPrice: 90,
        products: [],
        isFinished: false,
        isCancelled: false,
      },
      {
        id: 2,
        date: new Date(),
        userId: 2,
        price: 200,
        totalPrice: 180,
        products: [],
        isFinished: true,
        isCancelled: false,
      },
    ];

    cartServiceMock.getCarts.and.returnValue(of(mockCarts));

    component.ngOnInit();

    expect(cartServiceMock.getCarts).toHaveBeenCalled();
    expect(component.carts).toEqual(mockCarts);
  });

  it('should navigate to home when goToHome is called', () => {
    component.goToHome();
    expect(routerMock.navigate).toHaveBeenCalledWith(['/home']);
  });

  it('should navigate to cart details when viewCart is called', () => {
    component.viewCart(1);
    expect(routerMock.navigate).toHaveBeenCalledWith(['carts', 1]);
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CartFormComponent } from './cart-form.component';
import { CartService } from '../../services/cart.service';
import { CartProductService } from '../../services/cart-product.service';
import { BranchService } from '../../../branchs/services/branch.service';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { of } from 'rxjs';
import { instanceCart } from '../../models/cart.model';
import { CartProduct } from '../../models/cart-product.model';
import { MatDialog } from '@angular/material/dialog';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('CartFormComponent', () => {
  let component: CartFormComponent;
  let fixture: ComponentFixture<CartFormComponent>;
  let cartServiceMock: jasmine.SpyObj<CartService>;
  let cartProductServiceMock: jasmine.SpyObj<CartProductService>;
  let branchServiceMock: jasmine.SpyObj<BranchService>;
  let locationMock: jasmine.SpyObj<Location>;
  let routeMock: any;

  beforeEach(async () => {
    cartServiceMock = jasmine.createSpyObj('CartService', [
      'getCartById',
      'updateCart',
      'deleteCart',
    ]);

    cartServiceMock.getCartById.and.returnValue(of(instanceCart()));
    cartServiceMock.updateCart.and.returnValue(of(instanceCart()));
    cartServiceMock.deleteCart.and.returnValue(of(true));  

    cartProductServiceMock = jasmine.createSpyObj('CartProductService', [
      'deleteCartProduct',
    ]);

    cartProductServiceMock.deleteCartProduct.and.returnValue(of(true));  

    branchServiceMock = jasmine.createSpyObj('BranchService', ['getBranches']);

    branchServiceMock.getBranches.and.returnValue(
      of([{ id: 1, name: 'Branch 1' }])
    );

    locationMock = jasmine.createSpyObj('Location', ['back']);

    routeMock = {
      params: of({ id: 1 }),
    };

    await TestBed.configureTestingModule({
      imports: [CartFormComponent, BrowserAnimationsModule],
      providers: [
        { provide: CartService, useValue: cartServiceMock },
        { provide: CartProductService, useValue: cartProductServiceMock },
        { provide: BranchService, useValue: branchServiceMock },
        { provide: ActivatedRoute, useValue: routeMock },
        { provide: Location, useValue: locationMock },
        { provide: MatDialog, useValue: {} },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(CartFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create component', () => {
    expect(component).toBeTruthy();
  });

  it('should retrieve a cart when id is provided in route', () => {
    expect(cartServiceMock.getCartById).toHaveBeenCalledWith(1);
  });

  it('should update cart when finalizing sale', () => {
    spyOn(window, 'confirm').and.returnValue(true);
    component.onSubmit();
    expect(cartServiceMock.updateCart).toHaveBeenCalled();
  });

  it('should cancel sale when onCancel is triggered', () => {
    spyOn(window, 'confirm').and.returnValue(true);
    component.onCancel();
    expect(cartServiceMock.updateCart).toHaveBeenCalled();
  });

  it('should call deleteCartProduct when deleting a product', () => {
    const product: CartProduct = {
      id: 1,
      productId: 2,
      quantity: 1,
      unityPrice: 10,
      discount: 0,
      totalPrice: 10,
      cartId: 1,
    };
    component.deleteCartProduct(product);
    expect(cartProductServiceMock.deleteCartProduct).toHaveBeenCalledWith(1);
  });

  it('should increase product quantity', () => {
    const product: CartProduct = {
      id: 1,
      productId: 2,
      quantity: 1,
      unityPrice: 10,
      discount: 0,
      totalPrice: 10,
      cartId: 1,
    };
    component.increaseQuantity(product);
    expect(product.quantity).toBe(2);
    expect(cartServiceMock.updateCart).toHaveBeenCalled();
  });

  it('should decrease product quantity and remove it when reaches 0', () => {
    const product: CartProduct = {
      id: 1,
      productId: 2,
      quantity: 1,
      unityPrice: 10,
      discount: 0,
      totalPrice: 10,
      cartId: 1,
    };
    spyOn(component, 'deleteCartProduct');
    component.decreaseQuantity(product);
    expect(product.quantity).toBe(0);
    expect(component.deleteCartProduct).toHaveBeenCalledWith(product);
  });

  it('should delete cart if confirmed', () => {
    spyOn(window, 'confirm').and.returnValue(true);
    component.deleteCart();
    expect(cartServiceMock.deleteCart).toHaveBeenCalled();
  });

  it('should navigate back when calling onReturn', () => {
    component.onReturn();
    expect(locationMock.back).toHaveBeenCalled();
  });
});

import {
  ComponentFixture,
  TestBed,
  fakeAsync,
  tick,
} from '@angular/core/testing';
import { ProductFormComponent } from './product-form.component';
import { ProductService } from '../../services/product.service';
import { ActivatedRoute, Router } from '@angular/router';
import {
  ReactiveFormsModule,
  FormsModule,
  Validators,
  FormBuilder,
} from '@angular/forms';
import { of } from 'rxjs';
import { Product } from '../../models/product.model';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('ProductFormComponent', () => {
  let component: ProductFormComponent;
  let fixture: ComponentFixture<ProductFormComponent>;
  let productServiceSpy: jasmine.SpyObj<ProductService>;
  let routerSpy: jasmine.SpyObj<Router>;
  let activatedRouteStub: Partial<ActivatedRoute>;

  beforeEach(async () => {
    productServiceSpy = jasmine.createSpyObj('ProductService', [
      'getProductById',
      'addProduct',
      'updateProduct',
    ]);
    routerSpy = jasmine.createSpyObj('Router', ['navigate']);

    activatedRouteStub = {
      params: of({ id: 1 }),
    };

    await TestBed.configureTestingModule({
      imports: [
        ProductFormComponent,
        ReactiveFormsModule,
        FormsModule,
        BrowserAnimationsModule,
      ],
      providers: [
        { provide: ProductService, useValue: productServiceSpy },
        { provide: Router, useValue: routerSpy },
        { provide: ActivatedRoute, useValue: activatedRouteStub },
        FormBuilder,
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(ProductFormComponent);
    component = fixture.componentInstance;

    component.productForm = new FormBuilder().group({
      title: ['', Validators.required],
      category: ['', Validators.required],
      price: [0, [Validators.required, Validators.min(1)]],
      description: ['', Validators.required],
      image: ['', Validators.required],
      rating: new FormBuilder().group({
        rate: [0, [Validators.min(0), Validators.max(5)]],
        count: [0, Validators.min(0)],
      }),
    });

    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize the form correctly', () => {
    fixture.detectChanges();
    expect(component.productForm).toBeDefined();
    expect(component.productForm.get('title')?.value).toBe('');
    expect(component.productForm.get('category')?.value).toBe('');
    expect(component.productForm.get('price')?.value).toBe(0);
    expect(component.productForm.get('description')?.value).toBe('');
    expect(component.productForm.get('image')?.value).toBe('');
    expect(component.productForm.get('rating.rate')?.value).toBe(0);
    expect(component.productForm.get('rating.count')?.value).toBe(0);
  });

  it('should call addProduct on submit when in create mode', fakeAsync(() => {
    component.isEditMode = false;
    productServiceSpy.addProduct.and.returnValue(of({} as Product));

    component.productForm.setValue({
      title: 'New Product',
      category: 'New Category',
      price: 200,
      description: 'New Description',
      image: 'new_image.jpg',
      rating: { rate: 5, count: 20 },
    });

    component.onSubmit();
    tick();

    expect(productServiceSpy.addProduct).toHaveBeenCalledOnceWith(
      component.productForm.value
    );
    expect(routerSpy.navigate).toHaveBeenCalledWith(['/products']);
  }));

  it('should call updateProduct on submit when in edit mode', fakeAsync(() => {
    component.isEditMode = true;
    component.product = { id: 1 } as Product;
    productServiceSpy.updateProduct.and.returnValue(of({} as Product));

    component.productForm.setValue({
      title: 'Updated Product',
      category: 'Updated Category',
      price: 300,
      description: 'Updated Description',
      image: 'updated_image.jpg',
      rating: { rate: 4.8, count: 25 },
    });

    component.onSubmit();
    tick();

    expect(productServiceSpy.updateProduct).toHaveBeenCalledOnceWith({
      id: 1,
      ...component.productForm.value,
    });

    expect(routerSpy.navigate).toHaveBeenCalledWith(['/products']);
  }));

  it('should navigate back on cancel', () => {
    component.onCancel();
    expect(routerSpy.navigate).toHaveBeenCalledWith(['/products']);
  });
});

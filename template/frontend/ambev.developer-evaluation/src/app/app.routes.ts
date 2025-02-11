import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { ProductListComponent } from './products/components/product-list/product-list.component';
import { ProductFormComponent } from './products/components/product-form/product-form.component';
import { UserListComponent } from './users/component/user-list/user-list.component';
import { UserFormComponent } from './users/component/user-form/user-form.component';
import { CartFormComponent } from './carts/components/cart-form/cart-form.component';
import { BranchListComponent } from './branchs/components/branch-list/branch-list.component';
import { BranchFormComponent } from './branchs/components/branch-form/branch-form.component';
import { TokenGuard } from './core/shared/guards/token.guard';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  {
    path: 'home',
    component: HomeComponent,
    canActivate: [TokenGuard],
  },
  {
    path: 'products',
    component: ProductListComponent,
    canActivate: [TokenGuard],
  },
  {
    path: 'products/edit/:id',
    component: ProductFormComponent,
    canActivate: [TokenGuard],
  },
  {
    path: 'products/add',
    component: ProductFormComponent,
    canActivate: [TokenGuard],
  },
  {
    path: 'users',
    component: UserListComponent,
    canActivate: [TokenGuard],
  },
  {
    path: 'users/edit/:id',
    component: UserFormComponent,
    canActivate: [TokenGuard],
  },
  {
    path: 'users/add',
    component: UserFormComponent,
  },
  {
    path: 'carts/:id',
    component: CartFormComponent,
    canActivate: [TokenGuard],
  },
  {
    path: 'branches',
    component: BranchListComponent,
    canActivate: [TokenGuard],
  },
  {
    path: 'branches/edit/:id',
    component: BranchFormComponent,
    canActivate: [TokenGuard],
  },
  {
    path: 'branches/add',
    component: BranchFormComponent,
    canActivate: [TokenGuard],
  },
];

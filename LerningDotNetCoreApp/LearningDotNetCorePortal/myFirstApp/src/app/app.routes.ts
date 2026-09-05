import { Routes } from '@angular/router';
import { OrdersComponent } from './orders/orders.component';
import { OrderCreateComponent } from './order-create/order-create.component';
import { LoginComponent } from './login/login.component';
import { authGuard } from './core/auth.guard';

export const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'orders', component: OrdersComponent, canActivate: [authGuard] },
    { path: 'orders/create', component: OrderCreateComponent, canActivate: [authGuard] },
    { path: '', redirectTo: '/login', pathMatch: 'full' },
    { path: 'orders/edit/:id', component: OrderCreateComponent, canActivate: [authGuard] },
];

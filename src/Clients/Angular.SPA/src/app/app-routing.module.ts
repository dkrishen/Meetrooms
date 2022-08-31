import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CalendarComponent } from './components/calendar/calendar.component';
import { HomeComponent } from './components/home/home.component';
import { OrdersComponent } from './components/orders/orders.component';
import { WrapperComponent } from './components/wrapper/wrapper.component';
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
  {
    path: '',
    component: WrapperComponent,
    children: [
      {path: '', component: HomeComponent},
      {path: 'orders', component: OrdersComponent, canActivate: [AuthGuard]},
      {path: 'calendar', component: CalendarComponent, canActivate: [AuthGuard]},
    ]
  },
  {path: '**', redirectTo: ''}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CalendarComponent } from './components/calendar/calendar.component';
import { HomeComponent } from './components/home/home.component';
import { BookingsComponent } from './components/bookings/bookings.component';
import { WrapperComponent } from './components/wrapper/wrapper.component';
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
  {
    path: '',
    component: WrapperComponent,
    children: [
      {path: '', component: HomeComponent},
      { path: 'bookings', component: BookingsComponent, canActivate: [AuthGuard]},
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

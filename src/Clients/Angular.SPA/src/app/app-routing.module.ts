import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CalendarComponent } from './components/calendar/calendar.component';
import { HomeComponent } from './components/home/home.component';
import { BookingsComponent } from './components/bookings/bookings.component';
import { WrapperComponent } from './components/wrapper/wrapper.component';
import { AuthGuard } from './guards/auth.guard';
import { NotFoundComponent } from './components/not-found/not-found.component';

const routes: Routes = [
  {
    path: '',
    component: WrapperComponent,
    children: [
      {path: '', component: HomeComponent},
      {path: 'PageNotFound', component: NotFoundComponent},
      { path: 'bookings', component: BookingsComponent, canActivate: [AuthGuard]},
      {path: 'calendar', component: CalendarComponent, canActivate: [AuthGuard]},
    ]
  },
  {path: '**', redirectTo: 'PageNotFound'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

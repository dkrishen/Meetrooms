import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations'
import { MatCardModule } from '@angular/material/card'
import { MatInputModule } from '@angular/material/input'
import { MatButtonModule } from '@angular/material/button'
import { MatTableModule } from '@angular/material/table'
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatDialogModule } from '@angular/material/dialog'
import { MatDatepickerModule} from '@angular/material/datepicker'
import {NgxMaterialTimepickerModule} from 'ngx-material-timepicker';

import { HomeComponent } from './components/home/home.component';
import { BookingsComponent } from './components/bookings/bookings.component';
import { AUTH_API_URL, BACK_API_URL, SIGNALR_HUB_URL } from './app-injection-tokens';
import { environment } from 'src/environments/environment';
import { ACCESS_TOKEN_KEY } from './services/auth.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { WrapperComponent } from './components/wrapper/wrapper.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { SideMenuComponent } from './components/side-menu/side-menu.component';
import { OAuthModule } from 'angular-oauth2-oidc';
import { JwtModule } from '@auth0/angular-jwt';
import { FullCalendarModule } from '@fullcalendar/angular';
import dayGridPlugin from '@fullcalendar/daygrid'; 
import interactionPlugin from '@fullcalendar/interaction'; 
import { CalendarComponent } from './components/calendar/calendar.component';
import { SelectDateDialogBoxComponent } from './components/select-date-dialog-box/select-date-dialog-box.component';
import { MatNativeDateModule } from '@angular/material/core';
import { NotFoundComponent } from './components/not-found/not-found.component';

export function tokenGetter(){
  return localStorage.getItem(ACCESS_TOKEN_KEY);
}

FullCalendarModule.registerPlugins([
  dayGridPlugin,
  interactionPlugin
]);

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    BookingsComponent,
    WrapperComponent,
    NavMenuComponent,
    SideMenuComponent,
    CalendarComponent,
    SelectDateDialogBoxComponent,
    NotFoundComponent
  ],
  imports: [
    FullCalendarModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    OAuthModule.forRoot(),
    ReactiveFormsModule,

    MatCardModule,
    MatInputModule,
    MatButtonModule,
    MatTableModule,
    MatFormFieldModule,
    MatSidenavModule,
    MatToolbarModule,
    MatDialogModule,
    MatDatepickerModule,
    MatNativeDateModule,
    NgxMaterialTimepickerModule,

    FormsModule,

    JwtModule.forRoot({
      config: {
        tokenGetter,
        allowedDomains: environment.tokenWhiteListedDomains
      }
    })
  ],
  providers: [
    {
      provide: BACK_API_URL,
      useValue: environment.backApi    
    },
    {
      provide: SIGNALR_HUB_URL,
      useValue: environment.signalrApi    
    },
    {
      provide: AUTH_API_URL,
      useValue: environment.authApi    
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }

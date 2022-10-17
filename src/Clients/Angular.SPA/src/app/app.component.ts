import { Component, HostListener, OnInit } from '@angular/core';
import { AuthService } from './services/auth.service';
import { SignalRService } from './services/signalr.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(
    private authService: AuthService, 
    private signalrService: SignalRService){
    this.authService.configureSingleSignOn();
  }

  @HostListener('window:beforeunload')
  async unsubscribeFromNotifications() {
    this.signalrService.stop();
  }
}

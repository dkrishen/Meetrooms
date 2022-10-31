import { Component, HostListener } from '@angular/core';
import { AuthService } from './services/auth.service';
import { SignalRService } from './services/signalr.service';
import * as Notiflix from 'notiflix';

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

  ngOnInit() {
    this.signalrService.trigger$.subscribe((message) => {
      this.showSuccess(''+message);
    });
  }
  
  showSuccess(message: string) {
    Notiflix.Notify.success(message);
  }
}

import { Component, HostListener, Inject } from '@angular/core';
import { AuthService } from './services/auth.service';
import { SignalRService } from './services/signalr.service';
import * as Notiflix from 'notiflix';
import { DOCUMENT } from '@angular/common';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(
    private authService: AuthService, 
    private signalrService: SignalRService,
    @Inject(DOCUMENT) private document: Document){
    this.authService.configureSingleSignOn();
  }

  @HostListener('window:beforeunload')
  async unsubscribeFromNotifications() {
    this.signalrService.stop();
  }

  ngOnInit() {
    this.signalrService.trigger$.subscribe((notificationJson) => {
      var notification = JSON.parse(''+notificationJson);

      console.log(notification)
      debugger;

      if(notification.Successfully){
        this.showSuccess('' + notification.Message);
      }
      else {
        this.showFailure('' + notification.Message);
      }

      setTimeout(this.resetPage, 3000)
    });
  }
  
  resetPage(){
    this.document.location.reload();
  }

  showSuccess(message: string) {
    Notiflix.Notify.success(message);
  }

  showFailure(message: string) {
    Notiflix.Notify.failure(message);
  }
}

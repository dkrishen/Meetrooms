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
  async unsubscribeFromNotificationHub() {
    this.signalrService.stop();
  }

  ngOnInit() {
    this.signalrService.notificationTrigger$.subscribe((notificationJson) => {
      var notification = JSON.parse(''+notificationJson);

      if(notification.Successfully == true){
        this.showSuccess('' + notification.Message);
        setTimeout(this.resetPage, 3000)
      }
      else if(notification.Successfully == false) {
        this.showFailure('' + notification.Message);
        setTimeout(this.resetPage, 3000)
      }
      else {
        this.showInfo('' + notification.Message);
      }
    });
    
    this.signalrService.updateTrigger$.subscribe(() => {
      this.resetPage();
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

  showInfo(message: string) {
    Notiflix.Notify.info(message);
  }
}

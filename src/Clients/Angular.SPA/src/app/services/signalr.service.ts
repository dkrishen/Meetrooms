import { Inject, Injectable } from '@angular/core';
import { SIGNALR_HUB_URL } from '../app-injection-tokens';
import { AuthService } from './auth.service';
import * as signalR from '@aspnet/signalr';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  private notificationHubConnection: signalR.HubConnection | undefined;

  constructor(
    private authService: AuthService,
    @Inject(SIGNALR_HUB_URL) private signalrUrl: string  ) { }

    private _notificationTrigger = new Subject<void>();
    private _updateTrigger = new Subject<void>();

    get notificationTrigger$() {
      return this._notificationTrigger.asObservable();
    }
    get updateTrigger$() {
      return this._updateTrigger.asObservable();
    }

  public start(){
    if(this.isConnected()){
      console.log("notification hub already connected");
      return;
    }
    if(!this.authService.isAuthenticate()){
      console.log("token is not load")
      return;
    }

    this.startConnection();
    this.addUpdateListener();
    this.addNotificationListener();

    console.log("the hub has just been connected")
  }

  public startConnection = () => {
    const token = this.authService.getAccessToken();
    
    this.notificationHubConnection = new signalR.HubConnectionBuilder()
    .withUrl(this.signalrUrl+'notification', {
      accessTokenFactory: () => token
    })
    .build();
    
    this.notificationHubConnection
    .start()
    .then(() => console.log('Connection started'))
    .catch(err => console.log('Error while starting connection: ' + err));
  }
  
  public addNotificationListener = () => {
    this.notificationHubConnection?.on('SendNotificationAsync', (notification) => {
      this._notificationTrigger.next(notification);
    });
  }
  
  public addUpdateListener = () => {
    this.notificationHubConnection?.on('UpdateCalendarAsync', () => {
      this._updateTrigger.next();
    });
  }

  public callNotification(message: string){

  }
  
  public stop() {
    this.notificationHubConnection?.stop();
    this.notificationHubConnection = undefined;
  }

  public isConnected(): boolean{
    if(this.notificationHubConnection != undefined){
      return true;
    }
    else {
      return false;
    }
  }
}

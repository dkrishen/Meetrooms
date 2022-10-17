import { Inject, Injectable } from '@angular/core';
import { BACK_API_URL } from '../app-injection-tokens';
import { AuthService } from './auth.service';
import * as signalR from '@aspnet/signalr';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  
  private notificationHubConnection: signalR.HubConnection | undefined;

  constructor(
    private authService: AuthService,
    @Inject(BACK_API_URL) private apiUrl: string,
  ) { }

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
      this.addNotificationListener();

      console.log("the hub has just been connected")
  }

  public startConnection = () => {
    console.log("startConnection");
    const token = this.authService.getAccessToken();
    
    this.notificationHubConnection = new signalR.HubConnectionBuilder()
    .withUrl('http://localhost:5400/notification', {
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
      console.log(notification);
    });
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

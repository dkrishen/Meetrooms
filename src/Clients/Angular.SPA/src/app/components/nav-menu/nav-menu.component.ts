import {Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { UserService } from 'src/app/services/user.service';


@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  constructor(
    private authService: AuthService, 
    private userService: UserService
  )
  {
    this.updateName();
  }
    
  username: string | null = '';

  login(){
    this.authService.login();
  }
  logout(){
    this.authService.logout();
  }

  updateName(){
    this.userService.getUserInfo()
    .subscribe(
      (data) => (this.username = data.name),
      (error) => {
        this.username = ''
        console.log(error)
      }
    )
  }

  get token(){
    return this.authService.isAuthenticate();
   }
}


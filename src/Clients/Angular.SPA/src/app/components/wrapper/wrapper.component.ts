import { Component } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'app-wrapper',
  templateUrl: './wrapper.component.html',
  styleUrls: ['./wrapper.component.css']
})
export class WrapperComponent {

  constructor(private oauthService: OAuthService){
  }

 get token(){
  let claims:any = this.oauthService.getIdentityClaims();
  return claims ? claims : null;
 }
}

import { Injectable } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { JwksValidationHandler } from 'angular-oauth2-oidc-jwks';
import { authConfig } from '../sso.config';

export const ACCESS_TOKEN_KEY = ''

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private oauthService: OAuthService,
  ) {  }
  
  updateToken(): void {
    var token = this.oauthService.getAccessToken();
    localStorage.setItem(ACCESS_TOKEN_KEY, token)
  }

  isAuthenticate() : boolean{
    let claims:any = this.oauthService.getIdentityClaims();
    return claims ? true : false;
  }

  login(){
    this.oauthService.initImplicitFlow();
    this.updateToken();
  }

  logout(){
    this.oauthService.logOut();
    this.updateToken();
  }

  configureSingleSignOn(){
    this.oauthService.configure(authConfig);
    this.oauthService.tokenValidationHandler = new JwksValidationHandler();
    this.oauthService.loadDiscoveryDocumentAndTryLogin();
  }
}

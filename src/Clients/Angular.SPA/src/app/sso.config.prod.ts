import { AuthConfig } from 'angular-oauth2-oidc';

export const authConfig: AuthConfig = {
  issuer: 'http://94.158.219.173:5000',
  redirectUri: window.location.origin + '/',
  clientId: 'MRAAngular',
  dummyClientSecret: 'AngularSecret',
  responseType: 'code',
  scope: 'openid profile email my_scope',
  requireHttps: false,

  showDebugInformation: true,
};

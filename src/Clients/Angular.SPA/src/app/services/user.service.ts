import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AUTH_API_URL } from '../app-injection-tokens';
import { UserInfo } from '../models/userInfo';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(
    private http: HttpClient,
    @Inject(AUTH_API_URL) private apiUrl: string,
  ) { }

  getUserInfo(): Observable<UserInfo> {
    return this.http.get<UserInfo>(this.apiUrl + "connect/userinfo")
  }
}

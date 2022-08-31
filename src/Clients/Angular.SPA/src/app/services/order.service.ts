import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Guid } from 'guid-typescript';
import { Observable } from 'rxjs';
import { BACK_API_URL } from '../app-injection-tokens';
import { InputOrderFormDto } from '../models/InputOrderFormDto';
import { Order } from '../models/order';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(
    private http: HttpClient,
    private authService: AuthService,
    @Inject(BACK_API_URL) private apiUrl: string,
  ) { }

  getAllOrders(): Observable<Order[]> {
    this.authService.updateToken();
    return this.http.get<Order[]>(this.apiUrl + "api/order/GetAllOrders")
  }
  
  getOrdersByUser(): Observable<Order[]> { 
    this.authService.updateToken();
    return this.http.get<Order[]>(this.apiUrl + "api/order/GetOrdersByUser")
  }

  postOrder(data: InputOrderFormDto): Observable<any> {
    this.authService.updateToken();
    const body = {date: data.date, startTime: data.startTime, endTime: data.endTime}
    return this.http.post<any>(this.apiUrl + "api/order/AddOrder", body);
  }

  updateOrder(data: Order): Observable<any> {
    this.authService.updateToken();
    const body = {
      date: data.date, 
      startTime: data.startTime, 
      endTime: data.endTime, 
      id: data.id, 
      meetingRoomId: data.meetingRoomId,
      userId: data.userId,
    }
    return this.http.put<any>(this.apiUrl + "api/order/UpdateOrder", body);
  }

  deleteOrder(id: Guid): Observable<any> {
    this.authService.updateToken();
    const body = { id: id }
    return this.http.delete<any>(this.apiUrl + "api/order/DeleteOrder", {body: body});
  }
}

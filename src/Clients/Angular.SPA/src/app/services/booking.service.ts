import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Guid } from 'guid-typescript';
import { Observable } from 'rxjs';
import { BACK_API_URL } from '../app-injection-tokens';
import { InputBookingFormDto } from '../models/InputBookingFormDto';
import { Booking } from '../models/booking';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class BookingService {

  constructor(
    private http: HttpClient,
    private authService: AuthService,
    @Inject(BACK_API_URL) private apiUrl: string,
  ) { }

  getAllBookings(): Observable<Booking[]> {
    this.authService.updateToken();
    return this.http.get<Booking[]>(this.apiUrl + "api/Booking/All")
  }
  
  getBookingsByUser(): Observable<Booking[]> { 
    this.authService.updateToken();
    return this.http.get<Booking[]>(this.apiUrl + "api/Booking/My")
  }

  postBooking(data: InputBookingFormDto): Observable<any> {
    this.authService.updateToken();
    const body = {date: data.date, startTime: data.startTime, endTime: data.endTime}
    return this.http.post<any>(this.apiUrl + "api/Booking", body, {observe: 'response'});
  }

  updateBooking(data: Booking): Observable<any> {
    this.authService.updateToken();
    const body = {
      date: data.date, 
      startTime: data.startTime, 
      endTime: data.endTime, 
      id: data.id, 
      meetingRoomId: data.meetingRoomId,
      userId: data.userId,
    }
    return this.http.put<any>(this.apiUrl + "api/Booking", body, { observe: 'response' });
  }

  deleteBooking(id: Guid): Observable<any> {
    this.authService.updateToken();
    const body = { id: id }
    return this.http.delete<any>(this.apiUrl + "api/Booking", { body: body, observe: 'response' });
  }
}

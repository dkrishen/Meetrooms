import { Component, OnInit } from '@angular/core';
import { Booking } from 'src/app/models/booking';
import { BookingService } from 'src/app/services/booking.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  bookings: Booking[] = []
  columns = ['meetingRoomName', 'username', 'date', 'startTime', 'endTime']

  constructor(private bookingService: BookingService) { }

  ngOnInit(): void {
    this.bookingService.getAllBookings()
      .subscribe(res => {
        this.bookings = res
      })
  }
}

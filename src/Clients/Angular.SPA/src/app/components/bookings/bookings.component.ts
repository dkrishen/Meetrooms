import { Component, OnInit } from '@angular/core';
import { Booking } from 'src/app/models/booking';
import { BookingService } from 'src/app/services/booking.service';

@Component({
  selector: 'app-bookings',
  templateUrl: './bookings.component.html',
  styleUrls: ['./bookings.component.css']
})
export class BookingsComponent implements OnInit {

  bookings: Booking[] = []
  columns = ['meetingRoomName', 'username', 'date', 'startTime', 'endTime']

  constructor(private bookingService: BookingService) { }

  ngOnInit(): void {
    this.bookingService.getBookingsByUser()
      .subscribe(result => {
        this.bookings = result
      })
  }
}

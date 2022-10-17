import { Component, Inject, OnInit } from '@angular/core';
import { CalendarOptions, EventClickArg, EventInput } from '@fullcalendar/angular';
import { DateClickArg } from '@fullcalendar/interaction';
import { Booking } from 'src/app/models/booking';
import { BookingService } from 'src/app/services/booking.service';
import { MatDialog } from '@angular/material/dialog'
import { SelectDateDialogBoxComponent } from '../select-date-dialog-box/select-date-dialog-box.component';
import { UserService } from 'src/app/services/user.service';
import { DOCUMENT } from '@angular/common';
import { SignalRService } from 'src/app/services/signalr.service';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent implements OnInit {
  bookings: Booking[] = []
  events: EventInput[] = []

  currentUsername: string | null = '';

  constructor(
    private bookingService: BookingService,
    private dialog: MatDialog,
    private userService: UserService,
    private signalrService: SignalRService,
    @Inject(DOCUMENT) private document: Document) { 
    this.createCalendar();
  }

  createCalendar(){
    this.bookingService.getAllBookings()
    .subscribe(result => {
      this.bookings = result
      this.setEvents();
      this.calendarOptions.events = this.events;  
    })
  }

  resetPage(){
    this.document.location.reload();
  }

  updateName(){
    this.userService.getUserInfo()
    .subscribe(
      (data) => (this.currentUsername = data.name),
      (error) => {
        this.currentUsername = ''
        console.log(error)
      }
    )
  }

  ngOnInit(): void {
    this.updateName();
    this.signalrService.start();
  }

  calendarOptions: CalendarOptions = {
    initialView: 'dayGridMonth',
    firstDay: 1,
    dateClick: this.handleDateClick.bind(this),
    eventClick: this.handleEventClick.bind(this), 
  };

  handleDateClick(arg: DateClickArg) {
    let dialogRef = this.dialog.open(SelectDateDialogBoxComponent,{
      width: '250px',
      data: {'title':'create booking'}
    });

    dialogRef.componentInstance.title = "Create booking"
    dialogRef.componentInstance.date = (arg.date.getMonth()+1)+'/'+arg.date.getDate()+'/'+arg.date.getFullYear()
    dialogRef.componentInstance.isOwner = true;
    dialogRef.componentInstance.createMode = true;
    dialogRef.componentInstance.bookings = this.bookings;

    dialogRef.afterClosed()
    .subscribe(data => {
      if(data != undefined){
        this.bookingService.postBooking(data).subscribe(() => {
          // this.resetPage()
        });
      }
    });
  }
  
  handleEventClick(arg: EventClickArg) {
    var startTime = arg.event._def.title.slice(0, 5)
    var endTime = arg.event._def.title.slice(6, 11)
    var username = arg.event._def.title.slice(12, arg.event._def.title.length)

    var booking = this.bookings.find(b => {
      if(b.username == username && b.startTime == startTime && b.endTime == endTime) return true
      else return false
    })

    if (booking != undefined){
      var date = new Date(booking.date)

      let dialogRef = this.dialog.open(SelectDateDialogBoxComponent,{
        width: '250px',
        data: { 'title': (booking?.username == this.currentUsername ? 'edit booking' : 'booking info')}
      });

      dialogRef.componentInstance.title = (booking?.username == this.currentUsername ? 'edit booking' : 'booking info')
      dialogRef.componentInstance.date = (date.getMonth()+1)+'/'+date.getDate()+'/'+date.getFullYear()
      dialogRef.componentInstance.startTime = startTime;
      dialogRef.componentInstance.endTime = endTime;
      dialogRef.componentInstance.isOwner = (booking?.username == this.currentUsername ? true : false)
      dialogRef.componentInstance.createMode = false;
      dialogRef.componentInstance.bookings = this.bookings;

      dialogRef.afterClosed()
      .subscribe(data => {
        if(data != undefined){

          if(data){
            if (booking != undefined){
              booking.startTime = data.startTime;
              booking.endTime = data.endTime;
              booking.date = data.date;

              this.bookingService.updateBooking(booking).subscribe(() => {
                this.resetPage()
              });
            
            }
          }
          else {
            if (booking != undefined){
              this.bookingService.deleteBooking(booking.id).subscribe(() => {
                this.resetPage();
              });
            }
          }
        }
      }); 
    }
  }

  setEvents(){
    this.bookings.forEach(booking => {
      var date = new Date(booking.date)

      var event = {
        title: booking.startTime + '-' + booking.endTime + ' ' + booking.username,
        date: date.getFullYear()+'-'
          + ((date.getMonth()+1).toString().length == 1 ? '0' + (date.getMonth()+1) : date.getMonth()+1) +'-'
          + ((date.getDate()).toString().length == 1 ? '0' + (date.getDate()) : date.getDate())
      }
      this.events.push(event);
    });
  }
}

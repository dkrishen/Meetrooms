import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog'
import { InputBookingFormDto } from 'src/app/models/InputBookingFormDto';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { Booking } from 'src/app/models/booking';
import { DOCUMENT } from '@angular/common';

@Component({
  selector: 'app-select-date-dialog-box',
  templateUrl: './select-date-dialog-box.component.html',
  styleUrls: ['./select-date-dialog-box.component.css']
})
export class SelectDateDialogBoxComponent implements OnInit {
  public title?: string;
  public date?: string;
  public startTime?: string;
  public endTime?: string;
  public isOwner?: boolean;
  public createMode?: boolean;
  public bookings?: Booking[];
  public previousStartTime?: string;
  public previousEndTime?: string;

  constructor(public dialogRef: MatDialogRef<SelectDateDialogBoxComponent>,
    @Inject(DOCUMENT) private document: Document) { }

  ngOnInit(): void {
    this.previousStartTime = this.startTime;
    this.previousEndTime = this.endTime;

    var st = this.document.getElementById("startTimeInput")
    st?.setAttribute("value", this.startTime ? this.startTime : "")
    
    var et = this.document.getElementById("endTimeInput")
    et?.setAttribute("value", this.endTime ? this.endTime : "")
   }

  onSubmit(){
    var data = new InputBookingFormDto();

    data.date = this.date;
    data.startTime = this.startTime;
    data.endTime = this.endTime;

    if(this.validateTime(data)){
      this.dialogRef.close(data);
    }
  }

  onRemuve(){
    this.dialogRef.close(false);
  }

  updateEventDate(event: MatDatepickerInputEvent<Date>) {
    if(event.value?.getMonth() != undefined) {
      this.date = (event.value?.getMonth()+1) + '/' + (event.value?.getDate()) + '/' + event.value?.getFullYear()
    }
  }
  
  setStartTime(eventValue: string){
    this.startTime = eventValue;
  }
  
  setEndTime(eventValue: string){
    this.endTime = eventValue;
  } 
  
  validateTime(data: InputBookingFormDto){
    if(data.startTime == undefined || data.endTime == undefined || data.date == undefined){
      alert("fields must be filled")
      return false
    }
    if(data.startTime >= data.endTime){
      alert("start time must be more than end time")
      return false;
    }

    var bookingDate = new Date(data.date);
    var dayBookings = this.bookings?.filter(booking => {
      var date = new Date(booking.date)
      if (date.toString() ==bookingDate.toString()){
        return true
      }
      else {
        return false;
      }
    });

    var collision = dayBookings?.filter(booking => {
        if(data.startTime != undefined && data.endTime != undefined){
          if (((data.startTime >= booking.startTime && data.startTime < booking.endTime) ||
              (data.endTime <= booking.endTime && data.endTime > booking.startTime) ||
              (booking.startTime > data.startTime && booking.endTime < data.endTime)) &&
              !(booking.startTime == this.previousStartTime && booking.endTime==this.previousEndTime)){
            return true;
          }
          else return false;
        }
        else return false;
      })

      if(collision?.length != 0){
        alert("time collision found")
        console.log(collision)
        return false
      }
      
    return true;
  }
}

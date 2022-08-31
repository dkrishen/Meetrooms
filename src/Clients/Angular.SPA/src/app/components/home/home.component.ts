import { Component, OnInit } from '@angular/core';
import { Order } from 'src/app/models/order';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  orders: Order[] = []
  columns = ['meetingRoomName', 'username', 'date', 'startTime', 'endTime']

  constructor(private orderService: OrderService) { }

  ngOnInit(): void {
    this.orderService.getAllOrders()
      .subscribe(res => {
        this.orders = res
      })
  }
}

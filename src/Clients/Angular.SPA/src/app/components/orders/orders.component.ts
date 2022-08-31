import { Component, OnInit } from '@angular/core';
import { Order } from 'src/app/models/order';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {

  orders: Order[] = []
  columns = ['meetingRoomName', 'username', 'date', 'startTime', 'endTime']

  constructor(private orderService: OrderService) { }

  ngOnInit(): void {
    this.orderService.getOrdersByUser()
      .subscribe(result => {
        this.orders = result
      })
  }
}

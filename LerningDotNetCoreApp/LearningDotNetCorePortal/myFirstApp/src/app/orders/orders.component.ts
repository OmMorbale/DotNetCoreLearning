import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Order, OrdersService } from './orders.service';
import { TableModule } from 'primeng/table';
import { Button } from "primeng/button";
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [CommonModule, TableModule, Button,RouterLink],
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent {
  orders:Order[]=[];

  constructor(private orderService:OrdersService){}

  ngOnInit():void{
    this.orderService.getAll().subscribe(data =>{
      this.orders=data;
    });
  }

  onDelete(id: number): void {
  this.orderService.delete(id).subscribe(() => {
    this.orders = this.orders.filter(o => o.id !== id);   // remove from local array too
  });
}
}

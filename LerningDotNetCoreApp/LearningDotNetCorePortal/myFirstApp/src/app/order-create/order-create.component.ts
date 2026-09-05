import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { OrdersService } from '../orders/orders.service';
import { CommonModule } from '@angular/common';
import { ButtonModule } from "primeng/button";
import { InputTextModule } from 'primeng/inputtext';

@Component({
  selector: 'app-order-create',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, ButtonModule, InputTextModule],
  templateUrl: './order-create.component.html',
  styleUrl: './order-create.component.css'
})
export class OrderCreateComponent {
    orderForm: FormGroup;
  orderId: number | null = null;   // 👈 null = create mode, number = edit mode
  isEditMode = false;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private ordersService: OrdersService
  ) {
    this.orderForm = this.fb.group({
      customerName: ['', Validators.required],
      amount: [0, [Validators.required, Validators.min(1)]]
    });
  }

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');

    if (idParam) {
      // Edit mode — id exists in the URL
      this.orderId = Number(idParam);
      this.isEditMode = true;

      this.ordersService.getById(this.orderId).subscribe(order => {
        this.orderForm.patchValue(order);
      });
    }
    // else: stays in create mode, form stays empty, orderId stays null
  }

  onSubmit(): void {
    if (this.orderForm.invalid) return;

    if (this.isEditMode && this.orderId !== null) {
      this.ordersService.update(this.orderId, { id: this.orderId, ...this.orderForm.value })
        .subscribe(() => this.router.navigate(['/orders']));
    } else {
      this.ordersService.create(this.orderForm.value)
        .subscribe(() => this.router.navigate(['/orders']));
    }
  }
}

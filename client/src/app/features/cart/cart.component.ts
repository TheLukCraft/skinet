import { CartService } from './../../core/services/cart.service';
import { Component, inject } from '@angular/core';
import { CartItemComponent } from "./cart-item/cart-item.component";

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CartItemComponent],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.scss'
})
export class CartComponent {
  CartService = inject(CartService);
}

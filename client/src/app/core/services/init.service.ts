import { of } from 'rxjs';
import { CartService } from './cart.service';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class InitService {
  private CartService = inject(CartService);

  init() {
    const cartId = localStorage.getItem('cart_id');
    const cart$ = cartId? this.CartService.getCart(cartId) : of(null);

    return cart$;
  }
}

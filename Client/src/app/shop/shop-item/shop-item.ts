import { Component, Input } from '@angular/core';
import { IProduct } from '../../shared/Models/Product';

@Component({
  selector: 'app-shop-item',
  standalone: false,
  styleUrl: './shop-item.scss',
  templateUrl: './shop-item.html',
})
export class ShopItem {
  @Input() product!: IProduct;
}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Shop } from './shop/shop';
import { ShopItem } from './shop-item/shop-item';

@NgModule({
  imports: [CommonModule],
  declarations: [Shop, ShopItem],
  exports: [Shop],
})
export class ShopModule {}

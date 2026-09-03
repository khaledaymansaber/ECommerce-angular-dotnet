import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Shop } from './shop/shop';
import { ShopItem } from './shop-item/shop-item';
import { SharedModule } from '../shared/shared-module';

@NgModule({
  imports: [CommonModule ,SharedModule],
  declarations: [Shop, ShopItem],
  exports: [Shop],
})
export class ShopModule {}

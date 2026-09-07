import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { ShopService } from '../shop';
import { IProduct } from '../../shared/Models/Product';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product-details',
  standalone: false,
  styleUrl: './product-details.scss',
  templateUrl: './product-details.html',
})
export class ProductDetails implements OnInit {

  product!: IProduct;

  constructor(
  private shopService: ShopService,
  private route: ActivatedRoute,
  private cdr: ChangeDetectorRef
) {}

  ngOnInit(): void {

    this.loadProduct();
  }
MainImage!:string;

loadProduct() {
  const id = this.route.snapshot.paramMap.get("id")!;

  this.shopService.getProductDetails(parseInt(id)).subscribe({
    next: (value: IProduct) => {
      this.product = value;
      this.MainImage=this.product.photos?.[0]?.imageName
      this.cdr.detectChanges();
    },
    error: (err) => {
      console.log(err);
    }
  });
}
ReplaceImage(src:string)
{
this.MainImage=src
}

getImageUrl(imageName: string | undefined): string {
  if (!imageName) return '';
  if (imageName.startsWith('http')) {
    return imageName;
  }
  return `https://localhost:7270/${imageName}`;
}

}


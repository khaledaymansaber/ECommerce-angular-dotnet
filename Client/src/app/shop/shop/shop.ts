import { Component, OnInit, signal } from '@angular/core'; // قمنا بإضافة signal هنا
import { ShopService } from '../shop';
import { IProduct } from '../../shared/Models/Product';
import { IPagnation } from '../../shared/Models/Pagnation';
import { ICategory } from '../../shared/Models/Category';

@Component({
  selector: 'app-shop',
  standalone: false,
  styleUrl: './shop.scss',
  templateUrl: './shop.html',
})
export class Shop implements OnInit {

  products = signal<IProduct[]>([]);
  Categories = signal<ICategory[]>([]);
  selectedCategoryId: number = 0;
  SortSelected: string = 'Name';

  constructor(private shopService: ShopService) {}

  ngOnInit(): void {

    this.getProducts();
    this.getCategory();

  }

  getProducts(category?:number) {
    this.shopService.getProduct(category,this.SortSelected).subscribe({
      next: (value:IPagnation) => {

        this.products.set(value.data);
      },

  error: (err) => {
    console.error("Error loading products:", err);
  }
    });
  }

  getCategory() {
    this.shopService.getCategory().subscribe({
      next: (value:ICategory[]) => {
        this.Categories.set(value);
      }
    });
  }
  selectedById(categoryId:number){
  this.selectedCategoryId=categoryId;
  const categoryParam = categoryId === 0 ? undefined : categoryId;
  this.getProducts(categoryParam);
  }
SortingOption = [
    { name: "Name", value: 'Name' },
    { name: "Price: min-max", value: 'PriceAsn' },
    { name: "Price: max-min", value: 'PriceDes' }
  ]

SortingByPrice(sort: Event) {
    this.SortSelected = (sort.target as HTMLInputElement).value;
    const categoryParam = this.selectedCategoryId === 0 ? undefined : this.selectedCategoryId;
    this.getProducts(categoryParam);
  }
}

import { HttpClient } from '@angular/common/http';
import { Component, OnInit, signal } from '@angular/core';
import { IProduct } from './shared/Models/Product';
import { IPagnation } from './shared/Models/Pagnation';

@Component({
  selector: 'app-root',
  standalone: false,
  styleUrl: './app.scss',
  templateUrl: './app.html',
})
export class App implements OnInit {
  baseURL = 'https://localhost:7270/api/Products/get-all';


  Product = signal<IProduct[]>([]);
  protected readonly title = signal('Client');

  constructor(private http: HttpClient) {}

  getProduct() {
    this.http.get<IPagnation>(this.baseURL).subscribe({
      next: (value) => {
        this.Product.set(value.data);
        console.log("Data from API:", value.data);
      },
      error: (err) => {
        console.error("Error from API:", err);
      }
    });
  }

  ngOnInit(): void {
    this.getProduct();
  }
}

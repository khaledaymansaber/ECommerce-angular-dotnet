import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPagnation } from '../shared/Models/Pagnation';
import { ICategory } from '../shared/Models/Category';


@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseURL = 'https://localhost:7270/api/';

  constructor(private http:HttpClient) { }
  getProduct(categoryId?:number ,SortSelected?:string) {
    let params = new HttpParams();
    if (categoryId) {
      params = params.append('categoryId', categoryId.toString());
    }
    if (SortSelected) {
      params = params.append('Sort', SortSelected.toString());
    }
    return this.http.get<IPagnation>(this.baseURL + "Products/get-all", { params: params });
  }
  getCategory() {
    return  this.http.get<ICategory[]>(this.baseURL +"Categories/get-all");
  }

}


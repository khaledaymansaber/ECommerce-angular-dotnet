import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPagnation } from '../shared/Models/Pagnation';
import { ICategory } from '../shared/Models/Category';
import { ProductParams } from '../shared/Models/ProductParams';
import { IProduct } from '../shared/Models/Product';


@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseURL = 'https://localhost:7270/api/';

  constructor(private http:HttpClient) { }
getProduct(ProductParams:ProductParams) {
    let params = new HttpParams();
    if (ProductParams.selectedCategoryId) {
      params = params.append('categoryId', ProductParams.selectedCategoryId.toString());
    }
    if (ProductParams.SortSelected) {
      params = params.append('Sort', ProductParams.SortSelected.toString());
    }
    if (ProductParams.Search) {

      params = params.append('Search', ProductParams.Search.toString());
    }
      params = params.append('pageNumber', ProductParams.pageNumber.toString());
      params = params.append('pageSize', ProductParams.pageSize.toString());

    return this.http.get<IPagnation>(this.baseURL + "Products/get-all", { params: params });
  }
  getCategory() {
    return  this.http.get<ICategory[]>(this.baseURL +"Categories/get-all");
  }
  getProductDetails(Id: number) {
    return  this.http.get<IProduct>(this.baseURL +"Products/get-by-id/"+Id);
  }

}


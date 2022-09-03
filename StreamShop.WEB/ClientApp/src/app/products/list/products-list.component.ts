import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {ProductImage} from "../product-images/ProductImage";
import Category from "../../categories/category";

@Component({
  selector: 'app-products-list',
  templateUrl: './products-list.component.html'
})
export class ProductsListComponent {
  public list: Product[] = [];
  public serverUrl = "https://localhost:7151/images/products/";

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Product[]>(baseUrl + 'api/product').subscribe(result => {
      this.list = result;
      console.log(this.list)
    }, error => console.error(error));
  }
}

interface Product {
  id: number;
  name: string;
  description: string;
  originalPrice: number;
  promotionPrice: number;
  categoryId: number;
  category: Category;
  images: ProductImage[];
}


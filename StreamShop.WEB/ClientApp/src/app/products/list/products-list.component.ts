import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-products-list',
  templateUrl: './products-list.component.html'
})
export class ProductsListComponent {
  public list: Product[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Product[]>(baseUrl + 'api/products').subscribe(result => {
      console.log(result);
    }, error => console.error(error));
  }
}

interface Product {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

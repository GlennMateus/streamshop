import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html'
})
export class ProductComponent {


  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

  }
}

interface Product {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

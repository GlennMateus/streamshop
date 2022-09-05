import {Component, Inject, Injectable} from '@angular/core';
import {HttpClient, HttpStatusCode} from '@angular/common/http';
import {ProductImage} from "../product-images/ProductImage";
import Category from "../../categories/category";
import {getBaseUrl} from "../../../main";

@Component({
  selector: 'app-products-list',
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.css']
})
@Injectable()
export class ProductsListComponent {
  private baseUrl: string = getBaseUrl();
  public list: Product[] = [];
  public serverUrl = `${this.baseUrl}images/products/`;

  public setItemBgColor(index: number) : string {
    return index % 2 == 0 ? "bg-gray" : "bg-white";
  }
  public setactionBgColor(index: number) : string {
    return index % 2 == 0 ? "bg-white" : "bg-gray";
  }

  public deleteProduct(itemId:number) {
    this.http.delete(`${this.baseUrl}api/product/${itemId}`).subscribe( async result => {
      await this.init();
    }, error => console.error(error));
  }
  constructor(private http: HttpClient) {
    this.init();
  }
  init() {
    this.http.get<Product[]>(this.baseUrl + 'api/product').subscribe(result => {
      this.list = result;
      console.log('estive aqui carai')
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


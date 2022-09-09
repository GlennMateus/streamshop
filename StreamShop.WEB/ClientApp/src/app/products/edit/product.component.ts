import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {ActivatedRoute} from "@angular/router";
import {DomSanitizer} from "@angular/platform-browser";

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent {
  public product : Product | undefined;
  public productImages : {url:string}[] = [];
  public imgCounter : number = 0;

  constructor(private http: HttpClient, private sanitizer : DomSanitizer, route: ActivatedRoute) {
    var productId = route.snapshot.paramMap.get('id');
    if (productId) {
      this.http.get<Product>('api/products/' + productId).subscribe(result => {
        this.product = result;
      }, error => console.error(error));
    }
  }

  public addImage(event: any) {
    let [file] = event.target.files;

    if (file) {
      if (this.productImages[this.imgCounter] == undefined) {
        this.productImages.push({url: URL.createObjectURL(file)});
        event.target.classList.remove('d-none');
      }
      else {
        this.productImages[this.imgCounter].url = URL.createObjectURL(file);
      }
    }
  }

  public sanitizeURL(productIndex : number) {
    let imgURL = this.productImages[productIndex].url
    return this.sanitizer.bypassSecurityTrustUrl(imgURL);
  }

  public removeImage() {
    this.productImages.splice(this.imgCounter, 1);
    this.imgCounter--;
  }
}

interface Product {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

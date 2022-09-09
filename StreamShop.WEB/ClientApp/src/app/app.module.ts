import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import {ProductsListComponent} from "./products/list/products-list.component";
import {ProductComponent} from "./products/edit/product.component";


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    ProductsListComponent,
    ProductComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    SweetAlert2Module.forRoot(),
    RouterModule.forRoot([
      { path: '', component: ProductsListComponent, pathMatch: 'full' },
      { path: 'product', component: ProductComponent, pathMatch: 'full' },
      { path: 'product/:id', component: ProductComponent, pathMatch: 'full' }

    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

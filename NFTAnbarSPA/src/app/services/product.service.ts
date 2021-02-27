import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GridData } from '../models/GridData';
import { Product } from '../models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  apiUrl = 'http://localhost:5000/api/product';

  constructor(private http: HttpClient) {}

  get(options: GridData<Product>) {
    return this.http.get<GridData<Product>>(`${this.apiUrl}/?queryParams=${JSON.stringify(options)}`);
  }

  getById(id: number) {
    return this.http.get<Product>(`${this.apiUrl}/${id}`);
  }

  create(depoType: Product) {
    return this.http.post(`${this.apiUrl}`, depoType);
  }

  update(depoType: Product) {
    return this.http.put(`${this.apiUrl}/${depoType.id}`, depoType);
  }

  delete(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}

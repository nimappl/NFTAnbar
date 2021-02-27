import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Customer } from '../models/customer';
import { GridData } from '../models/GridData';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  apiUrl = 'http://localhost:5000/api/customer';

  constructor(private http: HttpClient) {}

  get(options: GridData<Customer>) {
    return this.http.get<GridData<Customer>>(`${this.apiUrl}/?queryParams=${JSON.stringify(options)}`);
  }

  getById(id: number) {
    return this.http.get<Customer>(`${this.apiUrl}/${id}`);
  }

  create(depoType: Customer) {
    return this.http.post(`${this.apiUrl}`, depoType);
  }

  update(depoType: Customer) {
    return this.http.put(`${this.apiUrl}/${depoType.id}`, depoType);
  }

  delete(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}

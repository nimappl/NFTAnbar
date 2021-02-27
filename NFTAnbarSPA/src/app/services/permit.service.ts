import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GridData } from '../models/GridData';
import { Permit } from '../models/permit';

@Injectable({
  providedIn: 'root'
})
export class PermitService {
  apiUrl = 'http://localhost:5000/api/permit';

  constructor(private http: HttpClient) {}

  get(options: GridData<Permit>) {
    return this.http.get<GridData<Permit>>(`${this.apiUrl}/?queryParams=${JSON.stringify(options)}`);
  }

  getById(id: number) {
    return this.http.get<Permit>(`${this.apiUrl}/${id}`);
  }

  create(depoType: Permit) {
    return this.http.post(`${this.apiUrl}`, depoType);
  }

  update(depoType: Permit) {
    return this.http.put(`${this.apiUrl}/${depoType.id}`, depoType);
  }

  delete(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}

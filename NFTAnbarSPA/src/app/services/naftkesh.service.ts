import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GridData } from '../models/GridData';
import { Naftkesh } from '../models/naftkesh';

@Injectable({
  providedIn: 'root'
})
export class NaftkeshService {
  apiUrl = 'http://localhost:5000/api/naftkesh';

  constructor(private http: HttpClient) {}

  get(options: GridData<Naftkesh>) {
    return this.http.get<GridData<Naftkesh>>(`${this.apiUrl}/?queryParams=${JSON.stringify(options)}`);
  }

  getById(id: number) {
    return this.http.get<Naftkesh>(`${this.apiUrl}/${id}`);
  }

  create(depoType: Naftkesh) {
    return this.http.post(`${this.apiUrl}`, depoType);
  }

  update(depoType: Naftkesh) {
    return this.http.put(`${this.apiUrl}/${depoType.id}`, depoType);
  }

  delete(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}

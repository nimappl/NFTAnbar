import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GridData } from '../models/GridData';
import { Havaleh } from '../models/havaleh';

@Injectable({
  providedIn: 'root'
})
export class HavalehService {
  apiUrl = 'http://localhost:5000/api/havaleh';

  constructor(private http: HttpClient) {}

  get(options: GridData<Havaleh>) {
    return this.http.get<GridData<Havaleh>>(`${this.apiUrl}/?queryParams=${JSON.stringify(options)}`);
  }

  getById(id: number) {
    return this.http.get<Havaleh>(`${this.apiUrl}/${id}`);
  }

  create(depoType: Havaleh) {
    return this.http.post(`${this.apiUrl}`, depoType);
  }

  update(depoType: Havaleh) {
    return this.http.put(`${this.apiUrl}/${depoType.id}`, depoType);
  }

  delete(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}

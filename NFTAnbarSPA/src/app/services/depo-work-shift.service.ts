import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DepoWorkShift } from '../models/depoWorkShift';
import { GridData } from '../models/GridData';

@Injectable({
  providedIn: 'root'
})
export class DepoWorkShiftService {
  apiUrl = 'http://localhost:5000/api/ndepoWorkShift';

  constructor(private http: HttpClient) {}

  get(options: GridData<DepoWorkShift>) {
    return this.http.get<GridData<DepoWorkShift>>(`${this.apiUrl}/?queryParams=${JSON.stringify(options)}`);
  }

  getById(id: number) {
    return this.http.get<DepoWorkShift>(`${this.apiUrl}/${id}`);
  }

  create(depoType: DepoWorkShift) {
    return this.http.post(`${this.apiUrl}`, depoType);
  }

  update(depoType: DepoWorkShift) {
    return this.http.put(`${this.apiUrl}/${depoType.id}`, depoType);
  }

  delete(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}

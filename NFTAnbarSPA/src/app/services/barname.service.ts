import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Barname } from '../models/barname';
import { GridData } from '../models/GridData';

@Injectable({
  providedIn: 'root'
})
export class BarnameService {
  apiUrl = 'http://localhost:5000/api/barname';

  constructor(private http: HttpClient) {}

  get(options: GridData<Barname>) {
    return this.http.get<GridData<Barname>>(`${this.apiUrl}/?queryParams=${JSON.stringify(options)}`);
  }

  getById(id: number) {
    return this.http.get<Barname>(`${this.apiUrl}/${id}`);
  }

  create(depoType: Barname) {
    return this.http.post(`${this.apiUrl}`, depoType);
  }

  update(depoType: Barname) {
    return this.http.put(`${this.apiUrl}/${depoType.id}`, depoType);
  }

  delete(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}

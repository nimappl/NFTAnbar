import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Depo } from '../models/depo';
import { GridData } from '../models/GridData';

@Injectable({
  providedIn: 'root'
})
export class DepoService {
  apiUrl = 'http://localhost:5000/api/ndepo';

  constructor(private http: HttpClient) {}

  get(options: GridData<Depo>) {
    return this.http.get<GridData<Depo>>(`${this.apiUrl}/?queryParams=${JSON.stringify(options)}`);
  }

  getById(id: number) {
    return this.http.get<Depo>(`${this.apiUrl}/${id}`);
  }

  create(depo: Depo) {
    return this.http.post(`${this.apiUrl}`, depo);
  }

  update(depo: Depo) {
    return this.http.put(`${this.apiUrl}/${depo.id}`, depo);
  }

  delete(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}

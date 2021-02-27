import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Contractor } from '../models/contractor';
import { GridData } from '../models/GridData';

@Injectable({
  providedIn: 'root'
})
export class ContractorService {
  apiUrl = 'http://localhost:5000/api/contractor';

  constructor(private http: HttpClient) {}

  get(options: GridData<Contractor>) {
    return this.http.get<GridData<Contractor>>(`${this.apiUrl}/?queryParams=${JSON.stringify(options)}`);
  }

  getById(id: number) {
    return this.http.get<Contractor>(`${this.apiUrl}/${id}`);
  }

  create(depoType: Contractor) {
    return this.http.post(`${this.apiUrl}`, depoType);
  }

  update(depoType: Contractor) {
    return this.http.put(`${this.apiUrl}/${depoType.id}`, depoType);
  }

  delete(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}

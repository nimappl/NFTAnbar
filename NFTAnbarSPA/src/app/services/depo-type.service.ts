import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DepoType } from '../models/depoType';
import { GridData } from '../models/GridData';

@Injectable({
  providedIn: 'root'
})
export class DepoTypeService {
  apiUrl = 'http://localhost:5000/api/ndepoType';

  constructor(private http: HttpClient) {}

  get(options: GridData<DepoType>) {
    return this.http.get<GridData<DepoType>>(`${this.apiUrl}/?queryParams=${JSON.stringify(options)}`);
  }

  getById(id: number) {
    return this.http.get<DepoType>(`${this.apiUrl}/${id}`);
  }
  
  create(depoType: DepoType) {
    return this.http.post(`${this.apiUrl}`, depoType);
  }

  update(depoType: DepoType) {
    return this.http.put(`${this.apiUrl}/${depoType.id}`, depoType);
  }

  delete(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}

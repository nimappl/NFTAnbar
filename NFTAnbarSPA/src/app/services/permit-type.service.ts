import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GridData } from '../models/GridData';
import { PermitType } from '../models/permitType';

@Injectable({
  providedIn: 'root'
})
export class PermitTypeService {
  apiUrl = 'http://localhost:5000/api/permitType';

  constructor(private http: HttpClient) {}

  get(options: GridData<PermitType>) {
    return this.http.get<GridData<PermitType>>(`${this.apiUrl}/?queryParams=${JSON.stringify(options)}`);
  }

  getById(id: number) {
    return this.http.get<PermitType>(`${this.apiUrl}/${id}`);
  }

  create(depoType: PermitType) {
    return this.http.post(`${this.apiUrl}`, depoType);
  }

  update(depoType: PermitType) {
    return this.http.put(`${this.apiUrl}/${depoType.id}`, depoType);
  }

  delete(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}

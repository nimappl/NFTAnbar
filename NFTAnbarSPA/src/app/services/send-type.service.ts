import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GridData } from '../models/GridData';
import { SendType } from '../models/sendType';

@Injectable({
  providedIn: 'root'
})
export class SendTypeService {
  apiUrl = 'http://localhost:5000/api/sendType';

  constructor(private http: HttpClient) {}

  get(options: GridData<SendType>) {
    return this.http.get<GridData<SendType>>(`${this.apiUrl}/?queryParams=${JSON.stringify(options)}`);
  }

  getById(id: number) {
    return this.http.get<SendType>(`${this.apiUrl}/${id}`);
  }

  create(depoType: SendType) {
    return this.http.post(`${this.apiUrl}`, depoType);
  }

  update(depoType: SendType) {
    return this.http.put(`${this.apiUrl}/${depoType.id}`, depoType);
  }

  delete(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}

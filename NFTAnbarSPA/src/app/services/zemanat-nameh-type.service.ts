import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ZemanatNamehType } from '../models/zemanat-nameh-type';
import { GridData } from '../models/GridData';

@Injectable({
  providedIn: 'root'
})
export class ZemanatNamehTypeService {
  apiUrl = 'http://localhost:5000/api/khzemanatnamehtype';

  constructor(private http: HttpClient) {}

  get(options: GridData<ZemanatNamehType>) {
    return this.http.get<GridData<ZemanatNamehType>>(`${this.apiUrl}/?queryParams=${JSON.stringify(options)}`);
  }

  getById(id: number) {
    return this.http.get<ZemanatNamehType>(`${this.apiUrl}/${id}`);
  }

  create(zemanatNamehType: ZemanatNamehType) {
    return this.http.post(`${this.apiUrl}`, zemanatNamehType);
  }

  update(zemanatNamehType: ZemanatNamehType) {
    return this.http.put(`${this.apiUrl}/${zemanatNamehType.id}`, zemanatNamehType);
  }

  delete(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}

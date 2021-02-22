import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { City } from '../models/city';
import { GridData } from '../models/GridData';

@Injectable({providedIn: 'root'})
export class LocationService {
  apiUrl = 'http://localhost:5000/api/city';

  constructor(private http: HttpClient) {}

  get(options: GridData<City>) {
    return this.http.get<GridData<City>>(`${this.apiUrl}/?queryParams=${JSON.stringify(options)}`);
  }

  getById(id: number) {
    return this.http.get<City>(`${this.apiUrl}/${id}`);
  }
  
  create(city: City) {
    return this.http.post(`${this.apiUrl}`, city);
  }

  update(city: City) {
    return this.http.put(`${this.apiUrl}/${city.id}`, city);
  }

  delete(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}

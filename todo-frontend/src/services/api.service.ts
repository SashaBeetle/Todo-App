import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

export interface Post {
 id: number,
 title: string,
 description: string,
 priority: string,
 dueDate: Date

}

export class Patch{
}
@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private readonly apiUrl = 'https://localhost:7247/api';

  constructor(private http: HttpClient) { 
    
  }


  postHistory(url: string, data: any){
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    const options = { headers };
    url = this.apiUrl+` `+ url;

    return this.http.post<any>(url, data, options)
  }

  postData(url: string, data: any) {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    const options = { headers };

  return this.http.post<any>(url, data, options);
  }

  deleteDataById(url: string, id:number) {
    return this.http.delete(`${url}/${id}`);
  }

  patchData(url: string, data: any) {
    return this.http.patch(url, data);
  }

  getData(url: string) {
    return this.http.get(url)
  }

  getDataById(url: string, id: number) {
    return this.http.get(`${url}/${id}`);
  }
}

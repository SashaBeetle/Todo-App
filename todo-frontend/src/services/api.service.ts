import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable } from 'rxjs';

export interface Post {
 id: number,
 title: string,
 description: string,
 priority: string,
 dueDate: Date

}
@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient) { 
    
  }


  public GetData(): Observable<Post[]>{
    return this.http.get<Post[]>('https://localhost:7247/api/cards')
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

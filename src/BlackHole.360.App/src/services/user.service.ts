import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserModel } from 'src/models/users/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private baseUrl = environment.baseApiUrl + 'users';
  private httpClient = inject(HttpClient);

  public getUsers(offset: number, count: number): Observable<UserModel[]>{
    return this.httpClient.get<UserModel[]>(`${this.baseUrl}?offset=${offset}&count=${count}`);
  }

  public getUser(name: string): Observable<UserModel>{
    return this.httpClient.get<UserModel>(`${this.baseUrl}/${name}`);
  }

  public getByInternalId(id: string): Observable<UserModel>{
    return this.httpClient.get<UserModel>(`${this.baseUrl}/internal/${id}`);
  }
  
  public updateUser(user: UserModel): Observable<UserModel>{
    return this.httpClient.put<UserModel>(`${this.baseUrl}/${user.id}`, user);
  }

}

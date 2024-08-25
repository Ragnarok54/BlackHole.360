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

  public getUsers(search: string | null, offset: number | null = null, count: number | null = null): Observable<UserModel[]>{
    var route = this.baseUrl;

    route += `?search=${search ? search : ''}`;
    route += `&offset=${offset ? offset : 0}`;
    route += `&count=${count ? count : 100000}`;

    return this.httpClient.get<UserModel[]>(route);
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

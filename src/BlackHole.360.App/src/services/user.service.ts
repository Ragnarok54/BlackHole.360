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
    // return of( [{
    //   id: '1',
    //   name: 'John Doe',
    //   jobTitle: 1
    // },
    // {
    //   id: '2',
    //   name: 'Jane Rqaw',
    //   jobTitle: 2
    // },
    // {
    //   id: '2',
    //   name: 'Jane Rqaw',
    //   jobTitle: 2
    // },    {
    //   id: '2',
    //   name: 'Jane Rqaw',
    //   jobTitle: 2
    // },    {
    //   id: '2',
    //   name: 'Jane Rqaw',
    //   jobTitle: 2
    // }]);
    return this.httpClient.get<UserModel[]>(`${this.baseUrl}?offset=${offset}&count=${count}`);
  }
}

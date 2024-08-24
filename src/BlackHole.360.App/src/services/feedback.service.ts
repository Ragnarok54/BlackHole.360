import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { FeedbackModel } from 'src/models/feedback/feedback.model';
import { UserModel } from 'src/models/users/user.model';

@Injectable({
  providedIn: 'root'
})
export class FeedbackService {
  private baseUrl = environment.baseApiUrl + 'feedback';
  private httpClient = inject(HttpClient);

  public getGiven(): Observable<FeedbackModel[]>{
    return this.httpClient.get<FeedbackModel[]>(`${this.baseUrl}`);
  }

  public getReceived(): Observable<FeedbackModel[]>{
    return this.httpClient.get<FeedbackModel[]>(`${this.baseUrl}/received`);
  }
}

import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { NewsModel } from 'src/models/news/news.model';

@Injectable({
  providedIn: 'root'
})
export class NewsService {
  private httpClient: HttpClient = inject(HttpClient);
  private baseUrl = environment.baseApiUrl + 'news';

  public getNews(offset: number, count: number): Observable<NewsModel[]> {
    return this.httpClient.get<NewsModel[]>(this.baseUrl+'?offset='+offset+'&count='+count);
  }
}

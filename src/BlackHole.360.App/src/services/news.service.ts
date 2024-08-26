import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { NewsModel } from 'src/models/news/news.model';

@Injectable({
  providedIn: 'root'
})
export class NewsService {
  private httpClient: HttpClient = inject(HttpClient);
  private baseUrl = '/ace/media/'

  public getNews(): Observable<NewsModel[]> {
    return this.httpClient.get<string>(this.baseUrl).pipe(map(dom => {
      debugger;
      let parser = new DOMParser();
      let doc = parser.parseFromString(dom, 'text/html');
      
      const mediaElements = doc.querySelectorAll('.media_element');
      let news: NewsModel[] = [];

      for(let mediaElement of Array.from(mediaElements)) {
        // Extract the image link
        const imageLink = (mediaElement?.querySelector('a img') as HTMLImageElement)?.src;
        
        // Extract the title
        const title = mediaElement?.querySelector('h2 a')?.textContent!;
        
        // Extract the date
        const date = mediaElement?.querySelector('.media_bara_stiri h3')?.textContent?.split(' ')[1]!;
        
        // Extract the content
        const content = mediaElement?.childNodes[4]?.textContent?.trim()!;

        let newsModel: NewsModel = {
          link: this.baseUrl,
          imgLink: imageLink,
          title: title,
          date: date,
          content: content
        };

        news.push(newsModel);
      }


      return news;
    }));
  }
}

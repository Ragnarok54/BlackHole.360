import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonContent, IonHeader, IonTitle, IonToolbar, IonButtons, IonMenuButton, IonCard, IonCardContent, IonCardTitle, IonCardHeader, IonButton, IonCardSubtitle } from '@ionic/angular/standalone';
import { AuthService } from 'src/services/auth.service';
import { NewsService } from 'src/services/news.service';
import { NewsModel } from 'src/models/news/news.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.page.html',
  styleUrls: ['./home.page.scss'],
  standalone: true,
  imports: [IonCardSubtitle, IonButton, IonCardHeader, IonCardTitle, IonCardContent, IonCard, IonButtons, IonContent, IonHeader, IonTitle, IonToolbar, IonMenuButton, CommonModule, FormsModule]
})
export class HomePage implements OnInit{
  private newsService: NewsService = inject(NewsService);
  
  public news: NewsModel[] = [];

  ngOnInit(): void {
    this.newsService.getNews(0, 20).subscribe(news => {
      console.log(news);
      this.news = news;
    });
  }

}

import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonContent, IonHeader, IonTitle, IonToolbar, IonButtons, IonMenuButton } from '@ionic/angular/standalone';
import { AuthService } from 'src/services/auth.service';
import { NewsService } from 'src/services/news.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.page.html',
  styleUrls: ['./home.page.scss'],
  standalone: true,
  imports: [IonButtons, IonContent, IonHeader, IonTitle, IonToolbar, IonMenuButton, CommonModule, FormsModule]
})
export class HomePage implements OnInit{
  private newsService: NewsService = inject(NewsService);

  ngOnInit(): void {
    this.newsService.getNews().subscribe(news => {
      console.log(news);
    });
  }

}

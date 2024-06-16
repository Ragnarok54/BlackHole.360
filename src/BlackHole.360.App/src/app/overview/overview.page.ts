import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonContent, IonHeader, IonTitle, IonToolbar, IonCardHeader, IonCard, IonCardTitle, IonCardContent, IonList, IonCardSubtitle, IonInfiniteScroll, IonInfiniteScrollContent, IonGrid, IonRow, IonCol, IonSearchbar, IonLabel, IonImg } from '@ionic/angular/standalone';
import { BaseModel } from 'src/models/generic/base.model';
import { UserModel } from 'src/models/users/user.model';
import { IonInfiniteScrollCustomEvent } from '@ionic/core';
import { first } from 'rxjs';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-overview',
  templateUrl: './overview.page.html',
  styleUrls: ['./overview.page.scss'],
  standalone: true,
  imports: [IonImg, IonLabel, IonSearchbar, IonCol, IonRow, IonGrid, IonInfiniteScrollContent, IonInfiniteScroll, IonCardSubtitle, IonList, IonCardContent, IonCardTitle, IonCard, IonCardHeader, IonContent, IonHeader, IonTitle, IonToolbar, IonGrid, CommonModule, FormsModule],
  providers: [UserService]
})
export class OverviewPage implements OnInit {
  private fetchCount: number = 50;
  private userService = inject(UserService);
  
  public users: UserModel[] = [];

  ngOnInit(): void {
    this.fetchData();
  }
 
  protected fetchData(event: IonInfiniteScrollCustomEvent<void> | null = null): void {
    this.userService
      .getUsers(this.users.length, this.fetchCount)
      .pipe(first())
      .subscribe(newChanges => {
        this.users = this.users.concat(newChanges);
         (event as IonInfiniteScrollCustomEvent<void>)?.target.complete();
      });
  }
}

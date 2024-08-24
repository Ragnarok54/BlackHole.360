import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonContent, IonHeader, IonTitle, IonToolbar, IonCardContent, IonList, IonItem, IonLabel, IonInput, IonCardTitle, IonCardHeader, IonCard, IonButton, IonButtons, IonMenuButton, IonFooter } from '@ionic/angular/standalone';
import { AuthService } from 'src/services/auth.service';
import { UserService } from 'src/services/user.service';
import { UserModel } from 'src/models/users/user.model';
import { AccountInfo } from '@azure/msal-browser';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.page.html',
  styleUrls: ['./profile.page.scss'],
  standalone: true,
  imports: [IonFooter, IonButtons, IonButton, IonCard, IonCardHeader, IonCardTitle, IonInput, IonLabel, IonItem, IonList, IonCardContent, IonContent, IonHeader, IonTitle, IonToolbar, IonMenuButton, CommonModule, FormsModule]
})
export class ProfilePage implements OnInit{
  private authService: AuthService = inject(AuthService);
  private userService: UserService = inject(UserService);
  
  public user: UserModel | null = null;

  ngOnInit(): void {
    this.fetchCurrentUser();
  }


  public updateProfile(): void {
    this.userService.updateUser(this.user!).subscribe(user => {
      this.user = user;
    });
  }

  private fetchCurrentUser(): void {
    this.authService.user.pipe().subscribe(accountInfo => {
      if (accountInfo != null){
        this.userService.getByInternalId(accountInfo?.localAccountId!).subscribe(user => {
          this.user = user;
        });
      }else{
        this.user = null;
      }
    });
  }
}

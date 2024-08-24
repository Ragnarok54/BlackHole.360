import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, DestroyRef, OnInit, inject } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { MsalBroadcastService, MsalRedirectComponent, MsalService } from '@azure/msal-angular';
import { EventMessage, EventType, AuthenticationResult, InteractionStatus, InteractionType, PopupRequest, RedirectRequest } from '@azure/msal-browser';
import { IonApp, IonSplitPane, IonMenu, IonContent, IonList, IonListHeader, IonNote, IonMenuToggle, IonItem, IonIcon, IonLabel, IonRouterOutlet, IonRouterLink, IonToolbar, IonHeader, IonTitle, IonImg, IonThumbnail, IonChip, IonFooter } from '@ionic/angular/standalone';
import { Subject, filter, takeUntil } from 'rxjs';
import { AuthService } from 'src/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss'],
  standalone: true,
  imports: [IonFooter, IonChip, IonThumbnail, IonImg, IonTitle, IonHeader, IonToolbar, RouterLink, RouterLinkActive, CommonModule, IonApp, IonSplitPane, IonMenu, IonContent, IonList, IonListHeader, IonNote, IonMenuToggle, IonItem, IonIcon, IonLabel, IonRouterLink, IonRouterOutlet],
  providers: [HttpClient, MsalRedirectComponent]
})
export class AppComponent implements OnInit {
  protected authService: AuthService = inject(AuthService);

  ngOnInit() {
    this.authService.initialize();
  }

  logout(){
    this.authService.logout();
  }
}

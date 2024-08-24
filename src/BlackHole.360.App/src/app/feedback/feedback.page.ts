import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonContent, IonHeader, IonTitle, IonToolbar, IonButtons, IonMenuButton, IonTabs, IonTabBar, IonTabButton, IonIcon, IonSegmentButton, IonSegment, IonLabel, IonFabButton, IonFab } from '@ionic/angular/standalone';
import { FeedbackService } from 'src/services/feedback.service';
import { FeedbackModel } from 'src/models/feedback/feedback.model';

@Component({
  selector: 'app-feedback',
  templateUrl: './feedback.page.html',
  styleUrls: ['./feedback.page.scss'],
  standalone: true,
  imports: [IonFab, IonFabButton, IonLabel, IonSegment, IonSegmentButton, IonIcon, IonTabButton, IonTabBar, IonTabs, IonButtons, IonContent, IonHeader, IonTitle, IonToolbar, IonMenuButton, CommonModule, FormsModule]
})
export class FeedbackPage {
  private feedbackSerivce: FeedbackService = inject(FeedbackService);
  
  protected feedback: FeedbackModel[] = [];
  
  protected getReceivedFeedback(){
    this.feedback = [];
    
    this.feedbackSerivce.getReceived().subscribe(feedback => {
      this.feedback = feedback;
      }
    );
  }

  protected getGivenFeedback(){
    this.feedback = [];
    
    this.feedbackSerivce.getGiven().subscribe(feedback => {
      this.feedback = feedback;
      }
    );
  }
}

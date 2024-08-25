import { AfterViewInit, Component, inject, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonContent, IonHeader, IonTitle, IonToolbar, IonButtons, IonMenuButton, IonTabs, IonTabBar, IonTabButton, IonIcon, IonSegmentButton, IonSegment, IonLabel, IonFabButton, IonFab, IonCard, IonList, IonCardHeader, IonCardTitle, IonCardSubtitle, IonCardContent, IonModal, IonButton, IonItem, IonInput, IonSearchbar, IonCheckbox, IonTextarea } from '@ionic/angular/standalone';
import { FeedbackService } from 'src/services/feedback.service';
import { FeedbackModel } from 'src/models/feedback/feedback.model';
import { UserModel } from 'src/models/users/user.model';
import { UserService } from 'src/services/user.service';
import { ToastController } from '@ionic/angular';

@Component({
  selector: 'app-feedback',
  templateUrl: './feedback.page.html',
  styleUrls: ['./feedback.page.scss'],
  standalone: true,
  imports: [IonTextarea, IonCheckbox, IonSearchbar, IonInput, IonItem, IonButton, IonModal, IonCardContent, IonCardSubtitle, IonCardTitle, IonCardHeader, IonList, IonCard, IonFab, IonFabButton, IonLabel, IonSegment, IonSegmentButton, IonIcon, IonTabButton, IonTabBar, IonTabs, IonButtons, IonContent, IonHeader, IonTitle, IonToolbar, IonMenuButton, CommonModule, FormsModule]
})
export class FeedbackPage implements AfterViewInit {
  @ViewChild('searchUsersModal') searchUsersModal: IonModal | undefined;

  private feedbackSerivce: FeedbackService = inject(FeedbackService);
  private userService: UserService = inject(UserService);
  private toastService: ToastController = inject(ToastController);

  protected feedbackList: FeedbackModel[] = [];
  protected content: string = '';

  public users: UserModel[] = [];
  public selectedUser: UserModel | null = null;

  ngAfterViewInit(): void {
    this.searchUsersModal?.ionModalWillPresent.subscribe(() => {
      this.searchUsers(null);
    });  
  }

  protected getReceivedFeedback(){
    this.feedbackList = [];
    
    this.feedbackSerivce.getReceived().subscribe(feedback => {
      this.feedbackList = feedback;
      }
    );
  }

  protected closeModal(modal: IonModal){
    this.content = '';
    this.selectedUser = null;

    modal.dismiss();
  }
  protected getGivenFeedback(){
    this.feedbackList = [];
    
    this.feedbackSerivce.getGiven().subscribe(feedback => {
      this.feedbackList = feedback;
      }
    );
  }

  public searchUsers(search: string | null){
    this.userService.getUsers(search).subscribe(users => {
      this.users = users;
    });
  }
  

  public sendFeedback(content: string, userId: string){
    let feedback: FeedbackModel = {
      content: content,
      isAnonymous: false,
      toUserId: userId
    };

    this.feedbackSerivce.addFeedback(feedback).subscribe(() => {
      this.toastService.create({
        message: 'Feedback successfully posted!',
        duration: 5000,
        position: 'bottom',
        buttons:[
          {
            text: 'Close',
            role: 'dismiss'
          }
        ]
      });
      
      this.getGivenFeedback();
    });
  }

}

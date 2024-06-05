import { Injectable, inject } from '@angular/core';
import { BaseModel } from 'src/models/generic/base.model';
import { MsalBroadcastService, MsalService } from '@azure/msal-angular';
import { EventMessage, EventType, AuthenticationResult } from '@azure/msal-browser';
import { filter } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private authService: MsalService = inject(MsalService);
  private msalBroadcastService: MsalBroadcastService = inject(MsalBroadcastService);
  
  public isAuthenticated: boolean = false;
  public user: BaseModel | undefined = undefined;

  login(): void {
    this.msalBroadcastService.msalSubject$
      .pipe(
        filter((msg: EventMessage) => msg.eventType === EventType.LOGIN_SUCCESS),
      )
      .subscribe((result: EventMessage) => {
        const payload = result.payload as AuthenticationResult;
        this.authService.instance.setActiveAccount(payload.account);
        this.setLoginDisplay();
      });
      this.setLoginDisplay();
  }

  setLoginDisplay() {
    let a = this.authService.instance.getAllAccounts().length > 0;
  }
}

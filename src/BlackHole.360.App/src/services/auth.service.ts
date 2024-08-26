import { Inject, Injectable, OnInit, inject } from '@angular/core';
import { BaseModel } from 'src/models/generic/base.model';
import { MsalService, MsalBroadcastService, MSAL_GUARD_CONFIG, MsalGuardConfiguration } from '@azure/msal-angular';
import { EventMessage, EventType, AuthenticationResult, InteractionStatus, InteractionType, PopupRequest, RedirectRequest, AccountInfo } from '@azure/msal-browser';
import { BehaviorSubject, Observable, Subject, filter, takeUntil } from 'rxjs';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly _destroying$ = new Subject<void>();
  private msalService: MsalService = inject(MsalService);
  private msalBroadcastService: MsalBroadcastService = inject(MsalBroadcastService);
  private userService: UserService = inject(UserService);
  private user$: Subject<AccountInfo | null> = new BehaviorSubject<AccountInfo | null>(null);

  public isAuthenticated: boolean = false;
  public user: Observable<AccountInfo | null> = this.user$.asObservable();

  constructor(@Inject(MSAL_GUARD_CONFIG) private msalGuardConfig: MsalGuardConfiguration){
    this.msalBroadcastService.msalSubject$
      .pipe(
        filter(
          (msg: EventMessage) =>
            msg.eventType === EventType.ACCOUNT_ADDED ||
            msg.eventType === EventType.ACCOUNT_REMOVED
        )
      )
      .subscribe((result: EventMessage) => {
        if (this.msalService.instance.getAllAccounts().length === 0) {
          window.location.pathname = '/';
        } else {
          this.user$.next(this.msalService.instance.getActiveAccount());
          // //this.setLoginDisplay();
          // this.userService.getUser(this.msalService.instance.getActiveAccount()?.localAccountId!).subscribe(user => {
          //   this.user$.next(user);
          // });
        }
      });

    this.msalBroadcastService.inProgress$
      .pipe(
        filter(
          (status: InteractionStatus) => status === InteractionStatus.None
        ),
        takeUntil(this._destroying$)
      )
      .subscribe(() => {
        this.user$.next(this.msalService.instance.getActiveAccount());
        //this.setLoginDisplay();
        this.checkAndSetActiveAccount();
      });
  }

  checkAndSetActiveAccount() {
     /**
     * If no active account set but there are accounts signed in, sets first account to active account
     * To use active account set here, subscribe to inProgress$ first in your component
     * Note: Basic usage demonstrated. Your app may require more complicated account selection logic
     */
    let activeAccount = this.msalService.instance.getActiveAccount();

    if (!activeAccount && this.msalService.instance.getAllAccounts().length > 0) {
      let accounts = this.msalService.instance.getAllAccounts();
      this.msalService.instance.setActiveAccount(accounts[0]);

      this.userService
        .getUser(this.msalService.instance.getActiveAccount()?.localAccountId!)
        .subscribe(user => {
          //this.user$.next(user);
        });
    }
  }

  initialize(): void {
    this.msalService.handleRedirectObservable().subscribe();
  }

  login() {
    if(this.isLoggedIn()){
      return;
    }

    if (this.msalGuardConfig.authRequest) {
      this.msalService.instance.loginRedirect({ ...this.msalGuardConfig.authRequest } as RedirectRequest);
    } else {
      this.msalService.instance.loginRedirect();
    }
  }

  logout() {
    this.msalService.logoutRedirect();
    this.user$.next(null);
  }

  isLoggedIn(): boolean {
    return this.msalService.instance.getActiveAccount() != null;
  }

  getName(): string {
    const account = this.msalService.instance.getActiveAccount();
    return account ? account.name! : '';
  }

  getEmail(): string {
    const account = this.msalService.instance.getActiveAccount();
    return account ? account.username! : '';
  }

  getId(): string {
    const account = this.msalService.instance.getActiveAccount();
    return account ? account.localAccountId! : '';
  }
}

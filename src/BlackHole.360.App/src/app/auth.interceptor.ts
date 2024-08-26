import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from 'src/services/auth.service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  let authService = inject(AuthService);

  const reqWithHeader = req.clone({
    headers: req.headers.set('currentUserId', authService.getId()),
  });
  
  return next(reqWithHeader);
};

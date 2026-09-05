import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { providePrimeNG } from 'primeng/config';
import Lara from '@primeng/themes/lara';
import { authInterceptor } from './core/auth.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [provideZoneChangeDetection({ eventCoalescing: true }),
     providePrimeNG({ 
            theme: {
                preset: Lara,
                options: {
                    darkModeSelector: false
                }
            }
        }),
         provideRouter(routes), provideHttpClient(withInterceptors([authInterceptor]))],
};

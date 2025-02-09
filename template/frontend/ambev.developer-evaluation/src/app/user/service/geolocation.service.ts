import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class GeolocationService {
  getCurrentLocation(): Observable<{ latitude: number; longitude: number }> {
    return new Observable((observer) => {
      if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
          (position) => {
            observer.next({
              latitude: position.coords.latitude,
              longitude: position.coords.longitude,
            });
            observer.complete();
          },
          (error) => {
            observer.error(error);
          }
        );
      } else {
        observer.error('Geolocalização não é suportada pelo seu navegador.');
      }
    });
  }
}

import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StorageService {
  isBrowser = typeof window !== 'undefined';

  set(key: string, value: string) {
    if (this.isBrowser) {
      localStorage.setItem(key, value);
    }
  }

  get(key: string): string | null {
    if (this.isBrowser) {
      return localStorage.getItem(key);
    }
    return null;
  }

  remove(key: string) {
    if (this.isBrowser) {
      localStorage.removeItem(key);
    }
  }

  clearAll() {
    if (this.isBrowser) {
      localStorage.clear();
    }
  }
}
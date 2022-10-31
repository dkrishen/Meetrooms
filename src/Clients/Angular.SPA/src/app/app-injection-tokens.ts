import { InjectionToken } from "@angular/core";

export const BACK_API_URL = new InjectionToken<string>('back api url');
export const AUTH_API_URL = new InjectionToken<string>('auth api url');
export const SIGNALR_HUB_URL = new InjectionToken<string>('signalr hub url');
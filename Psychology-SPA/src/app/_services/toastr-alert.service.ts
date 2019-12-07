import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class ToastrAlertService {

  constructor(private toastr: ToastrService) { }
  success(message: string) {
    return this.toastr.success(message);
  }
  info(message: string) {
    return this.toastr.info(message);
  }
  error(message: any) {
    if (message.error) {
      return this.toastr.error(message.error);
    } else {
      return this.toastr.error(message);
    }
  }
  warning(message: string) {
    return this.toastr.warning(message);
  }
}

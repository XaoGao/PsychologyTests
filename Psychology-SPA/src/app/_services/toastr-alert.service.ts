import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class ToastrAlertService {

  constructor(private toastr: ToastrService) { }
  public success(message: string) {
    return this.toastr.success(message);
  }
  public info(message: string) {
    return this.toastr.info(message);
  }
  public error(message: any) {
    if (message.error) {
      return this.toastr.error(message.error);
    } else {
      return this.toastr.error(message);
    }
  }
  public warning(message: string) {
    return this.toastr.warning(message);
  }
}

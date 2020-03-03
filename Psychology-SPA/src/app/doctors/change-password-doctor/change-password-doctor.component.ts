import { AuthService } from './../../_services/auth.service';
import { Doctor } from './../../_models/doctor';
import { ToastrAlertService } from './../../_services/toastr-alert.service';
import { ActivatedRoute } from '@angular/router';
import { DoctorService } from './../../_services/doctor.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-change-password-doctor',
  templateUrl: './change-password-doctor.component.html',
  styleUrls: ['./change-password-doctor.component.css']
})
export class ChangePasswordDoctorComponent implements OnInit {

  public doctor: Doctor;
  public password: string;
  public confirmPassword: string;
  public newPassword: string;

  changePasswordForm: FormGroup;

  constructor(private toastrService: ToastrAlertService,
              private route: ActivatedRoute,
              private doctorService: DoctorService,
              private authService: AuthService,
              private fb: FormBuilder) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.doctor = data.doctor;
    });
    this.createChangePasswordForm();
    this.password = '';
    this.confirmPassword = '';
    this.newPassword = '';
  }
  public changePassword(): void {
    if (this.changePasswordForm.valid) {
      this.authService.changePassword(this.authService.doctorId, '').subscribe(() => {
        this.toastrService.success('Вы успешно поменяли пароль');
      }, err => {
        this.toastrService.error(err);
      });
    }
  }

  private createChangePasswordForm() {
    this.changePasswordForm = this.fb.group({
      password: ['', Validators.required, Validators.minLength(6)],
      confirmPassword: ['', Validators.required],
      newPassword: ['', Validators.required, Validators.minLength(6)]
    }, { validator: this.passwordMatchValidator});
  }
  private passwordMatchValidator(g: FormGroup) {
    return g.get('password').value === g.get('confirmPassword').value ? null : { mismatch: true };
  }
}

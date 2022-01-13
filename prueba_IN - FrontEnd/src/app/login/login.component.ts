import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';

import { LoginService } from '../login.service';
import { User } from '../models/user';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm = this.formBuilder.group({
    user: '',
    password: ''
  });

  errorMessage: string | undefined;

  constructor(
    private formBuilder: FormBuilder,
    private loginService: LoginService,
    private router: Router
  ) { }

  ngOnInit(): void {
  }

  onSubmit(): void {
    let user: string = this.loginForm.value['user'];
    let pwd: string = this.loginForm.value['password'];
    let validForm: string = this.validateForm(user, pwd);
    if (validForm === 'Ok') {
      let userData: User = {
        id: 0,
        userName: user,
        password: pwd
      }
      this.loginService.login(userData).subscribe(userLogin => {
        if (!userLogin) {
          this.errorMessage = 'Credenciales no válidas';
        } else if (userLogin.user.userName === userData.userName) {
          console.log(this.loginService.token);
          this.router.navigate(['/sync']);
        } else {
          this.errorMessage = 'Credenciales no válidas';
        }
      });
    } else {
      this.errorMessage = validForm;
    }
  }

  private validateForm(user: string, pwd: string): string {
    if (!(user.length > 0)) {
      return 'Ingrese nombre de usuario';
    } else if (!(pwd.length > 0)) {
      return 'Ingrese contraseña';
    } else {
      return 'Ok';
    }
  }

}

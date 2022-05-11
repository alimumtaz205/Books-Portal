import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Route, Router } from '@angular/router';
import { AccountService } from 'src/app/_service/account.service';
import { AlertifyService } from 'src/app/_service/alertify/alertify.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  userName:any;
  uPassword:any;
  data:any;
  response:any;

  loginForm = this.fb.group({
    userName: ['', Validators.required],
    uPassword: ['', Validators.required]
  });

  constructor(private fb: FormBuilder,
              private router: Router, 
              private alertify: AlertifyService,
              private service: AccountService) { }

  ngOnInit(): void {
  }

  onSubmit(model:any) {
      debugger;
      this.service.login(model).subscribe((result) => {
        console.log(result);
        this.data=result;
        if(this.data.tranCode==0 && this.data.isSuccess==true)
        {
          this.alertify.success(this.data.message);
          this.router.navigateByUrl('dashboard');
        }
        else{
          this.alertify.error(this.data.message);
        }


      });
      
  }
}

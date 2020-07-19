import { Component } from '@angular/core';
import { first } from 'rxjs/operators';

import { User, Vitual } from '@app/_models';
import { UserService, AuthenticationService, AlertService } from '@app/_services';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({ templateUrl: 'home.component.html' })
export class HomeComponent {
    VitalsForm: FormGroup;
    loading = false;
    submitted = false;
    returnUrl: string;
    error = '';
    vitualData:Vitual;
    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private authenticationService: AuthenticationService,
        private UserService:UserService,
        private alertService: AlertService
    ) { 
        this.vitualData= new Vitual();
       
    }

    ngOnInit() {
        this.VitalsForm = this.formBuilder.group({
            Temperature: ['', Validators.required,,Validators.pattern('^\\d+\\.\\d{2}$')],
            HeartRate: ['', Validators.required],
            DeviceId:['',Validators.required],
            BusinessUnitId:['',Validators.required]
        });
    }

    // convenience getter for easy access to form fields
    get f() { return this.VitalsForm.controls; }

    showView(){
        this.router.navigate(['/viewdata']);
    }

    onSubmit() {
        this.submitted = true;

        // stop here if form is invalid
        if (this.VitalsForm.invalid) {
            return;
        }

        this.loading = true;
        
        this.vitualData=new Vitual();
       
            this.vitualData.temperature=this.f.Temperature.value,
            this.vitualData.deviceId=this.f.DeviceId.value,
            this.vitualData.heartRate=this.f.HeartRate.value,
            this.vitualData.businessUnitId=this.f.BusinessUnitId.value
            this.vitualData.organizationId="mnbvcxzlkjhgfdsapoiuytrewq098765"
        
        
        this.UserService.AddData(this.vitualData)
            .pipe(first())
            .subscribe(
                data => {
                    this.alertService.success('Added successful', true);
                },
                error => {
                    this.error = error;
                    this.loading = false;
                });
    }
}


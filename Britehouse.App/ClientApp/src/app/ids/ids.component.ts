import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { BusinessService } from '../business.service';

@Component({
  selector: 'app-ids',
  templateUrl: './ids.component.html',
  styleUrls: ['./ids.component.css']
})
export class IdsComponent implements OnInit {
  angForm: FormGroup;
  constructor(private formBuilder: FormBuilder,private businessService:BusinessService) {
    this.createForm();
  }

  createForm() {
    this.angForm = this.formBuilder.group({
      idNumber:['',Validators.required]
    });
  }
  errorMessage: string;
  saveId(idNumber) {
    this.businessService.saveId(idNumber);
  }
  ngOnInit() {
  }

}

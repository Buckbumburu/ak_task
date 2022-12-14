import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { IAddCustomerInput } from '../../models/add-customer-input';
import { ICustomerInfo } from '../../models/customer-info';

@Component({
  selector: 'add-customer-dialog',
  templateUrl: './add-customer-dialog.component.html',
  styleUrls: ['./add-customer-dialog.component.css']
})
export class AddCustomerDialogComponent
{
  public name: FormControl;
  public lastName: FormControl;
  public email: FormControl;
  public address: FormControl;
  private homePhone: FormControl;
  private workPhone: FormControl;
  private mobilePhone: FormControl;
  public customerForm: FormGroup;
  public isAdding: boolean;

  constructor(
    private httpClient: HttpClient,
    private dialogRef: MatDialogRef<AddCustomerDialogComponent>)
  {
    this.isAdding = false;
    this.name = new FormControl("", [Validators.required, Validators.minLength(1)]);
    this.lastName = new FormControl("", [Validators.required, Validators.minLength(1)]);
    this.email = new FormControl('', [Validators.required, Validators.email]);
    this.address = new FormControl("", [Validators.required, Validators.minLength(1)]);
    this.homePhone = new FormControl("");
    this.workPhone = new FormControl("");
    this.mobilePhone = new FormControl("");

    this.customerForm = new FormGroup({
      name: this.name,
      lastName: this.lastName,
      email: this.email,
      address: this.address,
      homePhone: this.homePhone,
      workPhone: this.workPhone,
      mobilePhone: this.mobilePhone
    });
  }

  public get isMissingPhone(): boolean
  {
    if (this.homePhone.value != null && this.homePhone.value !== "")
    {
      return false;
    }

    if (this.workPhone.value != null && this.workPhone.value !== "")
    {
      return false;
    }

    if (this.mobilePhone.value != null && this.mobilePhone.value !== "")
    {
      return false;
    }

    return true;
  }

  public getNameErrorMessage(): string
  {
    return this.getTextFieldErrorMessage(this.name);
  }

  public getLastNameErrorMessage(): string
  {
    return this.getTextFieldErrorMessage(this.lastName);
  }

  public getEmailErrorMessage(): string
  {
    if (this.email.hasError("required"))
    {
      return "This field is required";
    }

    if (this.email.hasError("email"))
    {
      return "Email is not valid";
    }

    return "";
  }

  public getAddressErrorMessage(): string
  {
    return this.getTextFieldErrorMessage(this.address);
  }

  private getTextFieldErrorMessage(control: FormControl): string
  {
    if (control.hasError("required") || control.hasError("minLength"))
    {
      return "This field is required";
    }

    return "";
  }

  public onAddClick(): void
  {
    if (this.isAdding)
    {
      return;
    }

    const input = this.createAddCustomerInput();

    this
      .httpClient
      .post<ICustomerInfo>("/api/customer", input)
      .subscribe(customer =>
      {
        this.dialogRef.close(customer);
      },
      () =>
      {
        this.isAdding = false;
      },
      () =>
      {
        this.isAdding = false;
      });
  }

  private createAddCustomerInput(): IAddCustomerInput
  {
    return <IAddCustomerInput>{
      name: this.name.value,
      lastName: this.lastName.value,
      email: this.email.value,
      address: this.address.value,
      homePhone: this.homePhone.value !== ""
        ? this.homePhone.value
        : null,
      workPhone: this.workPhone.value !== ""
        ? this.workPhone.value
        : null,
      mobilePhone: this.mobilePhone.value !== ""
        ? this.mobilePhone.value
        : null
    };
  }

  public onCancelClick(): void
  {
    this.dialogRef.close();
  }
}

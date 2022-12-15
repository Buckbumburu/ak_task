import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ICustomerInfo } from '../../models/customer-info';

@Component({
  selector: 'delete-customer-dialog',
  templateUrl: './delete-customer-dialog.component.html',
  styleUrls: ['./delete-customer-dialog.component.css']
})
export class DeleteCustomerDialogComponent
{
  public fullName: string;

  constructor(
    private dialogRef: MatDialogRef<DeleteCustomerDialogComponent>,
    @Inject(MAT_DIALOG_DATA) private customerToDelete: ICustomerInfo)
  {
    this.fullName = customerToDelete.fullName;
  }

  public onOkClick()
  {
    this.dialogRef.close(true);
  }

  public onCancelClick(): void
  {
    this.dialogRef.close();
  }
}

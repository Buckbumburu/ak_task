import { Component, AfterViewInit } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { ICustomerInfo } from "./models/customer-info"
import { MatDialog } from '@angular/material/dialog';
import { AddCustomerDialogComponent } from './components/add-customer-dialog/add-customer-dialog.component';
import { CustomerDataSource } from './customer-data-source';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements AfterViewInit
{
  public displayedColumns: string[];
  public customers: CustomerDataSource;

  constructor(
    private httpClient: HttpClient,
    private dialog: MatDialog)
  {
    this.displayedColumns = [
      "full-name", "email", "address", "home-phone",
      "work-phone", "mobile-phone", "customer-actions"];
    this.customers = new CustomerDataSource();
  }
    
  public ngAfterViewInit(): void
  {
    this
      .httpClient
      .get<ICustomerInfo[]>("/api/customer")
      .subscribe((customers: ICustomerInfo[]) =>
      {
        this.customers.setData(customers);
      });
  }

  public onAddCustomerClick(): void
  {
    const dialogRef = this.dialog.open(AddCustomerDialogComponent);

    dialogRef.afterClosed().subscribe((result: ICustomerInfo) =>
    {
      if (result == null)
      {
        return;
      }

      this.customers.add(result);
    });
  }

  public onEditCustomerClick(customer: ICustomerInfo): void
  {
  }

  public onRemoveCustomerClick(customer: ICustomerInfo): void
  {
  }
}

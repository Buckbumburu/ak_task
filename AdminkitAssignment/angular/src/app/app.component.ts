import { Component, AfterViewInit, ViewChild } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { ICustomerInfo } from "./models/customer-info"
import { MatDialog } from '@angular/material/dialog';
import { AddOrUpdateCustomerDialogComponent } from './components/add-or-update-customer-dialog/add-or-update-customer-dialog.component';
import { DeleteCustomerDialogComponent } from './components/delete-customer-dialog/delete-customer-dialog.component';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements AfterViewInit
{
  public displayedColumns: string[];
  public dataSource: MatTableDataSource<ICustomerInfo>;
  private customers: ICustomerInfo[];

  @ViewChild(MatSort) sort: MatSort | undefined;

  constructor(
    private httpClient: HttpClient,
    private dialog: MatDialog)
  {
    this.displayedColumns = [
      "fullName", "email", "address", "homePhone",
      "workPhone", "mobilePhone", "customer-actions"];
    this.dataSource = new MatTableDataSource<ICustomerInfo>([]);
    this.customers = [];
  }

  public get customerCount(): number
  {
    return this.customers.length;
  }

  public ngAfterViewInit(): void
  {
    this.dataSource.sort = this.sort!;
    this
      .httpClient
      .get<ICustomerInfo[]>("/api/customer")
      .subscribe((customers: ICustomerInfo[]) =>
      {
        this.customers = customers;
        this.dataSource.data = customers;
      });
  }

  public onAddCustomerClick(): void
  {
    const dialogRef = this.dialog.open(AddOrUpdateCustomerDialogComponent);

    dialogRef.afterClosed().subscribe((result: ICustomerInfo) =>
    {
      if (result == null)
      {
        return;
      }

      this.customers.push(result);
      this.dataSource.data = this.customers;
    });
  }

  public onEditCustomerClick(customer: ICustomerInfo): void
  {
    const dialogRef = this.dialog.open(AddOrUpdateCustomerDialogComponent, {
      data: customer
    });

    dialogRef.afterClosed().subscribe((result: ICustomerInfo) =>
    {
      if (result == null)
      {
        return;
      }

      customer.fullName = result.fullName;
      customer.email = result.email;
      customer.address = result.fullName;
      customer.homePhone = result.homePhone;
      customer.workPhone = result.workPhone;
      customer.mobilePhone = result.mobilePhone;
      this.dataSource.data = this.customers;
    });
  }

  public onDeleteCustomerClick(customer: ICustomerInfo): void
  {
    const dialogRef = this.dialog.open(DeleteCustomerDialogComponent, {
      data: customer
    });

    dialogRef.afterClosed().subscribe((result: ICustomerInfo) =>
    {
      if (!result)
      {
        return;
      }

      this
        .httpClient
        .delete(`/api/customer/${customer.id}`)
        .subscribe(() =>
        {
          const index = this.customers.indexOf(customer);

          if (index == -1) {
            return;
          }

          this.customers.splice(index, 1);
          this.dataSource.data = this.customers;
        });
    });
  }
}

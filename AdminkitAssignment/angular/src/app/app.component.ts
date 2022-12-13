import { Component, AfterViewInit, OnDestroy } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { ICustomerInfo } from "./models/customer-info"
import { DataSource } from '@angular/cdk/collections';
import { Observable, BehaviorSubject, Subscription } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { AddCustomerDialogComponent } from './components/add-customer-dialog/add-customer-dialog.component';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements AfterViewInit, OnDestroy
{
  public displayedColumns: string[];
  public customers: CustomerDataSource;
  public totalCustomers: number;
  private totalCustomersSubscription?: Subscription;

  constructor(private httpClient: HttpClient)
  {
    this.displayedColumns = [
      "full-name", "email", "address", "home-phone",
      "work-phone", "mobile-phone", "customer-actions"];
    this.customers = new CustomerDataSource();
    this.totalCustomers = 0;
  }
    
  public ngAfterViewInit(): void
  {
    this.totalCustomersSubscription = this
      .customers
      .connect()
      .subscribe((customers: readonly ICustomerInfo[]) =>
      {
        this.totalCustomers = customers.length;
      });

    this
      .httpClient
      .get<ICustomerInfo[]>("/api/customer")
      .subscribe((customers: ICustomerInfo[]) =>
      {
        this.customers.setData(customers);
      });
  }

  ngOnDestroy(): void
  {
    if (!this.totalCustomersSubscription)
    {
      return;
    }

    this.totalCustomersSubscription.unsubscribe();
  }

  public onAddCustomerClick(): void
  {
  }

  public onEditCustomerClick(customer: ICustomerInfo): void
  {
  }

  public onRemoveCustomerClick(customer: ICustomerInfo): void
  {
  }
}

class CustomerDataSource extends DataSource<ICustomerInfo>
{
  private customerSubject: BehaviorSubject<ICustomerInfo[]>;
  private customerObservable: Observable<ICustomerInfo[]>;

  constructor()
  {
    super();
    this.customerSubject = new BehaviorSubject<ICustomerInfo[]>([]);
    this.customerObservable = this.customerSubject.asObservable();
  }

  public connect(): Observable<readonly ICustomerInfo[]>
  {
    return this.customerObservable;
  }

  public disconnect(): void
  {
  }

  public setData(customers: ICustomerInfo[])
  {
    this.customerSubject.next(customers);
  }
}

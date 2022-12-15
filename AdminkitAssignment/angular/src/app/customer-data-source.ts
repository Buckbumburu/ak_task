import { ICustomerInfo } from "./models/customer-info"
import { DataSource } from '@angular/cdk/collections';
import { Observable, BehaviorSubject } from 'rxjs';

export class CustomerDataSource extends DataSource<ICustomerInfo>
{
  private customerSubject: BehaviorSubject<ICustomerInfo[]>;
  private customerObservable: Observable<ICustomerInfo[]>;
  private current: ICustomerInfo[];

  constructor()
  {
    super();
    this.current = [];
    this.customerSubject = new BehaviorSubject<ICustomerInfo[]>(this.current);
    this.customerObservable = this.customerSubject.asObservable();
  }

  public get total(): number
  {
    return this.current.length;
  }

  public connect(): Observable<readonly ICustomerInfo[]>
  {
    return this.customerObservable;
  }

  public disconnect(): void
  {
  }

  public add(customer: ICustomerInfo)
  {
    this.current.push(customer);
    this.setData(this.current);
  }

  public delete(customer: ICustomerInfo)
  {
    const index = this.current.indexOf(customer);

    if (index == -1)
    {
      return;
    }

    this.current.splice(index, 1);
    this.setData(this.current);
  }

  public setData(customers: ICustomerInfo[])
  {
    this.current = customers;
    this.customerSubject.next(customers);
  }
}

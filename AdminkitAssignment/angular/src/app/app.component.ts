import { Component, AfterViewInit } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { ICustomerInfo } from "./models/customer-info"

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements AfterViewInit
{
    public customers: ICustomerInfo[] = [];

    constructor(private httpClient: HttpClient)
    {
    }
    
    public ngAfterViewInit(): void
    {
        this
            .httpClient
            .get<ICustomerInfo[]>("/api/customer")
            .subscribe((customers: ICustomerInfo[]) =>
            {
                this.customers = customers;
            });
    }
}

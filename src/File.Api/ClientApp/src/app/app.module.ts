import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { FileComponent } from "./file/file.component";
import { ListComponent } from "./components/list/list.component";
import { HttpClientModule } from "@angular/common/http";

@NgModule({
    declarations: [AppComponent, FileComponent, ListComponent],
    imports: [BrowserModule, AppRoutingModule, HttpClientModule],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule {}

import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from '@angular/common/http'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TreeComponent } from './components/tree/tree.component';
import { CookBookComponent } from './components/cook-book/cook-book.component';
import { TreeViewComponent } from './components/tree-view/tree-view.component';
import { ResultTreeViewComponent } from './components/result-tree-view/result-tree-view.component'
import { AngularTreeGridModule } from 'angular-tree-grid';
import { NodeContentComponent } from './components/node-content/node-content.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    TreeComponent,
    CookBookComponent,
    TreeViewComponent,
    ResultTreeViewComponent,
    NodeContentComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    AngularTreeGridModule,
    NgbModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

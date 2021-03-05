import { Component, Input, OnInit, Output } from '@angular/core';
import { ITreeComponent, treesConst } from './components/tree-view/tree-view.component';
import { TreeService } from './services/tree.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent{
  title = 'CookBook-UI';
}

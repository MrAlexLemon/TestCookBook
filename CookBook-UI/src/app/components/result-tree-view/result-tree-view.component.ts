import { OnInit } from '@angular/core';
import { Component, Input, Output, EventEmitter } from '@angular/core';
import { TreeService } from 'src/app/services/tree.service';
import { ITreeComponent } from '../tree-view/tree-view.component';
import { ITreeNode } from '../tree/tree.component';

@Component({
  selector: 'result-tree-view',
//  templateUrl: "./tree.component.html",
template: `
<div>
  <h1>Test Tree</h1>
  <tree-view [SendTrees]="Nodes" [SelectedNode]="selectedNode"
  (onSelectedChanged)="onSelectNode($event)"
  (onRequestNodes)="onRequest($event)">
  </tree-view>
</div>
`,

//  styleUrls: ["./tree.component.css"]
  styles: [
        '.treecomponents {display:table; list-style-type: none; padding-left: 16px;}',
        '.treecomponent { display: table-row; list-style-type: none; }',
        '.nodebutton { display:table-cell; cursor: pointer; }',
        '.nodeinfo { display:table-cell; padding-left: 5px; list-style-type: none; }',
        '.nodetext { padding-left: 3px; padding-right: 3px; cursor: pointer; }',
        '.nodetext.bg-info { font-weight: bold; }',
        '.nodetext.text-root { font-size: 16px; font-weight: bold; }'
    ]
})
export class ResultTreeViewComponent{

  Nodes: Array<ITreeComponent>;
    selectedNode: ITreeComponent; // нужен для отображения детальной информации по выбранному узлу.

    constructor(private treeService: TreeService) {
      this.Nodes = [];
      this.selectedNode = {} as ITreeComponent;
    }

    // начальное заполнение верхнего уровня иерархии
    ngOnInit() {
      console.log("lol1");
      this.treeService.GetRoot().subscribe(
        //res => console.log(res),
        res => {this.Nodes = res as ITreeComponent[]},
        //console.log(this.Nodes),
        error => console.log(error)
      );
    }

    
    // обработка события смены выбранного узла
    onSelectNode(node: ITreeComponent) {
      console.log("onSelectNode");
      this.selectedNode = node;
      console.log(node);
      //console.log(this.selectedNode);
    }
    // обработка события вложенных узлов
    onRequest(parent: ITreeComponent) {
      console.log("onRequest");
      console.log("Parent: ");
      console.log(parent);
      if(parent.leaf)
        return;
      this.treeService.GetNodes(parent.value).subscribe(
        res => {parent.trees = res as ITreeComponent[], console.log(res)},
        error=> console.log(error));
    }

}

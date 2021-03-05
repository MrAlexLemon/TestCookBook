import { Directive } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { TreeService } from 'src/app/services/tree.service';
import { ITreeNode, TestClass } from '../tree/tree.component';

@Component({
  selector: 'app-cook-book',
  templateUrl: './cook-book.component.html',
  styleUrls: ['./cook-book.component.css']
})
export class CookBookComponent implements OnInit {

    Nodes: Array<ITreeNode>;
    selectedNode: ITreeNode; // нужен для отображения детальной информации по выбранному узлу.

    constructor(private treeService: TreeService) {
      this.Nodes = [];
      this.selectedNode = {} as ITreeNode;
    }

    // начальное заполнение верхнего уровня иерархии
    ngOnInit() {
      console.log("lol");
      this.treeService.GetRoot().subscribe(
        //res => console.log(res),
        res => {this.Nodes = res as ITreeNode[],
          this.Nodes.forEach(x=>{x.badge=1, x.name="asd"})},
        //console.log(this.Nodes),
        error => console.log(error)
      );
    }
    // обработка события смены выбранного узла
    onSelectNode(node: ITreeNode) {
      console.log("onSelectNode");
      this.selectedNode = node;
      console.log(node);
      console.log(this.selectedNode);
    }
    // обработка события вложенных узлов
    onRequest(parent: ITreeNode) {
      console.log("onRequest");
      console.log("Parent: ");
      console.log(parent);
      if(parent.isLeaf)
        return;
      this.treeService.GetNodes(parent.value).subscribe(
        res => {parent.children = res as ITreeNode[], console.log(res)},
        error=> console.log(error));
    }

}

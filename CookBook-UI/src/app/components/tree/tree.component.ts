import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

export interface TestClass{
  A:number;
  B:number;
  C:number;
}

export interface ITreeNode {
	id: number;
	name: string;
	children: Array<ITreeNode>;
	isExpanded: boolean;
	badge: number;
	parent: ITreeNode;
	isLeaf: boolean;
  value: TestClass;
}

@Component({
  selector: 'app-tree',
  templateUrl: './tree.component.html',
  styleUrls: ['./tree.component.css']
})
export class TreeComponent implements OnInit {

  @Input() Nodes: Array<ITreeNode>;
	@Input() SelectedNode: ITreeNode;

	@Output() onSelectedChanged: EventEmitter<ITreeNode> = new EventEmitter<ITreeNode>();
	@Output() onRequestNodes: EventEmitter<ITreeNode> = new EventEmitter<ITreeNode>();

	constructor() { 
    this.Nodes = [];
    this.SelectedNode = {} as ITreeNode;
  }

	onSelectNode(node: ITreeNode) {
    console.log("TestonSelectNode");
    //console.log(node);
		this.onSelectedChanged.emit(node);
	}

	onExpand(node: ITreeNode) {
    console.log("Expandtest");
		node.isExpanded = !node.isExpanded;

		if (node.isExpanded && (!node.children || node.children.length === 0)) {
			this.onRequestNodes.emit(node);
		}
	}

	onRequestLocal(node: ITreeNode) {
    console.log("TestonRequestLocal");
    console.log(node);
		this.onRequestNodes.emit(node);
	}

  ngOnInit(): void {
    console.log("test");
  }

}

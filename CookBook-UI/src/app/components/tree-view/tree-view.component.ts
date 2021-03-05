import { Component, Input, Output, EventEmitter } from '@angular/core';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { TreeService } from 'src/app/services/tree.service';
import { NodeContentComponent } from '../node-content/node-content.component';
import { TestClass } from '../tree/tree.component';

export interface ITreeComponent {
  leaf: boolean;
  parentCategoryId: number;
  categoryName: string;
  categoryId: number;
  url: string;
  trees: Array<ITreeComponent>;
  isExpanded?: boolean;
  value: TestClass;
}

export const treesConst:ITreeComponent[] = [
  // {
  //     "leaf":true,
  //     "parentCategoryId":10001,
  //     "categoryName":"Первый родитель",
  //     "categoryId":10500,
  //     "url":"first-parent",
  //     "trees":
  //     [{
  //         "leaf":false,
  //         "parentCategoryId":10500,
  //         "categoryName":"Ребенок первого родителя",
  //         "categoryId":10515,
  //         "url":"children-first-parent",
  //         "trees":[]
  //     }]
  // },
  // {
  //     "leaf":true,
  //     "parentCategoryId":10001,
  //     "categoryName":"Второй родитель",
  //     "categoryId":10102,
  //     "url":"second-parent",
  //     "trees":
  //     [{
  //         "leaf":true,
  //         "parentCategoryId":10102,
  //         "categoryName":"Первый ребенок второго родителя",
  //         "categoryId":10614,
  //         "url":"first-children-second-parent",
  //         "trees":
  //         [{
  //             "leaf":false,
  //             "parentCategoryId":10614,
  //             "categoryName":"Внук",
  //             "categoryId":10633,
  //             "url":"vanok",
  //             "trees":[]
  //         },
  //         {
  //             "leaf":false,
  //             "parentCategoryId":10614,
  //             "categoryName":"Внучара",
  //             "categoryId":10634,
  //             "url":"big-boy",
  //             "trees":[]
  //         },
  //         {
  //             "leaf":false,
  //             "parentCategoryId":10614,
  //             "categoryName":"Внучка",
  //             "categoryId":10867,
  //             "url":"top-one-princes-good-girl",
  //             "trees":[]
  //         }]
  //     },
  //     {
  //         "leaf":true,
  //         "parentCategoryId":10102,
  //         "categoryName":"Второй ребенок второго родителя",
  //         "categoryId":10473,
  //         "url":"second-children-second-parent",
  //         "trees":
  //         [{
  //             "leaf":false,
  //             "parentCategoryId":10473,
  //             "categoryName":"Внук",
  //             "categoryId":10478,
  //             "url":"vano",
  //             "trees":[]
  //         },
  //         {
  //             "leaf":false,
  //             "parentCategoryId":10473,
  //             "categoryName":"Внучка",
  //             "categoryId":10475,
  //             "url":"olya",
  //             "trees":[]
  //         }]
  //     }]
  // },
  // {
  //     "leaf":true,
  //     "parentCategoryId":10001,
  //     "categoryName":"Соседка из квартиры 66",
  //     "categoryId":10105,
  //     "url":"sosedka-rembo",
  //     "trees":
  //     [{
  //         "leaf":false,
  //         "parentCategoryId":10105,
  //         "categoryName":"Сын маминой подруги",
  //         "categoryId":10360,
  //         "url":"cool-boy",
  //         "trees":[]
  //     }]
  // }
];
//<i class="btn btn-light glyphicon glyphicon-{{tree.isExpanded ? 'minus' : 'plus'}}" (click)="onExpand(tree)"></i>
//<i class="btn btn-light fa fa-minus" (click)="onExpand(tree)"></i>

// <button type="button" class="btn btn-outline-secondary btn-number" data-type="minus" >
// <span class="fa fa-minus"></span>
//                     </button>

@Component({
  selector: 'tree-view',
//  templateUrl: "./tree.component.html",
template: `
<ul class="treecomponents">
  <li *ngFor="let tree of SendTrees" class="treecomponent">
  <i class="btn btn-light fa fa-{{(tree.isExpanded || tree.leaf) ? 'minus' : 'plus'}}" (click)="onExpand(tree)"></i>
    <div class="nodeinfo">

    <button (click)="openModal()">open the modal</button>

      <span (click)="onSelectNode(tree)">
            {{tree.value}}
            </span>

      <tree-view [SendTrees]="tree.trees" [SelectedNode]="SelectedNode" (onSelectedChanged)="onSelectNode($event)" (onRequestNodes)="onRequestLocal($event)" *ngIf="tree.isExpanded">
      </tree-view>
    </div>
  </li>
</ul>
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
export class TreeViewComponent{

  @Input() SendTrees: Array<ITreeComponent>;    // Список узлов на данном уровне
  @Input() SelectedNode: ITreeComponent;        // Выбранный пользователем узел

  @Output() onSelectedChanged: EventEmitter<ITreeComponent> = new EventEmitter<ITreeComponent>();   // Смена выбранного пользователем узла
  @Output() onRequestNodes: EventEmitter<ITreeComponent> = new EventEmitter<ITreeComponent>();      // Запрос узлов при необходимости

  constructor(private treeService: TreeService,  public modalService: NgbModal){
    console.log("const");
    this.SendTrees = [];
    // this.treeService.GetRoot().subscribe(
    //   res => this.SendTrees = res as ITreeComponent[],
    //   error => console.log(error)
    // );
    //console.log(this.SendTrees);
    this.SelectedNode = {} as ITreeComponent;
  }


  public test = {
    name: 'test',
    age: 1
  }

  closeResult = '';

  openModal() {
    const modalRef = this.modalService.open(NodeContentComponent);
    modalRef.componentInstance.SelectedNode = this.test;
    modalRef.result.then((result) => {
      if (result) {
        console.log(result);

      }
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });

    // modalRef.componentInstance.passEntry.subscribe((receivedEntry) => {
    //   console.log(receivedEntry);
    // })
  }


  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }

  onSelectNode(node: ITreeComponent) {
    //console.log("onSelectNode1");
    this.onSelectedChanged.emit(node);
    //console.log(node);
  }

  onExpand(node: ITreeComponent) {
    console.log("onExpand");
    if(!node.leaf)
    {
        node.isExpanded = !node.isExpanded;
        if (node.isExpanded && (!node.trees || node.trees.length === 0)) {
          this.onRequestNodes.emit(node);
        }
        else
        {
          console.log("else");
          this.treeService.GetNodes(node.value).subscribe(
            res => {node.trees = res as ITreeComponent[], console.log(res)},
            error=> console.log(error));
        }
    }
  }

  onRequestLocal(node: ITreeComponent) {
    console.log("onRequestLocal");
    this.onRequestNodes.emit(node);
  }

}

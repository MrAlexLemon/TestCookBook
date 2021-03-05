import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-node-content',
  templateUrl: './node-content.component.html',
  styleUrls: ['./node-content.component.scss']
})
export class NodeContentComponent implements OnInit {

  @Input() public SelectedNode: any;
  @Output() passEntry: EventEmitter<any> = new EventEmitter();

  constructor(
    public activeModal: NgbActiveModal
  ) { }

  ngOnInit() {
    console.log(this.SelectedNode);
  }

  passBack() {
    this.passEntry.emit(this.SelectedNode);
    this.activeModal.close(this.SelectedNode);
  }

}

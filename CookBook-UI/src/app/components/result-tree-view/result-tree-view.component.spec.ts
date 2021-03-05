import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResultTreeViewComponent } from './result-tree-view.component';

describe('ResultTreeViewComponent', () => {
  let component: ResultTreeViewComponent;
  let fixture: ComponentFixture<ResultTreeViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ResultTreeViewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ResultTreeViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

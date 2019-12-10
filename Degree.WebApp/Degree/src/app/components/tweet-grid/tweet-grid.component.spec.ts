import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TweetGridComponent } from './tweet-grid.component';

describe('TweetListComponent', () => {
  let component: TweetGridComponent;
  let fixture: ComponentFixture<TweetGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TweetGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TweetGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

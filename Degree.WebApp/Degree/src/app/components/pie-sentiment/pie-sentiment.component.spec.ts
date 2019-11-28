import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PieSentimentComponent } from './pie-sentiment.component';

describe('PieSentimentComponent', () => {
  let component: PieSentimentComponent;
  let fixture: ComponentFixture<PieSentimentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PieSentimentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PieSentimentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

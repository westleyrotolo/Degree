import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RadialSentimentComponent } from './radial-sentiment.component';

describe('RadialSentimentComponent', () => {
  let component: RadialSentimentComponent;
  let fixture: ComponentFixture<RadialSentimentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RadialSentimentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RadialSentimentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

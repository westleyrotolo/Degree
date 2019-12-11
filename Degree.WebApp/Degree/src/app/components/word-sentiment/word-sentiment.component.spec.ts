import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WordSentimentComponent } from './word-sentiment.component';

describe('WordSentimentComponent', () => {
  let component: WordSentimentComponent;
  let fixture: ComponentFixture<WordSentimentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WordSentimentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WordSentimentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

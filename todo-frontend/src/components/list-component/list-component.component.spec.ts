import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListComponentComponent } from './list-component.component';

describe('ListComponentComponent', () => {
  let component: ListComponentComponent;
  let fixture: ComponentFixture<ListComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListComponentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ListComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

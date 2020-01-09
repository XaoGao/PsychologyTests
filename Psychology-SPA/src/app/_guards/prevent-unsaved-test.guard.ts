import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { TestComponent } from '../tests/test/test.component';

@Injectable()
export class PreventUnsavedTestGuard implements CanDeactivate<TestComponent> {
  canDeactivate(component: TestComponent) {
    if (component.testForm.dirty) {
      return confirm('Вы точно хотите закончить тестирование преждеверменно?');
    }
    return true;
  }
}

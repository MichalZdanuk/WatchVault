import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-error-message',
  imports: [],
  templateUrl: './error-message.html',
  styleUrl: './error-message.css',
})
export class ErrorMessage {
  @Input() message: string = 'Something went wrong';
  @Input() retryText: string = 'Retry';
  @Output() retry = new EventEmitter<void>();
}

import { Component, OnInit } from '@angular/core';
import { NzMessageDataOptions, NzMessageService } from 'ng-zorro-antd/message';

@Component({
  template:''
})
export class BaseComponent  {
  private options: NzMessageDataOptions = {nzDuration: 5000};
  constructor(
    private message?: NzMessageService,
  ) { }


  public showMessageError(message: string, options?: NzMessageDataOptions) {
    this.message!.create('error', message, options ?? this.options);
  }

  public showMessageSuccess(message: string, options?: NzMessageDataOptions) {
    this.message!.create('success', message, options ?? this.options);
  }


}

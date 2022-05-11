import { Injectable } from '@angular/core';
declare let alertify: any;

@Injectable({
  providedIn: 'root'
})
export class AlertifyService {

  confirm(message: string ) {
    alertify.confirm(message,
  function(){
    alertify.success('Ok');
  },
  function(){
    alertify.error('Cancel');
  });
  }
 //https://alertifyjs.com/prompt.html
  prompt(title: string, message: string, defvalue: string)
  {
    // alertify.prompt().set(message, title).show();
    //alertify.dialog('prompt').set({transition: 'zoom', message}).show();
    //alertify.prompt('Input (color):').set('type', 'color'); 
    //alertify.prompt('Input (date):').set('type', 'date'); 
    //alertify.prompt('Input (datetime-local):').set('type', 'datetime-local'); 
   // alertify.prompt('Input (number):').set('type', 'number'); 
    //alertify.prompt('Input (password):').set('type', 'password'); 
    alertify.prompt( 'Prompt Title', 'Prompt Message', 'Prompt Value'
    , function(evt: Event,value:string) { alertify.success('You entered: ' + value) }
    , function() { alertify.error('Cancel') });

  }

  success(message: string) {
    alertify.success(message);
  }

  error(message: string) {
    alertify.error(message);
  }

  warning(message: string) {
    alertify.warning(message);
  }

  message(message: string) {
    alertify.message(message);
  }
  
}

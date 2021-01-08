'use strict';
export default class Servicio{
    constructor(){
        
    }
    http(_request) {
        return new Promise(function(resolve, reject) {
            fetch(_request)
           .then(response => {
               if (response.status === 200) {
                 return response.json();
               } else {
                 throw new Error('API SERVER ERROR!');
               }
           })
           .then(data => {
               resolve(data);
           }).catch(error => {
             reject(error);
           });
        });
    }
}
'use strict';
import Servicio from "./Servicio.js";
export default class Producto extends Servicio{
    
    constructor(){
        super();
    }
    host='https://localhost:44335/';
    getPersons(){
        var myRequest = new Request(this.host+'api/PersonController/GetPerson/0', {method: 'GET', body: null});
        return new Promise(function(resolve, reject) {
            this.http(myRequest).then((response) => {
                 resolve(response);
             }).catch(error => {
                reject(error);
             });
        }.bind(this));
    }

    agregarPersona(person){
        var myRequest = new Request(this.host+'api/PersonController/savePerson', {method: 'POST',headers: {'Content-Type': 'application/json'}, body: JSON.stringify(person)});
        return new Promise(function(resolve, reject) {
            this.http(myRequest).then((response) => {
                 resolve(response);
             }).catch(error => {
                reject(error);
             });    
        }.bind(this));
    }
    getGender(){
        var myRequest = new Request(this.host+'api/PersonController/getGender', {method: 'GET', body: null});
        return new Promise(function(resolve, reject) {
            this.http(myRequest).then((response) => {
                 resolve(response);
             }).catch(error => {
                reject(error);
             });
        }.bind(this));
    }

}
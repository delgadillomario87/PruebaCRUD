'use strict';
import Person from "./Person.js";

var oArrayPerson, oPerson, tblPerson, btnGuardar, btnEliminar, lblName, lblLastName,selectGende;

OnInit();

function OnInit() {
    oArrayPerson = "";
    oPerson = new Person();
    tblPerson = document.getElementById('tblPerson');
    btnGuardar = document.getElementById("btnSavePerson");
    btnEliminar = document.getElementById("btnEliminar");
    selectGende = document.getElementById("selectGender");
    lblName = document.getElementById("lblName");
    lblName.value = "";
    lblLastName = document.getElementById("lblLastname");
    lblLastName.value = "";
    oPerson.getPersons().then((response) => {
        response.response.forEach(elem => {
            oArrayPerson += `<tr data-Person='${JSON.stringify(elem)}'><td>${elem.name}</td><td>${elem.lastName}</td><td>${elem.description}</td><td><button type="button" class="btn btn-success" vertical-align: middle data-toggle="modal" data-target="#exampleModal"><i class="fas fa-edit">Editar</i></button></td> </tr>`;
        });
        tblPerson.innerHTML = oArrayPerson;

        $(document).ready(function () {
            $('#tablaPerson').DataTable();
        });
    }).catch(error => {

    });

    oPerson.getGender().then((response) => {
        response.response.forEach(elem => {
            var option=document.createElement("option");
            option.text=elem.description;
            option.id=elem.genderId;
            selectGende.add(option);
        });      
    }).catch(error => {

    });
}


btnGuardar.addEventListener("click", function (event) {

    let persona = {
        "personId": 0,
        "name": lblName.value,
        "lastName": lblLastName.value,
        "genderId": selectGende[selectGende.selectedIndex].id
    }
    oPerson.agregarPersona(persona).then((response) => {

    }).catch(error => {

    });

    $('#exampleModal').modal('hide');
    OnInit();
});



// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


var searchInp = document.getElementById("searchInp");
searchInp.addEventListener("keyup", function () {
   
    // Creating Our XMLHttpRequest object 
    let xhr = new XMLHttpRequest();

    // Making our connection  
    let url = `https://localhost:44341/Employee/Index?${searchInp.value}`;
    xhr.open("POST", url, true);

    // function execute after request is successful 
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            console.log(this.response);
        }
    }
    // Sending our request 
    xhr.send();
})


// Initialization for ES Users
import { Ripple, initMDB } from "mdb-ui-kit";

initMDB({ Ripple });

// Initialization for ES Users
import { Input, initMDB } from "mdb-ui-kit";

initMDB({ Input });

// Initialization for ES Users
import { Modal, Ripple, initMDB } from "mdb-ui-kit";

initMDB({ Modal, Ripple });
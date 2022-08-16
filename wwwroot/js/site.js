// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function CargarRequisitos(event) {
    $('.modal').modal('show');

    event.preventDefault();

    fetch('https://localhost:5001/Perritos/Requisitos')
    .then(response => response.text())
    .then(html => {
        document.getElementById("div_requisito").innerHTML = html;
    });
}

$(document).ready(function() {
    $(".toast").toast('show');
});

function deletePerrito(id) {
    Swal.fire({
        title: '¡Advertencia!',
        text: '¿Esta seguro de querer eliminar este perrito?',
        icon: 'warning',
        showDenyButton: true,
        confirmButtonText: 'Eliminar',  
        denyButtonText: 'Cancelar',
    }).then((result) => {  
        /* Read more about isConfirmed, isDenied below */  
        if (result.isConfirmed) {    
            window.location.href = "https://localhost:5001/Perritos/Delete/" + id;
        } 
    });
}

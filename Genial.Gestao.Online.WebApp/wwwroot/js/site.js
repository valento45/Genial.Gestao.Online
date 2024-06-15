$(document).ready(function () {

    applyMascaras();

});


function applyMascaras() {
    $(".celular").mask('(00) 00000-0000');
    $(".cpf").mask('000.000.000-00');
    $(".phone").mask('(00) 0000-0000');

}
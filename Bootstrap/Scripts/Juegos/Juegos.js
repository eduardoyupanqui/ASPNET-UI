//$("#btnNuevo").click(function (evt) {
//    $("#modal-content").load("/juegos/Agregar");
//});

$("#btnNuevo").click(function (evt) {

    $("#modal-content").load("/juegos/agregar", function () {
        setTimeout(function () { $('.text-box:first').focus() }, 1000);
    });
});

$(".btnVistaPrevia").click(function (evt) {

    $("#modal-content").load("/juegos/vistaprevia/" + $(this).data("id"));

});

$(".btnEditar").click(function (evt) {

    $("#modal-content").load("/juegos/editar/" + $(this).data("id"), function () {

        setTimeout(function () { $('.text-box:first').focus() }, 1000);

    });

});

$(".btnEliminar").click(function (evt) {

    $("#modal-content").load("/juegos/eliminar/" + $(this).data("id"));

});

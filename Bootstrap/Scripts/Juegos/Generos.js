$("#btnNuevo").click(function (evt) {
    $("#modal-content").load("/generos/Agregar");
});

$(".btnEditar").click(function (evt) {
    $("#modal-content").load("/generos/Editar/" + $(this).data("id"));
});

$(".btnEliminar").click(function (evt) {
    $("#modal-content").load("/generos/Eliminar/" + $(this).data("id"));
});
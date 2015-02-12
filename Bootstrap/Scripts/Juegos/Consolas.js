$("#btnNuevo").click(function (evt) {
    $("#modal-content").load("/consolas/Agregar");
});

$(".btnEditar").click(function (evt) {
    $("#modal-content").load("/consolas/Editar/"+$(this).data("id"));
});

$(".btnEliminar").click(function (evt) {
    $("#modal-content").load("/consolas/Eliminar/" + $(this).data("id"));
});
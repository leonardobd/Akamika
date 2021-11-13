
$('button[id=btn_Eliminar]').on('click', function () {
    var dato = $(this).data("id");
    $('#Id').val(dato);
})

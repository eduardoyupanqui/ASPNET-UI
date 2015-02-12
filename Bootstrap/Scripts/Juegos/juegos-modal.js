$('#btnSalvar').click(function () {
    var error = 0;
    $(':input[data-val-required]', '#JuegosForm').each(function () {
        if (!$(this).is('input:checkbox'))
        {
            $(this).valid();
            if ($(this).val() == '')
            {
                if (error == 0)
                {
                    var tab = $(this).closest('.tab-pane').attr('id');
                    $('#MyTabs a[href="#' + tab + '"]').tab('show');
                    $(this).focus();
                }
                error = 1;
                
            }
        }
    });


    if (error == 1) {
        return false;
    } else {
        $('#JuegosForm').submit();
    }
});
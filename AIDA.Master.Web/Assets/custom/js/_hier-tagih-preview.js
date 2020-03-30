jQuery(document).ready(function () {
    var oTable = $('.datatable').DataTable({
        'iDisplayLength': 10,
        'lengthMenu': [[10, 50, 100, -1], [10, 50, 100, 'All']]
    });

    $formConfirmation = $('#form-confirmation');

    $('#btn-submit-preview').on('click', function () {
        $('#modal-confirmation').modal({
            backdrop: 'static',
            keyboard: false
        });
    });

    $formConfirmation.submit(function () { 
        //$formConfirmation.find('.modal-footer .btn[type="submit"]').addClass('loading');
        $('#loading-indicator').show();
        $formConfirmation.find('.modal-footer .btn').attr('disabled', 'disabled');

        $('#btn-submit-preview').addClass('loading').attr('disabled', 'disabled');
    });
});
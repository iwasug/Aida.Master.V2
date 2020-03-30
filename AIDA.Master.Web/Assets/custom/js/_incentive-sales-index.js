jQuery(document).ready(function () {
    $('.datepicker').datepicker({
        format: 'mm-yyyy',
        viewMode: 'months',
        minViewMode: 'months',
        autoclose: true
    });

    $('.select2').select2();

    var oTable = $('.datatable').DataTable({
        'iDisplayLength': 10,
        'lengthMenu': [[10, 50, 100, -1], [10, 50, 100, 'All']]
    });

    jQuery.validator.setDefaults({
        debug: true,
        success: "valid"
    });

    $form = $('#main-form');

    $form.validate({
        errorElement: 'span', //default input error message container
        errorClass: 'help-block help-block-error', // default input error message class
        focusInvalid: false, // do not focus the last invalid input
        ignore: "",  // validate all fields including form hidden input
        rules: {
            FormattedMonthYear: {
                required: true
            }
        },
        messages: {},
        invalidHandler: function (event, validator) { //display error alert on form submit
            //Metronic.scrollTo($form, -200);
        },
        errorPlacement: function (error, element) {
            if ($(element).attr('name') === 'FormattedMonthYear') {
                error.insertAfter($(element).closest('.input-group'));
            }
            else {
                error.insertAfter($(element));
            }
        },
        highlight: function (element) { // hightlight error inputs
            var fieldName = $(element).attr('name');

            $(element).closest('.form-group').addClass('has-error'); // set error class to the control group
        },

        unhighlight: function (element) { // revert the change done by hightlight
            $(element).closest('.has-error').removeClass('has-error'); // set error class to the control group
        },

        success: function (label) {
            label.closest('.has-error').removeClass('has-error'); // set success class to the control group
        },

        submitHandler: function (form) {
            //$(form).find('.btn[type="submit"]').addClass('loading');
            $('#loading-indicator').show();
            $(form).find('.btn').attr('disabled', 'disabled');
            form.submit();
        }
    });

    var p = $form.find('input[name="p"]').val();
    var plant = $form.find('input[name="b"]').val();

    $form.find('input[name="p"]').on('change', function () {
        if ($(this).val() !== p) {
            p = $(this).val();
            $form.find('input[name="p"]').val(p);
            $form.trigger('submit');
            $(this).attr('disabled', 'disabled');
        }
    });

    $form.find('select[name="b"]').on('change', function () {
        $('#form-export').find('input[name="b"]').val($(this).val());
    });

    $form.find('input[name="t"]').on('change', function () {
        $('#form-export').find('input[name="t"]').val($(this).val());
    });

    $('#btn-export-excel').on('click', function () {
        $('#form-export').trigger('submit');
    });
});
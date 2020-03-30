jQuery(document).ready(function () {
    $('.datepicker').datepicker({
        format: 'mm-yyyy',
        viewMode: 'months',
        minViewMode: 'months',
        autoclose: true
    });

    $formImport = $('#main-form');

    $formImport.validate({
        errorElement: 'span', //default input error message container
        errorClass: 'help-block help-block-error', // default input error message class
        focusInvalid: false, // do not focus the last invalid input
        ignore: "",  // validate all fields including form hidden input
        rules: {
            InputFile: {
                required: true,
                extension: 'xls|xlsx'
            },
            Periode: {
                required: true
            }
        },
        messages: {},
        invalidHandler: function (event, validator) { //display error alert on form submit
            //Metronic.scrollTo($form, -200);
        },
        errorPlacement: function (error, element) {
            if ($(element).attr('type') == 'checkbox') {
                error.insertAfter($(element).closest('.checkbox-list'));
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
            $(form).find('.form-actions .btn[type="submit"]').addClass('loading');
            $(form).find('.form-actions .btn').attr('disabled', 'disabled');
            form.submit();
        }
    });

    var p = $form.find('input[name="Periode"]').val();

    $form.find('input[name="Periode"]').on('change', function () {
        if ($(this).val() != p) {
            p = $(this).val();
            $form.find('input[name="Periode"]').val(p);
            $form.trigger('submit');
            $(this).attr('disabled', 'disabled');
        }
    });
});
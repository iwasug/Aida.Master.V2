jQuery(document).ready(function () {
    var arrData = [];
    var defaultValidTo = new Date();
    defaultValidTo.setDate(defaultValidTo.getDate() + 1);

    function formatDate(date) {
        var d = new Date(date),
            month = '' + (d.getMonth() + 1),
            day = '' + d.getDate(),
            year = d.getFullYear();

        if (month.length < 2) month = '0' + month;
        if (day.length < 2) day = '0' + day;

        return [year, month, day].join('-');
    }

    var oTable = $('.datatable').DataTable({
        //'scrollX': true,
        'bProcessing': true,
        'bServerSide': true,
        'autoWidth': false,
        'order': [[0, 'asc']],
        'iDisplayLength': 10,
        'lengthMenu': [[10, 50, 100, -1], [10, 50, 100, 'All']],
        'language': {
            'processing': 'loading...'
        },
        'destroy': true,
        'dom': "<'row'<'col-sm-4 clearfix'l><'col-sm-4 text-center'B><'col-sm-4'f>>" +
            "<'row'<'col-md-12'tr>>" +
            "<'row'<'col-md-5'i><'col-md-7'p>>",
        buttons: [
            {
                text: '<i class="fa fa-pencil"></i> Update',
                attr: {
                    id: 'btn-update'
                },
                className: 'btn blue',
                action: function (e, dt, node, config) {
                    updateAccess();
                }
            }
        ],
        'ajax': {
            'url': baseUrl + 'Supervisor/Datatable',
            'type': 'post',
            'dataType': 'json',
            'data': function (d) {
                return {
                    Keyword: d.search.value,
                    Length: d.length,
                    Start: d.start,
                    IndexOrderCol: d.order[0].column,
                    OrderType: d.order[0].dir
                };
            },
            'error': function (e) {
                alert('terjadi kesalaha sistem!');
            }
        },
        aoColumns: [
            {
                'mData': function (s) {
                    return '';
                }, 'bSortable': false, 'sClass': 'text-right'
            },
            {
                'mData': function (s) {
                    return '<a href="' + baseUrl + 'Supervisor/Edit/' + s.nik + '" class="btn btn-xs default green-stripe">edit</a >';
                }, 'bSortable': false, 'sClass': 'text-left'
            },
            {
                'mData': function (s) {
                    var c = '';

                    if (s.is_able_to_upload) {
                        c = ' checked';
                    }

                    return '<input type="checkbox" class="checkbox" name="arrData" value="'+s.nik+'"'+c+'/>';
                }, 'bSortable': false, 'sClass': 'text-center'
            },
            {
                'mData': function (s) {
                    var dis = '';

                    if (!s.is_able_to_upload) {
                        dis = ' disabled';
                    }

                    return '<input type="text" class="form-control input-sm date-picker" name="UploadValidTo" value="' + s.f_upload_valid_to + '"' + dis + '/>';
                }, 'bSortable': false, 'sClass': 'text-center'
            },
            { 'mData': 'nik', 'sClass': 'text-center' },
            { 'mData': 'fullname', 'sClass': 'text-left' },
            { 'mData': 'default_rayon_type', 'sClass': 'text-center' },
            {
                'mData': function (s) {
                    if (s.is_role) {
                        return '<i class="fa fa-check font-green"></i>';
                    }
                    else {
                        return '<i class="fa fa-times font-red"></i>';
                    }
                }, 'sClass': 'text-center'
            },
            { 'mData': 'f_valid_from', 'sClass': 'text-center' },
            { 'mData': 'f_valid_to', 'sClass': 'text-center' },
        ],
        rowCallback: function (row, data, index) {
            var dtInfo = oTable.page.info();

            var arrTd = $(row).children();
            $(arrTd[0]).html(dtInfo.start + index + 1);

            return row;
        },
        drawCallback: function (settings) {
            $('.datatable').find('.checkbox').uniform();

            $('.date-picker').datepicker({
                autoclose: true,
                format: 'yyyy-mm-dd'
            }).on('changeDate', function (e) {
            });

            arrData = [];

            $('.datatable').find('input[name="arrData"]').each(function () {
                if (this.checked) {
                    var dt = $(this).closest('tr').find('input[name="UploadValidTo"]').val();

                    arrData.push({
                        NIK: $(this).val(),
                        IsAllowed: true,
                        UploadValidTo: dt
                    });
                }
                else {
                    arrData.push({
                        NIK: $(this).val(),
                        IsAllowed: false,
                        UploadValidTo: ''
                    });
                }
            });
        }
    });

    $('.datatable').on('change', 'input[name="arrData"]', function () {
        for (i = 0; i < arrData.length; i++) {
            if (arrData[i].NIK == $(this).val()) {
                if (this.checked) {
                    $(this).closest('tr').find('input[name="UploadValidTo"]').removeAttr('disabled');
                    $(this).closest('tr').find('input[name="UploadValidTo"]').val(formatDate(defaultValidTo));

                    arrData[i].IsAllowed = true;
                    arrData[i].UploadValidTo = formatDate(defaultValidTo);
                }
                else {
                    $(this).closest('tr').find('input[name="UploadValidTo"]').val('');
                    $(this).closest('tr').find('input[name="UploadValidTo"]').attr('disabled', 'disabled');

                    arrData[i].IsAllowed = false;
                    arrData[i].UploadValidTo = '';
                }

                $(this).closest('tr').find('.date-picker').datepicker('update');

                break;
            }
        }
    });

    function updateAccess() {
        if (arrData.length == 0) {
            alert('pilih row terlebih dahulu');
            return false;
        }

        arrData = [];

        $('.datatable').find('input[name="arrData"]').each(function () {
            if (this.checked) {
                var dt = $(this).closest('tr').find('input[name="UploadValidTo"]').val();

                arrData.push({
                    NIK: $(this).val(),
                    IsAllowed: true,
                    UploadValidTo: dt
                });
            }
            else {
                arrData.push({
                    NIK: $(this).val(),
                    IsAllowed: false,
                    UploadValidTo: ''
                });
            }
        });

        $('#btn-update').addClass('loading');
        $('#tbl-wrapper').find('.dt-buttons button').attr('disabled', 'disabled');

        $.ajax({
            type: "POST",
            url: baseUrl + 'Supervisor/UpdateAccess',
            data: {
                ListData: arrData,
                __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
            },
            dataType: 'json'
        }).done(function (resp) {
            if (resp.data != null && resp.data.length > 0) {
                for (i = 0; i < resp.data.length; i++) {
                    var $arrData = $('.datatable').find('input[name="arrData"][value="' + resp.data[i].nik + '"]');
                    $arrData.prop("checked", resp.data[i].isAllowed);
                    $arrData.closest('tr').find('input[name="UploadValidTo"]').val(resp.data[i].formattedUploadValidTo);

                    if (resp.data[i].isAllowed) {
                        $arrData.closest('tr').find('input[name="UploadValidTo"]').removeAttr('disabled');
                    }
                    else {
                        $arrData.closest('tr').find('input[name="UploadValidTo"]').attr('disabled', 'disabled');
                    }
                }

                $.uniform.update();
            }

            if (resp.status == 1) {
                alert('update berhasil');
            }
            else {
                alert('update gagal');
            }

            $('#tbl-wrapper').find('.dt-buttons button').removeAttr('disabled')
            $('#btn-update').removeClass('loading');
        }).fail(function () {
            alert('terjadi kesalahan');

            $('#tbl-wrapper').find('.dt-buttons button').removeAttr('disabled')
            $('#btn-update').removeClass('loading');
        });
    }
});
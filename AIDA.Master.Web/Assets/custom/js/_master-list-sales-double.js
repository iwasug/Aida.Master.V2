jQuery(document).ready(function () {

    var arrDeleteId = [];

    var oTable;

    oTable = initDatatable();

    function initDatatable() {
        //if ($.fn.DataTable.isDataTable('.datatable')) {
        //    $('.datatable').DataTable().destroy();
        //}

        return $('.datatable').DataTable({
            'iDisplayLength': 10,
            'lengthMenu': [[10, 50, 100, -1], [10, 50, 100, 'All']],
            'destroy': true,
            'dom': "<'row'<'col-sm-4 clearfix'l><'col-sm-4 text-center'B><'col-sm-4'f>>" +
                "<'row'<'col-md-12'tr>>" +
                "<'row'<'col-md-5'i><'col-md-7'p>>",
            buttons: [
                {
                    text: '<i class="fa fa-trash-o"></i> Delete',
                    attr: {
                        id: 'btn-delete-rows'
                    },
                    className: 'btn red',
                    action: function (e, dt, node, config) {
                        clearDouble();
                    }
                },
                {
                    extend: 'copy', className: 'btn', text: '<i class="fa fa-copy"></i> Copy'
                },
                { extend: 'csv', className: 'btn', text: '<i class="fa fa-download"></i> CSV' }
                //{ extend: 'excel', className: 'btn' }
            ],
            rowCallback: function (row, data, index) {
                var api = this.api();
                var dtInfo = api.page.info();

                var arrTd = $(row).children();
                $(arrTd[0]).html(dtInfo.start + index + 1);

                return row;
            }
        });
    }

    $('input[name="Ids"]').change(function () {
        if (this.checked) {
            arrDeleteId.push($(this).val());
        }
        else {
            arrDeleteId.remove($(this).val());
        }
    });

    function clearDouble() {
        if (arrDeleteId.length == 0) {
            alert('pilih row terlebih dahulu');
            return false;s
        }

        $('#btn-delete-rows').addClass('loading');
        $('.portlet-body').find('.dt-buttons button').attr('disabled', 'disabled');

        $.ajax({
            type: "POST",
            url: baseUrl + 'MasterListSales/ClearDouble',
            data: {
                FormattedValidDate: monthYear,
                ListId: arrDeleteId
            },
            dataType: 'json'
        }).done(function (resp) {
            if (resp.data != null && resp.data.length > 0) {
                for (i = 0; i < resp.data.length; i++) {
                    oTable.row($('#tbl-double tbody').find('tr[data-id="' + resp.data[i] + '"]')).remove().draw();
                }

                alert('berhasil hapus ' + resp.data.length + ' baris data');
            }

            $('.portlet-body').find('.dt-buttons button').removeAttr('disabled')
            $('#btn-delete-rows').removeClass('loading');
        }).fail(function () {
            alert('terjadi kesalahan sistem');

            $('.portlet-body').find('.dt-buttons button').removeAttr('disabled')
            $('#btn-delete-rows').removeClass('loading');
        });
    }

    
});
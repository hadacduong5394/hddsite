var config = {
    id: null
}

var Role = {
    init: function () {
        Role.events();
    },
    events: function () {
        $('#BtnCreateNew').off('click').on('click', function () {
            location.href = "/Role/CreateNew";
        });

        $('.EditItem').off('click').on('click', function () {
            var id = $(this).data('id');
            location.href = "/Role/Update?id=" + id;
        });

        $('.DeleteItem').off('click').on('click', function () {
            config.id = $(this).data('id');
            $('#modalConfirmDelete').show();
        });

        $('#btnOkDelete').off('click').on('click', function (e) {
            $('#modalConfirmDelete').hide();
            var id = config.id;
            config.id = null;
            $('#frm_' + id).submit();
        });

        $('.viewDetail').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            $.ajax({
                url: "/Role/ViewDetail",
                data: { id: id },
                type: "post",
                dataType: "json",
                success: function (result) {
                    $('.lblName').text(result.roleName);
                    $('.lblCreateBy').text(result.createBy);
                    $('.lblCreateDate').text(result.createDate);
                    $('.lblUpdateBy').text(result.updateBy);
                    $('.lblUpdateDate').text(result.updateDate);
                    $('.lblDescreption').text(result.descreption);
                    $('.lblStatus').text(result.status);

                    $('#modalViewDetail').modal();
                }
            });
        });
    }
}
Role.init();
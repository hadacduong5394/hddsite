var config = {
    id: null
}

var User = {
    init: function () {
        User.events();
    },
    events: function () {
        $('#BtnCreateNew').off('click').on('click', function (e) {
            location.href = "/User/CreateNew";
        });

        $('.EditItem').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            location.href = '/User/Update?id=' + id;
        });

        $('.DeleteItem').off('click').on('click', function (e) {
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
                url: "/User/ViewDetail",
                data: { id: id },
                type: "post",
                dataType: "json",
                success: function (result) {
                    $('.lblUserName').text(result.userName);
                    $('.lblFullName').text(result.fullName);
                    $('.lblEmail').text(result.email);
                    $('.lblCreateBy').text(result.createBy);
                    $('.lblCreateDate').text(result.createDate);
                    $('.lblUpdateBy').text(result.updateBy);
                    $('.lblUpdateDate').text(result.updateDate);
                    $('.lblStatus').text(result.status);

                    $('#modalViewDetail').modal();
                }
            });
        });
    }
}
User.init();
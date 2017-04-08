var LogOut = {
    init: function () {
        LogOut.events();
    },
    events: function () {
        $('#frmLogOff').on('click', function () {
            $('#frmLogOff').submit();
        });
    }
}
LogOut.init();
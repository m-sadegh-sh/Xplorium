jQuery.search = function (path) {
    $('input#q').focus();
    $('input#q').autocomplete({
        source: path,
        minLength: 0,
        delay: 1000
    });
    $('form').validate({
        onfocusout: false,
        onkeyup: false,
        rules: { q: { required: true} },
        highlight: function (element, errorClass) {
            $(element).effect("highlight", { color: '#F3CFCF' }, 2000);
        },
        errorPlacement: function (error, element) {
            return null;
        }
    });
};
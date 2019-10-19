jQuery.search = function (path) {
    $('input#q').focus();
    jQuery.validator.addMethod('at-least-one', function (val, el) {
        return $(el).parents().find('.at-least-one:filled').length;
    });
    $('form').validate({
        onfocusout: false,
        onkeyup: false,
        rules: { 'at-least-one': { required: true} },
        highlight: function (element, errorClass) {
            $(element).effect("highlight", { color: '#F3CFCF' }, 2000);
        },
        errorPlacement: function (error, element) {
            return null;
        }
    });
    $('form').submit(function () {
        //$('input[type="text"]').remove();
        $(this).serialize();
        //return false;
    });
    $('input[type="text"]').change(function () {
        getQuery();
    });
    getQuery = function () {
        $.ajax({
            url: path + '?q=' + $('input#q').val() + '&none=' + $('input#none').val(),
            success: function (data) {
                if (data == '') {
                    $('h3#result').html('Use the form below and your advanced search will appear here');
                }
                else {
                    $('h3#result').html('Your formatted query is: <strong>' + data "</strong>');
                }
            },
            failed: function (data) {
                $('h3#result').html('We\'re sorry but we can\'t construct final query.');
            }
        });
    };
};

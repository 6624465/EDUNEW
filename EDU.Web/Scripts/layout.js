$(function () {
    $("input ,select, textarea,hidden, a").not($(":button,a")).keypress(function (evt) {
        if (evt.keyCode == 13) {
            evt.preventDefault();
            return false;
        }
    });

    $("#txtSearch").keypress(function (evt) {
        if (evt.keyCode == 13) {
            $("#btnSearch").click();
            window.location = $('#btnSearch').attr('href');
        }
    });

    /* JS Utility Functions */
    //$(document).on('keypress', '.numOnlyCss', function (evt) {
    //    var text = $('#txtChequeNo').val();
    //    evt = (evt) ? evt : window.event;
    //    var charCode = (evt.which) ? evt.which : evt.keyCode;
    //    if (text.length >= 10) {
    //        if (charCode == 8 || charCode == 9)
    //            return true;
    //        return false;
    //    }

    //    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
    //        return false;
    //    }
    //    return true;
    //});

    $(document).on('keypress', '.dtCss', function (evt) {
        return false;
    });

    $(document).on('keypress', '.alphaNumCss', function (evt) {
        evt = (evt) ? evt : window.event;
        var charCode = parseInt((evt.which) ? evt.which : evt.keyCode);
        if ((charCode >= 48 && charCode <= 57) || (charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122))
            return true;
        else
            return false;
    });

    $(document).on('keypress', '.decimalCss', function (evt) {
        //
        //alert(evt.keyCode);
        //alert(evt.charCode);

        //var charCode = (evt.which) ? evt.which : (evt.charCode);
        //if (charCode == 8 || charCode == 0) {

        //}
        //else if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        //    alert(charCode);
        //    return false;
        //}

        //
        //var character = String.fromCharCode(evt.charCode)
        //var newValue = this.value + character;                
        //if (isNaN(newValue) || parseFloat(newValue) * 10000 % 1 > 0) {
        //    evt.preventDefault();
        //    return false;
        //}

        //return true;
        /*
       var charCode = (evt.which) ? evt.which : event.keyCode;
       if (charCode != 46 && charCode > 31
         && (charCode < 48 || charCode > 57))
           return false;

       return true;*/

        if (navigator.appName == "Netscape")
            Key = evt.charCode; //or e.which; (standard method)
        else
            Key = evt.keyCode;
        var character = String.fromCharCode(Key);
        //console.log(Key);

        if (Key == 0)
            return true;

        var newValue = this.value + character;
        if (isNaN(newValue) || parseFloat(newValue) * 10000 % 1 > 0) {
            evt.preventDefault();
            return false;
        }

    });

    $(document).on('keypress', '.numCss', function (evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if ((charCode >= 48 && charCode <= 57) || charCode == 8)
            return true;
        else
            return false;
    });

    $.ajaxSetup({
        error: function (x, e) {
            if (x.status == 401) {
                location.href = ErrorUrl;
            }
        }
    });

    $('input').iCheck({
        checkboxClass: 'icheckbox_flat',
        radioClass: 'iradio_flat',
        increaseArea: '20%' // optional
    });

    /* JS Utility Functions */
});
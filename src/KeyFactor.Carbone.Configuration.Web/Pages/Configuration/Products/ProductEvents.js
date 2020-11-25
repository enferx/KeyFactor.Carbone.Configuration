$(function () {
    failure = (a) => {
        var error = a.responseJSON.error;
        var validator = $("#form").validate();
        if (error.code == "Configuration:000001") {
            validator.showErrors({ "Product.Number": error.message });
        }
    },
    success = (a) => {
        var errors = a.responseJSON.errors;
        if (errors) {
            var validator = $("#form").validate();
            for (var i = 0; i < errors.length; i++) {
                for (var j = 0; j < errors[i].memberNames.length; j++) {
                    validator.showErrors({ errors[i].memberNames[j] : errors[i].message);
                }
                //if (errors[i].code == "Configuration:000001") {
                //    validator.showErrors({ "Product.Number": error.message });
                //}
            }
            
        }
    }

    //$('#save').click(function (e) {
    //    e.preventDefault();
    //    var formData = $('#editForm').serialize();
    //    abp.ajax({
    //        url: "/Configuration/Products/EditProduct", //recommended
    //        type: "POST",
    //        data: formData,
    //        contentType: 'application/x-www-form-urlencoded; charset=UTF-8'
    //    }).then(function (result) {
    //        console.log(result);
    //    }).catch(function (a,b,c) {
    //        alert("request failed :(");
    //    });
    //    //$.ajax({
    //    //    url: "/Configuration/Products/EditProduct", //recommended
    //    //    type: "POST",
    //    //    data: formData,
    //    //    contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
    //    //    success: function (a,b) {
    //    //        alert("success");
    //    //    },
    //    //    error: function (a, b, c) {
    //    //        alert("error");
    //    //    }
    //    //});
    //});
});
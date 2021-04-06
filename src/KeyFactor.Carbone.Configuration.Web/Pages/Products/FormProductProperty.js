let datatype = $('select[name="Input.DataType"]').val();
$("div[name='container_input_decimal']").hide();
$("div[name='container_input_double']").hide();
$("div[name='container_input_integer']").hide();
$("div[name='container_input_string']").hide();

$("div[name='container_input_" + datatype.toLowerCase() + "']").show();

$('select[name="Input.DataType"]').change(function () {
    let datatype = $(this).val();

    $("div[name='container_input_decimal'] input[type=text]").val(null);
    $("div[name='container_input_double'] input[type=text]").val(null);
    $("div[name='container_input_integer'] input[type=text]").val(null);
    $("div[name='container_input_string'] input[type=text]").val(null);

    $("div[name='container_input_decimal']").hide();
    $("div[name='container_input_double']").hide();
    $("div[name='container_input_integer']").hide();
    $("div[name='container_input_string']").hide();

    $("div[name='container_input_" + datatype.toLowerCase() + "']").show();
});

function success () {
    window.location.href = "/Products";
}

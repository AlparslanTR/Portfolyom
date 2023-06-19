$(function () {
    $("#contactForm").submit(function (event) {
        event.preventDefault();
        var $form = $(this);
        var $button = $form.find("#sendMessageButton");
        $button.prop("disabled", true);

        var name = $form.find("#name").val();
        var email = $form.find("#email").val();
        var subject = $form.find("#subject").val();
        var message = $form.find("#message").val();

        $.ajax({
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: {
                name: name,
                email: email,
                subject: subject,
                message: message
            },
            cache: false,
            success: function () {
                $('#success').html("<div class='alert alert-success'>");
                $('#success > .alert-success').html("<button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;")
                    .append("</button>");
                $('#success > .alert-success')
                    .append("<strong>Mesajýnýz gönderildi. </strong>");
                $('#success > .alert-success')
                    .append('</div>');
                $form.trigger("reset");
            },
            error: function () {
                $('#success').html("<div class='alert alert-danger'>");
                $('#success > .alert-danger').html("<button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;")
                    .append("</button>");
                $('#success > .alert-danger').append($("<strong>").text("Üzgünüm " + name + ", maðazamýzýn mail sunucusuna yanýt verilmiyor gibi görünüyor. Lütfen daha sonra tekrar deneyin!"));
                $('#success > .alert-danger').append('</div>');
                $form.trigger("reset");
            },
            complete: function () {
                setTimeout(function () {
                    $button.prop("disabled", false);
                }, 1000);
            }
        });
    });

    $("a[data-toggle=\"tab\"]").click(function (e) {
        e.preventDefault();
        $(this).tab("show");
    });

    $('#name').focus(function () {
        $('#success').html('');
    });
});
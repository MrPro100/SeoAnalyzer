$(document).ready(function ()
{
    $('form').addTriggersToJqueryValidate().triggerElementValidationsOnFormValidation();

    $('form').formValidation(function (element, result)
    {       
        $(".input-group").addClass("has-error");

        $('form').find("input[type=text]").each(function (ev)
        {
            if (!$(this).val())
            {
                $(this).attr("placeholder", "Please enter Page Address");
                $(this).addClass('placeholderValidation');
            }
        });
       
        if (result)
            var input =  $(".input-group");
                input.removeClass("has-error");
                input.children().removeClass("placeholderValidation");
                input.children().attr("placeholder", "URL");
    });
});

function onBegin()
{
    $('#searchError').hide(0);
    $('#searchResults').hide(0);
    $('#loadingDisplay').show(0);
    $('#loadProgressBar').css('width', '50%').attr("aria-valuenow", 50);
    $(".settings").css("opacity", "0.5");
}

function onComplete()
{
    $('#loadProgressBar').css('width', '100%').attr("aria-valuenow", 100);
    $(".settings").css("opacity", "1");
    $("#loadingDisplay").delay(500).fadeOut(20).queue(function (next)
    {
        $('#loadProgressBar').delay(1200).css('width', '0%').attr("aria-valuenow", 0);
        next();
    });
}
function onSuccess()
{
    $('#searchResults').delay(800).slideDown(500);
}

function onFailure()
{
    $('#searchError').show(0);
}
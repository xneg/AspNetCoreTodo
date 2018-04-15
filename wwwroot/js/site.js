$(document).ready(function() {
    $('#add-item-button').on('click', addItem);
    $('.done-checkbox').on('click', function(e) {
        markCompleted(e.target);
    });
})

function addItem() {
    $('#add-item-error').hide();
    var newTitle = $('#add-item-title').val();
    var dateTo = $('#deadline').val()

    $.post('/Todo/AddItem', {title: newTitle, deadline: dateTo}, function() {
        window.location = '/Todo';
    })
    .fail(function(data) {
        if (data && data.responseJSON) {
            var firstError = data.responseJSON[Object.keys(data.responseJSON)[0]];
            $('#add-item-error').text(firstError);
            $('#add-item-error').show();
        }
        else
        {
            $('#add-item-error').text(data.statusText);
            $('#add-item-error').show();
            /* statusCode
               responseText
               statusText */
        }
    })
}

function markCompleted(checkbox) {
    checkbox.disabled = true;

    $.post('/Todo/MarkDone', {id: checkbox.name}, function() {
        var row = checkbox.parentElement.parentElement;
        $(row).addClass('done');
    })
}
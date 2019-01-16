const api_loc = "/api/Employee";
let employees = null;

$(document).ready(function () {
    getData();
});

function getData() {
    $.ajax({
        type: "GET",
        url: api_loc,
        cache: false,
        success: function (data) {
            const tBody = $("#employees");

            $(tBody).empty();

            $.each(data, function (key, item) {
                const tr = $("<tr></tr>")
                    .append($("<td></td>").text(item.firstName))
                    .append($("<td></td>").text(item.middleName))
                    .append($("<td></td>").text(item.lastName))
                    .append($("<td></td>").text(item.hireDate.substring(0, 10)))
                    .append(
                        $("<td></td>").append(
                            $("<button>Edit</button>").on("click", function () {
                        }).addClass("btn btn-primary")
                            .attr("data-toggle", "modal")
                            .attr("data-target", "#exampleModal")
                            .attr("data-id", item.id)
                            .attr("data-fname", item.firstName)
                            .attr("data-mname", item.middleName)
                            .attr("data-lname", item.lastName)
                            .attr("data-hiredate", item.hireDate.substring(0, 10))
                        )
                    )
                    .append(
                        $("<td></td>").append(
                            $("<button>Delete</button>").on("click", function () {
                                 DeleteItem(item.id);
                            })
                        )
                    );
                tr.appendTo(tBody);
            });
            employees = data;
        }
    });
}

function addEmployee() {
    const item = {
        FirstName: $("#add-fname").val(),
        MiddleName: $("#add-mname").val(),
        LastName: $("#add-lname").val(),
        HireDate: $("#add-hire-date").val().substring(0, 10)
    };

    $.ajax({
        type: "POST",
        accepts: "application/json",
        url: api_loc,
        contentType: "application/json",
        data: JSON.stringify(item),
        error: function (jqXHR, textStatus, errorThrown) {
            alert("Something went wrong!");
        },
        success: function (result) {
            getData();
        }
    });
}

function editEmployee() {
    const item = {
        id: $("#edit-id").val(),
        FirstName: $("#edit-fname").val(),
        MiddleName: $("#edit-mname").val(),
        LastName: $("#edit-lname").val(),
        HireDate: $("#edit-hire-date").val().substring(0, 10)
    };

    $.ajax({
        url: api_loc + "/" + $("#edit-id").val(),
        type: "PUT",
        accepts: "application/json",
        contentType: "application/json",
        data: JSON.stringify(item),
        success: function(result) {
            getData();
        }
    });
}

$('#exampleModal').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget); // Button that triggered the modal
    var id = button.data('id');
    var firstName = button.data('fname'); // Extract info from data-* attributes
    var middleName = button.data('mname'); // Extract info from data-* attributes
    var lastName = button.data('lname'); // Extract info from data-* attributes
    var hireDate = button.data('hiredate'); // Extract info from data-* attributes
    // If necessary, you could initiate an AJAX request here (and then do the updating in a callback).
    // Update the modal's content. We'll use jQuery here, but you could use a data binding library or other methods instead.
    var modal = $(this);
    //modal.find('.modal-title').text('New message to ' + recipient)
    modal.find('#edit-id').val(id);
    modal.find('#edit-fname').val(firstName);
    modal.find('#edit-mname').val(middleName);
    modal.find('#edit-lname').val(lastName);
    modal.find('#edit-hire-date').val(hireDate);
});
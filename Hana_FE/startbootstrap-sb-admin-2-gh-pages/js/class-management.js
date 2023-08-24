$(() => {
    $("#dataTable").DataTable({
        ajax: {
            url: "https://localhost:7010/api/Class/GetAllClasses",
            type: 'GET',
            contentType: 'application/json',
            error: function (xhr) {
                $.toast({
                    heading: 'Error',
                    text: xhr.statusText,
                    icon: 'error',
                    position: 'top-right',
                    showHideTransition: 'plain'
                })
            }
        },
        columns: [
            { data: "id" },
            { data: "name" },
            { data: "startDate", orderable: false },
            { data: "dueDate", orderable: false },
            {
                data: "id",
                orderable: false,
                render: (id) =>
                    `<a href='class-detail-management.html?id=${id}'>detail</a>`,
            },
            {
                data: "id",
                orderable: false,
                render: (id) =>
                    `<a href='javascript:void(0)' onclick='deleteClass(${id})'>delete</a>`,
            },
        ],
        columnDefs: [
            {
                targets: 0,
                className: "text-center"
            },
            {
                targets: 4,
                className: "text-center"
            }
            ,
            {
                targets: 5,
                className: "text-center"
            }
        ],
    });

    $("#add-btn").click(function () {
        $("#add-student-popup").modal("show");
    });

    $("#add-class").click(function () {
        addClass();
    });

    $("#start-date").change(function () {
        const startDate = new Date($(this).val());
        const dueDateInput = $("#due-date");
        const currentDueDate = new Date(dueDateInput.val());

        if (startDate > currentDueDate) {
            dueDateInput.val($(this).val());
        }

        dueDateInput.attr("min", $(this).val());
    });

    $("#due-date").change(function () {
        const dueDate = new Date($(this).val());
        const startDateInput = $("#start-date");
        const currentStartDate = new Date(startDateInput.val());

        if (dueDate < currentStartDate) {
            startDateInput.val($(this).val());
        }

        startDateInput.attr("max", $(this).val());
    });

});

function deleteClass(classId) {
    if (confirm("Are you sure you want to delete this class? Need to be sure class don't have any student!")) {
        $.ajax({
            url: `https://localhost:7010/api/Class/DeleteClass/${classId}`,
            method: "DELETE",
            success: function (response) {
                $.toast({
                    heading: "Success!",
                    text: response.message,
                    icon: "success",
                    position: "top-right",
                    showHideTransition: "plain",
                });
                location.reload();
            },
            error: function (xhr) {
                $.toast({
                    heading: "Error",
                    text: xhr.responseJSON.message,
                    icon: "error",
                    position: "top-right",
                    showHideTransition: "plain",
                });
            }
        });
    }
}

function addClass() {
    const className = $("#class-name").val();
    const desc = $("#desc").val();
    const startDate = $("#start-date").val();
    const dueDate = $("#due-date").val();

    const newClass = {
        name: className,
        desc: desc,
        startDate: startDate,
        dueDate: dueDate
    };

    $.ajax({
        url: "https://localhost:7010/api/Class/AddNewClass",
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify(newClass),
        success: function (response) {
            console.log("Class added:", response);

            $("#add-class-modal").modal("hide");
            $.toast({
                heading: "Success!",
                text: response.message,
                icon: "success",
                position: "top-right",
                showHideTransition: "plain",
            });
            location.reload();
        },
        error: function (xhr) {
            $.toast({
                heading: "Error",
                text: xhr.responseJSON.message,
                icon: "error",
                position: "top-right",
                showHideTransition: "plain",
            });
        }
    });
}

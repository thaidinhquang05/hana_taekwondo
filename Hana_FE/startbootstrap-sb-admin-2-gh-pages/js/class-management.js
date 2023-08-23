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

    });
});

function deleteClass(classId) {
    if (confirm("Are you sure you want to delete this class?")) {
        $.ajax({
            url: `https://localhost:7010/api/Class/DeleteClass/${classId}`,
            method: "DELETE",
            success: function (response) {
                console.log("Class deleted:", response);
                loadStudent(classId);
            },
            error: function (xhr, status, error) {
                console.error(error);
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
        className: className,
        desc: desc,
        startDate: startDate,
        dueDate: dueDate
    };

    $.ajax({
        url: "https://localhost:7010/api/Class/AddClass",
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify(newClass),
        success: function (response) {
            console.log("Class added:", response);

            $("#add-class-modal").modal("hide");
            location.reload();
        },
        error: function (xhr, status, error) {
            console.error(error);
        }
    });
}

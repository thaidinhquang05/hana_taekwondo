$(() => {
    let urlParam = new URLSearchParams(window.location.search);
    let classId = urlParam.get("id");

    loadStudent(classId);
    getInfoClass(classId);
    $("#add-btn").click(function () {
        loadAvailableStudents(classId);
        $("#add-student-popup").modal("show");
    });


    $("#add-to-class").click(function () {
        const selectedStudents = $("#student-dropdown").val();

        if (selectedStudents.length > 0) {
            addStudentsToClass(classId, selectedStudents);
        }
    });
});

function loadStudent(classId) {
    $("#dataTable").DataTable({
        ajax: {
            url: "https://localhost:7010/api/Student/GetStudentsByClass/" + classId,
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
            { data: "fullName" },
            { data: "dob", orderable: false },
            { data: "gender", orderable: false },
            { data: "parentName", orderable: false },
            { data: "phone", orderable: false },
            {
                data: "id",
                orderable: false,
                render: (id) =>
                    `<a href='../../public/student/student-detail.html?id=${id}'><i class="fas fa-user-edit"></i></a>`,
            },
            {
                data: "id",
                orderable: false,
                render: (id) =>
                    `<a href='javascript:void(0)' onclick='removeStudent(${id},${classId})'>remove</a>`,
            },
        ],
        columnDefs: [
            {
                targets: 0,
                className: "text-center"
            },
            {
                targets: 6,
                className: "text-center"
            },
            {
                targets: 7,
                className: "text-center"
            }
        ],
    });
}

function loadAvailableStudents(classId) {
    $.ajax({
        url: "https://localhost:7010/api/Student/GetStudentToAddClass/" + classId,
        type: 'GET',
        contentType: 'application/json',
        success: function (data) {
            console.log(data);
            const students = data.data;

            if (students && Array.isArray(students)) {
                const dropdown = $("#student-dropdown");
                dropdown.empty();

                students.forEach(s => {
                    dropdown.append(new Option(s.fullName, s.id));
                });
            } else {
                console.error("Invalid data format:", data);
            }
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

function addStudentsToClass(classId, studentIds) {
    dataSend = {
        studentIds: studentIds,
        classId: classId
    };
    $.ajax({
        url: `https://localhost:7010/api/Class/AddStudentToClass`,
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify(dataSend),
        success: function (response) {
            $("#add-student-popup").modal("hide");
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

function getInfoClass(classId) {
    $.ajax({
        url: `https://localhost:7010/api/Class/GetClassById/${classId}`,
        method: "GET",
        contentType: "application/json",
        success: function (data) {
            $("#class-name").text(data.data.name);
            $("#class-description").text(data.data.desc);
            $("#start-date").text(data.data.startDate);
            $("#end-date").text(data.data.dueDate);
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

function removeStudent(studentId,classId) {
    if (confirm("Are you sure you want to remove this student from class?")) {
        $.ajax({
            url: `https://localhost:7010/api/Class/RemoveStudent/${studentId},${classId}`,
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
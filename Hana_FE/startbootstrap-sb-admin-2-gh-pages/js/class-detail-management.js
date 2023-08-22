$(() => {
    let urlParam = new URLSearchParams(window.location.search);
    let classId = urlParam.get("id");

    loadStudent(classId);

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
    debugger
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
                    `<a href='student-detail.html?id=${id}'><i class="fas fa-user-edit"></i></a>`,
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
        error: function (xhr, status, error) {
            console.error(error);
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
            console.log("Students added to class:", response);

            $("#add-student-popup").modal("hide");

            location.reload();
        },
        error: function (xhr, status, error) {
            console.error(error);
        }
    });
}
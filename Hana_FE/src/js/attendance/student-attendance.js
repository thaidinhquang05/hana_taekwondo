$(() => {
    $(document).ajaxStart(() => {
        $(".loading-div").show();
    });

    $(document).ajaxStop(() => {
        $(".loading-div").hide();
    });

    let urlParam = new URLSearchParams(window.location.search);
    let classId = urlParam.get("id");
    let date = urlParam.get("date");

    loadStudentList(classId, date);

    $("#save-btn").on("click", () => {
        loadClassList($("#attendant-date").val());
    });
});

function loadStudentList(classId, date) {
    $("#dataTable").DataTable({
        ajax: {
            url: `${API_START_URL}/api/Class/GetStudentByClassAndDate?classId=${classId}&date=${date}`,
            type: "GET",
            contentType: "application/json",
            error: function (xhr) {
                $.toast({
                    heading: "Error",
                    text: xhr.statusText,
                    icon: "error",
                    position: "top-right",
                    showHideTransition: "plain",
                });
            },
        },
        destroy: true,
        columns: [
            { data: "index" },
            {
                data: "studentImg",
                orderable: false,
                render: (studentImg) =>
                    `<img src=${
                        studentImg !== null
                            ? `../../img/student/${studentImg}`
                            : "../../img/defaultavatar.png"
                    } style="width: 110px; height: 120px;" alt=""/>`,
            },
            { data: "fullName", orderable: false },
            { data: "gender", orderable: false },
            {
                data: "isAttend",
                orderable: false,
                render: function (isAttend) {
                    var isAbsent = null;
                    if (isAttend === null || isAttend === false) {
                        isAbsent = true;
                        isAttend = false;
                    } else {
                        isAttend = true;
                        isAbsent = false;
                    }

                    return `
                        <input ${
                            isAbsent ? "checked" : ""
                        } name="checkAttend" value="Absent" type="radio" checked/>
                        <label>Absent</label>
                        <input ${
                            isAttend ? "checked" : ""
                        } name="checkAttend" value="Attend" type="radio"/>
                        <label>Attend</label>
                    `;
                },
            },
            {
                data: "note",
                orderable: false,
                render: (note) => `
                    <textarea style="width: 100% ; height: 7rem ;">${note}</textarea>
                `,
            },
        ],
        columnDefs: [
            {
                targets: 0,
                className: "text-center",
            },
            {
                targets: 1,
                className: "text-center",
            },
            {
                targets: 3,
                className: "text-center",
            },
            {
                targets: 4,
                className: "text-center",
            },
        ],
    });
}

$(() => {
	$(document).ajaxStart(() => {
		$(".loading-div").show();
	});

	$(document).ajaxStop(() => {
		$(".loading-div").hide();
	});

	let urlParam = new URLSearchParams(window.location.search);
	let studentId = urlParam.get("id");

	if (studentId === null) {
		window.location.href = "../../public/404.html";
	}
	loadStudentInfo(studentId);
	loadStudentAttendanceReport(studentId);
});

function loadStudentAttendanceReport(studentId) {
	$("#dataTable").DataTable({
		ajax: {
			url: `${API_START_URL}/api/Attendance/GetAttendanceByStudentId/${studentId}`,
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
			{ data: "slotDesc", orderable: false },
			{ data: "date", orderable: false },
			{
				data: "isAttendance",
				orderable: false,
				render: (isAttendance) => {
					return isAttendance
						? `<span style="color: green;">Present</span>`
						: `<span style="color: red;">Absent</span>`;
				},
			},
			{ data: "note", orderable: false },
		],
		columnDefs: [
			{
				targets: 0,
				className: "text-center",
			},
		],
	});
}

function loadStudentInfo(studentId) {
	$.ajax({
		url: `${API_START_URL}/api/Student/GetStudentInfo/${studentId}`,
		method: "GET",
		contentType: "application/json",
		success: function (response) {
			$(".card-header h2").append(response.data.fullName);
		},
		error: function (xhr) {
			$.toast({
				heading: "Error",
				text: xhr.responseJSON.message,
				icon: "error",
				position: "top-right",
				showHideTransition: "plain",
			});
		},
	});
}

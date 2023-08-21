$(() => {
	let urlParam = new URLSearchParams(window.location.search);
	let studentId = urlParam.get("id");

	loadStudentInfo(studentId);

	loadTuitionHistory(studentId);

	$("#update-btn").on("click", () => {
		let student = {
			fullName: $("#inputStudentName").val(),
			dob: $("#inputBirthday").val(),
			gender: $("#inlineRadio1").is(":checked") ? "Male" : "Female",
			parentName: $("#inputParent").val(),
			phone: $("#inputPhone").val(),
		};

		updateStudent(student, studentId);
	});
});

function loadStudentInfo(studentId) {
	$.ajax({
		url: `https://localhost:7010/api/Student/GetStudentInfo/${studentId}`,
		method: "GET",
		contentType: "application/json",
		success: (data) => {
			const studentData = data.data;
			$("#inputStudentName").val(studentData.fullName);
			$("#inputParent").val(studentData.parentName);
			$("#inputPhone").val(studentData.phone);
			$("#inputBirthday").val(studentData.dob);
			studentData.gender == "Male"
				? $("#inlineRadio1").prop("checked", true)
				: $("#inlineRadio2").prop("checked", true);
		},
		error: (xhr) => {
			$.toast({
				heading: "Error!!!",
				text: xhr.responseJSON?.message,
				icon: "error",
				position: "top-right",
				showHideTransition: "plain",
			});
		},
	});
}

function loadTuitionHistory(studentId) {
	$("#dataTable").DataTable({
		ajax: `https://localhost:7010/api/Tuition/GetTuitionByStudentId/${studentId}`,
		columns: [
			{ data: "id" },
			{ data: "paidDate" },
			{ data: "dueDate" },
			{ data: "actualAmount", orderable: false },
			{ data: "content", orderable: false },
			{ data: "note", orderable: false },
		],
	});
}

function updateStudent(student, id) {
	$.ajax({
		url: `https://localhost:7010/api/Student/UpdateStudent/${id}`,
		method: "PUT",
		contentType: "application/json",
		data: JSON.stringify(student),
		success: (data) => {
			$.toast({
				heading: "Updated Successfully!!!",
				text: data.data.message,
				icon: "success",
				position: "top-right",
				showHideTransition: "plain",
			});
			loadStudentInfo(id);
		},
		error: (xhr) => {
			$.toast({
				heading: "Updated Failed!!!",
				text: xhr.responseJSON?.message,
				icon: "error",
				position: "top-right",
				showHideTransition: "plain",
			});
		},
	});
}

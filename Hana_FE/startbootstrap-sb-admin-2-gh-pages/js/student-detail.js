$(() => {
	let urlParam = new URLSearchParams(window.location.search);
	let studentId = urlParam.get("id");
	$("#schedule").jqs()

	loadStudentInfo(studentId);
	loadTuitionHistory(studentId);

	// update student click
	$("#update-btn").on("click", () => {
		let student = {
			fullName: $("#inputStudentName").val(),
			dob: $("#inputBirthday").val(),
			gender: $("#inlineRadio1").is(":checked") ? "Male" : "Female",
			parentName: $("#inputParent").val(),
			phone: $("#inputPhone").val(),
			schedule: $("#schedule").jqs("export")
		};
		updateStudent(student, studentId);
	});

	// add tuition click
	$("#add-tuition-btn").on("click", (e) => {
		e.preventDefault();

		let tuition = {
			paidDate: $("#paid-date").val(),
			dueDate: $("#due-date").val(),
			amount: $("#amount").val(),
			actualAmount: $("#actual-amount").val(),
			content: $("#content-text").val(),
			note: $("#note-text").val(),
		};
		addTuition(studentId, tuition);
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
			$("#schedule").jqs('import', JSON.parse(studentData.schedule));
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

function loadTuitionHistory(studentId, destroy) {
	$("#dataTable").DataTable({
		ajax: `https://localhost:7010/api/Tuition/GetTuitionByStudentId/${studentId}`,
		destroy: destroy,
		columns: [
			{ data: "id" },
			{ data: "paidDate" },
			{ data: "dueDate" },
			{ data: "actualAmount", orderable: false },
			{ data: "content", orderable: false },
			{ data: "note", orderable: false },
			{
				data: "id",
				orderable: false,
				render: (id) =>
					`<a href='tuition-detail.html?id=${id}'><i class="fas fa-edit"></i></a>`,
			},
		],
		columnDefs: [
			{
				targets: 0,
				className: "text-center",
			},
			{
				targets: 6,
				className: "text-center",
			},
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

function addTuition(studentId, tuition) {
	$.ajax({
		url: `https://localhost:7010/api/Tuition/AddNewTuition/${studentId}`,
		method: "POST",
		contentType: "application/json",
		data: JSON.stringify(tuition),
		success: (response) => {
			$.toast({
				heading: "Added Successfully!!!",
				text: response.message,
				icon: "success",
				position: "top-right",
				showHideTransition: "plain",
			});
			let destroy = true;
			loadTuitionHistory(studentId, destroy);
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

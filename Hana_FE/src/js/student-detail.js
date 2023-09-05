$(() => {
	$(document).ajaxStart(() => {
		$(".loading-div").show();
	});

	$(document).ajaxStop(() => {
		$(".loading-div").hide();
	});

	let urlParam = new URLSearchParams(window.location.search);
	let studentId = urlParam.get("id");

	if (studentId == null) {
		window.location.href = "../../public/404.html";
	}

	loadStudentInfo(studentId);
	renderTimetables();
	loadStudentTimetable(studentId);
	loadTuitionHistory(studentId);

	let date = new Date();
	let currentDay = String(date.getDate()).padStart(2, "0");
	let currentMonth = String(date.getMonth() + 1).padStart(2, "0");
	let currentYear = date.getFullYear();
	let currentDate = `${currentYear}-${currentMonth}-${currentDay}`;
	$("#inputBirthday").attr("max", currentDate);

	$("#paid-date").change(function () {
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
		const startDateInput = $("#paid-date");
		const currentStartDate = new Date(startDateInput.val());

		if (dueDate < currentStartDate) {
			startDateInput.val($(this).val());
		}

		startDateInput.attr("max", $(this).val());
	});

	$("#paid-date-update").change(function () {
		const startDate = new Date($(this).val());
		const dueDateInput = $("#due-date-update");
		const currentDueDate = new Date(dueDateInput.val());

		if (startDate > currentDueDate) {
			dueDateInput.val($(this).val());
		}

		dueDateInput.attr("min", $(this).val());
	});

	$("#due-date-update").change(function () {
		const dueDate = new Date($(this).val());
		const startDateInput = $("#paid-date-update");
		const currentStartDate = new Date(startDateInput.val());

		if (dueDate < currentStartDate) {
			startDateInput.val($(this).val());
		}

		startDateInput.attr("max", $(this).val());
	});

	// update student click
	$("#update-btn").on("click", () => {
		Swal.fire({
			title: "Do you want to save the changes?",
			icon: "question",
			showCancelButton: true,
			confirmButtonText: "Save",
		}).then((result) => {
			if (result.isConfirmed) {
				let fileInput = document.getElementById("studentImg");
				let file = fileInput.files[0];

				let formData = new FormData();
				formData.append("studentImg", file);
				formData.append("fullName", $("#inputStudentName").val());
				formData.append("dob", $("#inputBirthday").val());
				formData.append(
					"gender",
					$("#inlineRadio1").is(":checked") ? "Male" : "Female"
				);
				formData.append("parentName", $("#inputParent").val());
				formData.append("phone", $("#inputPhone").val());
				updateStudent(formData, studentId);
			}
		});
	});

	$("#update-timetable-btn").on("click", () => {
		Swal.fire({
			title: "Do you want to save the changes?",
			icon: "question",
			showCancelButton: true,
			confirmButtonText: "Save",
		}).then((result) => {
			if (result.isConfirmed) {
				let timetables = [];
				$('input[type="checkbox"]:checked').each(function () {
					timetables.push({
						timetableId: this.value,
					});
				});
				updateStudentTimetables(studentId, timetables);
			}
		});
	});

	// add tuition click
	$("#add-tuition-btn").on("click", (e) => {
		e.preventDefault();

		$("#paid-date").change(function () {
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
			const startDateInput = $("#paid-date");
			const currentStartDate = new Date(startDateInput.val());

			if (dueDate < currentStartDate) {
				startDateInput.val($(this).val());
			}

			startDateInput.attr("max", $(this).val());
		});

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

	$("#update-tuition-btn").on("click", (e) => {
		e.preventDefault();
		Swal.fire({
			title: "Do you want to save the changes?",
			icon: "question",
			showCancelButton: true,
			confirmButtonText: "Save",
		}).then((result) => {
			if (result.isConfirmed) {
				let tuition = {
					paidDate: $("#paid-date-update").val(),
					dueDate: $("#due-date-update").val(),
					amount: $("#amount-update").val(),
					actualAmount: $("#actual-amount-update").val(),
					content: $("#content-text-update").val(),
					note: $("#note-text-update").val(),
				};
				updateTuition(studentId, $("#tuitionInfoId").val(), tuition);
			}
		});
	});
});

function loadStudentInfo(studentId) {
	$.ajax({
		url: `${API_START_URL}/api/Student/GetStudentInfo/${studentId}`,
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
			if (studentData.studentImg !== null) {
				$(".img-account-profile").attr("src", studentData.studentImg);
			}
		},
		error: (xhr) => {
			window.location.href = "../../public/404.html";
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

function renderTimetables() {
	$.ajax({
		url: `${API_START_URL}/api/Timetable/GetAllTimetables`,
		method: "GET",
		contentType: "application/json",
		success: (response) => {
			let data = response.data;
			$("#timetable").empty();
			$("#timetable").append(
				data.map(
					(slot) =>
						`<tr>
						<th scope="row" style="text-align: center;">
							Slot ${slot.slot.id}
							<span class="slot-desc" style="display: block;">(${slot.slot.desc})</span>
						</th>
						${slot.slot.timetables.map(
							(timetable) =>
								`<td style="text-align: center;">
									<input type="checkbox" value="${timetable.id}">
								</td>`
						)}
					</tr>`
				)
			);
		},
		error: (xhr) => {
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

function loadStudentTimetable(studentId) {
	$.ajax({
		url: `${API_START_URL}/api/Timetable/GetTimetablesByStudentId/${studentId}`,
		method: "GET",
		contentType: "application/json",
		success: (response) => {
			let data = response.data;
			data.forEach((element) => {
				$('input[type="checkbox"]').each(function () {
					if (element.timetableId == this.value) {
						this.checked = true;
					}
				});
			});
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

function updateStudentTimetables(studentId, timetables) {
	$.ajax({
		url: `${API_START_URL}/api/Timetable/UpdateStudentTimetables/${studentId}`,
		method: "PUT",
		contentType: "application/json",
		data: JSON.stringify(timetables),
		success: (response) => {
			$.toast({
				heading: "Success!!!",
				text: response.message,
				icon: "success",
				position: "top-right",
				showHideTransition: "plain",
			});
		},
		error: (xhr) => {
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

function loadTuitionHistory(studentId) {
	$("#dataTable").DataTable({
		ajax: `${API_START_URL}/api/Tuition/GetTuitionByStudentId/${studentId}`,
		destroy: true,
		columns: [
			{ data: "id" },
			{ data: "paidDate" },
			{ data: "dueDate" },
			{
				data: "actualAmount",
				orderable: false,
				render: (actualAmount) =>
					`${new Intl.NumberFormat("vi-VN", {
						style: "currency",
						currency: "VND",
					}).format(actualAmount)}`,
			},
			{ data: "content", orderable: false },
			{ data: "note", orderable: false },
			{
				data: "id",
				orderable: false,
				render: (id) =>
					`<a href='#' onclick='showTuitionInfo(${id})' 
						data-toggle="modal"
						data-target="#updateTuitionModal"
					>
						<i class="fas fa-edit"></i>
					</a>`,
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
		url: `${API_START_URL}/api/Student/UpdateStudent/${id}`,
		method: "PUT",
		data: student,
		contentType: false,
		processData: false,
		success: (data) => {
			$.toast({
				heading: "Updated Successfully!!!",
				text: data.message,
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
		url: `${API_START_URL}/api/Tuition/AddNewTuition/${studentId}`,
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
			loadTuitionHistory(studentId);
			$("#addTuitionModal").modal("hide");
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

function showTuitionInfo(tuitionId) {
	$.ajax({
		url: `${API_START_URL}/api/Tuition/GetTuitionById/${tuitionId}`,
		method: "GET",
		contentType: "application/json",
		success: (response) => {
			let tuition = response.data;
			$("#tuitionInfoId").val(tuition.id);
			$("#paid-date-update").val(tuition.paidDate);
			$("#due-date-update").val(tuition.dueDate);
			$("#amount-update").val(tuition.amount);
			$("#actual-amount-update").val(tuition.actualAmount);
			$("#content-text-update").val(tuition.content);
			$("#note-text-update").val(tuition.note);
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

function updateTuition(studentId, tuitionId, tuition) {
	$.ajax({
		url: `${API_START_URL}/api/Tuition/UpdateTuitionInfo/${tuitionId}`,
		method: "PUT",
		contentType: "application/json",
		data: JSON.stringify(tuition),
		success: (res) => {
			$.toast({
				heading: "Success!!!",
				text: "Updated Successfully!!!",
				icon: "success",
				position: "top-right",
				showHideTransition: "plain",
			});
			loadTuitionHistory(studentId);
			$("#updateTuitionModal").modal("hide");
		},
		error: (xhr) => {
			$.toast({
				heading: "Error!!!",
				text: xhr.responseJSON.message,
				icon: "error",
				position: "top-right",
				showHideTransition: "plain",
			});
		},
	});
}

function readImg(input) {
	if (input.files && input.files[0]) {
		var reader = new FileReader();

		reader.onload = function (e) {
			$(".img-account-profile").attr("src", e.target.result);
		};

		reader.readAsDataURL(input.files[0]);
	}
}

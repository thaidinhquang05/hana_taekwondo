$(() => {
	$(document).ajaxStart(() => {
		$(".loading-div").show();
	});

	$(document).ajaxStop(() => {
		$(".loading-div").hide();
	});

	renderTimetables();

	$("#inputPaidDate").change(function () {
		const startDate = new Date($(this).val());
		const dueDateInput = $("#inputDueDate");
		const currentDueDate = new Date(dueDateInput.val());

		if (startDate > currentDueDate) {
			dueDateInput.val($(this).val());
		}

		dueDateInput.attr("min", $(this).val());
	});

	$("#inputDueDate").change(function () {
		const dueDate = new Date($(this).val());
		const startDateInput = $("#inputPaidDate");
		const currentStartDate = new Date(startDateInput.val());

		if (dueDate < currentStartDate) {
			startDateInput.val($(this).val());
		}

		startDateInput.attr("max", $(this).val());
	});

	let date = new Date();
	let currentDay = String(date.getDate()).padStart(2, "0");
	let currentMonth = String(date.getMonth() + 1).padStart(2, "0");
	let currentYear = date.getFullYear();
	let currentDate = `${currentYear}-${currentMonth}-${currentDay}`;
	$("#inputBirthday").attr("max", currentDate);

	$("#add-btn").on("click", () => {
		let tuition = {
			paidDate: $("#inputPaidDate").val(),
			dueDate: $("#inputDueDate").val(),
			amount: $("#inputAmount").val(),
			actualAmount: $("#inputActualAmount").val(),
			content: $("#content-text").val(),
			note: $("#note-text").val(),
		};

		let timetables = [];
		$('input[type="checkbox"]:checked').each(function () {
			timetables.push({
				timetableId: this.value,
			});
		});

		let fileInput = document.getElementById("studentImgInput");
		let file = fileInput.files[0];

		let formData = new FormData();
		formData.append("studentImg", file);
		formData.append("fullName", $("#inputStudentName").val());
		formData.append("dob", $("#inputBirthday").val());
		formData.append(
			"gender",
			$("#inlineRadio1").is(":checked") ? true : false
		);
		formData.append("parentName", $("#inputParent").val());
		formData.append("phone", $("#inputPhone").val());
		formData.append(
			"tuition",
			tuition.paidDate === "" ? null : JSON.stringify(tuition)
		);
		formData.append(
			"timetables",
			timetables.length > 0 ? JSON.stringify(timetables) : null
		);

		if (
			$("#inputStudentName").val() === "" ||
			$("#inputBirthday").val() === "" ||
			$("#inputParent").val() == ""
		) {
			$.toast({
				heading: "Warning!!!",
				text: "Need to filled all the field",
				icon: "warning",
				position: "top-right",
				showHideTransition: "plain",
			});
		} else {
			addStudent(formData);
		}
	});
});

function addStudent(student) {
	$.ajax({
		url: `${API_START_URL}/api/Student/AddNewStudent`,
		type: "POST",
		data: student,
		contentType: false,
		processData: false,
		success: (response) => {
			$.toast({
				heading: "Added New Student Successfully!",
				text: response.message,
				icon: "success",
				position: "top-right",
				showHideTransition: "plain",
			});
			setTimeout(() => {
				window.location.href = "../student/student-list.html";
			}, 2000);
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

function renderTimetables() {
	$.ajax({
		url: `${API_START_URL}/api/Timetable/GetAllTimetables`,
		method: "GET",
		contentType: "application/json",
		success: (response) => {
			let data = response.data;
			$("tbody").empty();
			$("tbody").append(
				data.map(
					(slot) =>
						`<tr>
						<th scope="row">
							Slot ${slot.slot.id}
							<span class="slot-desc">(${slot.slot.desc})</span>
						</th>
						${slot.slot.timetables.map(
							(timetable) =>
								`<td>
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

function readImg(input) {
	if (input.files && input.files[0]) {
		var reader = new FileReader();

		reader.onload = function (e) {
			$("#studentImg").attr("src", e.target.result);
		};

		reader.readAsDataURL(input.files[0]);
	}
}

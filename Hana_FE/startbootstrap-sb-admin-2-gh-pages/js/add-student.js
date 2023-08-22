$(() => {
	renderTimetables()

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
		$('input[type="checkbox"]:checked').each(function() {
			timetables.push({
				timetableId: this.value
			})
		});

		let student = {
			fullName: $("#inputStudentName").val(),
			dob: $("#inputBirthday").val(),
			gender: $("#inlineRadio1").is(":checked") ? true : false,
			parentName: $("#inputParent").val(),
			phone: $("#inputPhone").val(),
			tuition: tuition.paidDate === "" ? null : tuition,
			timetables: timetables.length == 0 ? null : timetables,
		};
		
        if (student.fullName === "" || student.dob === "" || student.parentName == "") {
            $.toast({
				heading: "Warning!!!",
				text: "Need to filled all the field",
				icon: "warning",
				position: "top-right",
				showHideTransition: "plain",
			});
        } else {
            addStudent(student)
        }
	});
});

function addStudent(student) {
	$.ajax({
		url: "https://localhost:7010/api/Student/AddNewStudent",
		method: "POST",
		contentType: "application/json",
		data: JSON.stringify(student),
		success: (response) => {
            $.toast({
				heading: "Success!",
				text: response.message,
				icon: "success",
				position: "top-right",
				showHideTransition: "plain",
			});
            setTimeout(() => {
                window.location.href = "../student-list.html";
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
		url: "https://localhost:7010/api/Timetable/GetAllTimetables",
		method: "GET",
		contentType: "application/json",
		success: (response) => {
            let data = response.data;
			$("tbody").empty()
			$("tbody").append(
				data.map((slot) => (
					`<tr>
						<th scope="row">
							Slot ${slot.slot.id}
							<span class="slot-desc">(${slot.slot.desc})</span>
						</th>
						${
							slot.slot.timetables.map((timetable) => (
								`<td>
									<input type="checkbox" value="${timetable.id}">
								</td>`
							))
						}
					</tr>`
				))
			)
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

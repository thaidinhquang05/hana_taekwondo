$(() => {
	$(document).ajaxStart(() => {
		$(".loading-div").show();
	});

	$(document).ajaxStop(() => {
		$(".loading-div").hide();
	});

	let urlParam = new URLSearchParams(window.location.search);
	let slotId = urlParam.get("id");
	let date = urlParam.get("date");

	loadStudentList(date);

	$("#attendance-form").on("submit", function (event) {
		submitAttend(event, slotId, date);
	});
});

function loadStudentList(date) {
	$("#dataTable").DataTable({
		ajax: {
			url: `${API_START_URL}/api/Class/GetStudentMakeUpBySlotAndDate?date=${date}`,
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
				render: function (isAttend, _, rowData) {
					var isAbsent = null;
					if (isAttend === null || isAttend === false) {
						isAbsent = true;
						isAttend = false;
					} else {
						isAttend = true;
						isAbsent = false;
					}

					return `
            <input ${isAbsent ? "checked" : ""} 
							name="checkAttend${
								rowData.id
							}" value="Absent" type="radio" style="cursor:pointer;"/>
            <label>Absent</label>

            <input ${isAttend ? "checked" : ""} 
							name="checkAttend${
								rowData.id
							}" value="Attend" type="radio" style="cursor:pointer;"/>
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

function submitAttend(event, slotId, date) {
	event.preventDefault();

	const rowData = [];

	$("#dataTable tbody tr").each(function () {
		const row = $(this);
		const id = row
			.find('input[name^="checkAttend"]')
			.attr("name")
			.replace("checkAttend", "");
		const isAttend = row.find('input[name^="checkAttend"]:checked').val();
		const note = row.find("textarea").val();
		rowData.push({
			id: id,
			isAttend: isAttend === "Attend",
			note: note,
		});
	});
	$.ajax({
		url: `${API_START_URL}/api/Class/TakeMakeUpAttendance?slotId=${slotId}&date=${date}`,
		method: "POST",
		data: JSON.stringify(rowData),
		contentType: "application/json",
		success: function (response) {
			$.toast({
				heading: "Take Attendance Successfully!",
				text: response.message,
				icon: "success",
				position: "top-right",
				showHideTransition: "plain",
			});
			setTimeout(() => {
				window.location.href = "../attendance/take-attendance.html";
			}, 2000);
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

$(() => {
	$(document).ajaxStart(() => {
		$(".loading-div").show();
	});

	$(document).ajaxStop(() => {
		$(".loading-div").hide();
	});

	let date = new Date();
	let currentDay = String(date.getDate()).padStart(2, "0");
	let currentMonth = String(date.getMonth() + 1).padStart(2, "0");
	let currentYear = date.getFullYear();
	let currentDate = `${currentYear}-${currentMonth}-${currentDay}`;

	$("#attendant-date").val(currentDate);

	loadSlots($("#attendant-date").val());

	$("#pick-date-btn").on("click", () => {
		// loadSlots($("#attendant-date").val());
	});
});

function loadSlots(date) {
	$("#dataTable").DataTable({
		ajax: {
			url: `${API_START_URL}/api/Slot/GetSlots`,
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
			{ data: "slotDescription", orderable: false },
			{
				data: "id",
				orderable: false,
				render: (id) => `
					<a href='student-attendance.html?id=${id}&date=${date}'>
						Take Attendance
					</a>
					|
					<a href='makeup-attendance.html?id=${id}&date=${date}'>
						Make Up Attendance
					</a>
				`,
			},
		],
		columnDefs: [
			{
				targets: 0,
				className: "text-center",
			},
		],
	});
}

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

	loadClassList($("#attendant-date").val());

	$("#pick-date-btn").on("click", () => {
		loadClassList($("#attendant-date").val());
	});
});

function loadClassList(date) {
	$("#dataTable").DataTable({
		ajax: {
			url: `${API_START_URL}/api/Class/GetClassesByDate?date=${date}`,
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
			{ data: "className", orderable: false },
			{ data: "desc", orderable: false },
			{
				data: "id",
				orderable: false,
				render: (id) => `
          <a href='class-detail-management.html?id=${id}'>
            Take Attendance
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

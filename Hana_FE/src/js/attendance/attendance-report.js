$(() => {
	$(document).ajaxStart(() => {
		$(".loading-div").show();
	});

	$(document).ajaxStop(() => {
		$(".loading-div").hide();
	});
	const currentYear = new Date().getFullYear();
	const yearOptionsDiv = document.getElementById("yearOptions");
	const yearDropdownButton = document.getElementById("yearDropdown");

	if (localStorage.getItem("Year") != null) {
		yearDropdownButton.textContent = localStorage.getItem("Year");
	} else {
		yearDropdownButton.textContent = currentYear;
	}

	for (let year = currentYear; year >= currentYear - 10; year--) {
		const yearOption = document.createElement("a");
		yearOption.classList.add("dropdown-item");
		yearOption.href = `../../public/attendance/attendance-report.html?year=${year}`;

		yearOption.textContent = year;
		yearOptionsDiv.appendChild(yearOption);
	}

	yearOptionsDiv.addEventListener("click", function (event) {
		event.preventDefault();

		const selectedYear = event.target.textContent;

		yearDropdownButton.textContent = `${selectedYear}`;
		window.location.href = `../../public/attendance/attendance-report.html?year=${selectedYear}`;
		localStorage.setItem("Year", selectedYear);
	});

	const year = yearDropdownButton.textContent;
	loadHistory(year);
});

function loadHistory(year) {
	$("#dataTable").DataTable({
		ajax: {
			url: `${API_START_URL}/api/Student/GetAttendanceHistory?year=${year}`,
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
			{ data: "fullName" },
			{ data: "jan", orderable: false },
			{ data: "feb", orderable: false },
			{ data: "mar", orderable: false },
			{ data: "apr", orderable: false },
			{ data: "may", orderable: false },
			{ data: "jun", orderable: false },
			{ data: "jul", orderable: false },
			{ data: "aug", orderable: false },
			{ data: "sep", orderable: false },
			{ data: "oct", orderable: false },
			{ data: "nov", orderable: false },
			{ data: "dec", orderable: false },
		],
		columnDefs: [
			{
				targets: 0,
				className: "text-center",
			},
			{
				targets: 2,
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
			{
				targets: 5,
				className: "text-center",
			},
			{
				targets: 6,
				className: "text-center",
			},
			{
				targets: 7,
				className: "text-center",
			},
			{
				targets: 8,
				className: "text-center",
			},
			{
				targets: 9,
				className: "text-center",
			},
			{
				targets: 10,
				className: "text-center",
			},
			{
				targets: 11,
				className: "text-center",
			},
			{
				targets: 12,
				className: "text-center",
			},
			{
				targets: 13,
				className: "text-center",
			},
		],
	});
}

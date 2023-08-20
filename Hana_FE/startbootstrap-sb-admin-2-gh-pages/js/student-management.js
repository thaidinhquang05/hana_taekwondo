$(() => {
	$("#dataTable").DataTable({
		ajax: "https://localhost:7010/api/Student/GetAllStudents",
		columns: [
			{ data: "id" },
			{ data: "fullName" },
			{ data: "dob", orderable: false },
			{ data: "gender", orderable: false },
			{ data: "parentName", orderable: false },
			{ data: "phone", orderable: false },
			{
				data: "id",
				orderable: false,
				render: (id) =>
					`<a href='student-detail.html/${id}'><i class="fas fa-user-edit"></i></a>`,
			},
		],
		columnDefs: [
            {
				targets: 0, // your case first column
				className: "text-center"
			},
			{
				targets: 6, // your case first column
				className: "text-center"
			}
		],
	});
});

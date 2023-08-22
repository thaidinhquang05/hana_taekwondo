$(() => {
	$("#dataTable").DataTable({
        ajax: {
            url: "https://localhost:7010/api/Student/GetAllStudents",
            type: 'GET',
            contentType: 'application/json',
            error: function(xhr) {
                $.toast({
                    heading: 'Error',
                    text: "Have something wrong while load student list!!!",
                    icon: 'error',
                    position: 'top-right',
                    showHideTransition: 'plain'
                })
            }
        },
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
					`<a href='student-detail.html?id=${id}'><i class="fas fa-user-edit"></i></a>`,
			},
		],
		columnDefs: [
            {
				targets: 0,
				className: "text-center"
			},
			{
				targets: 6,
				className: "text-center"
			}
		],
	});
});

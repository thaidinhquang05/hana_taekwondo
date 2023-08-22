$(() => {
	$("#dataTable").DataTable({
        ajax: {
            url: "https://localhost:7010/api/Class/GetAllClasses",
            type: 'GET',
            contentType: 'application/json',
            error: function(xhr) {
                $.toast({
                    heading: 'Error',
                    text: xhr.statusText,
                    icon: 'error',
                    position: 'top-right',
                    showHideTransition: 'plain'
                })
            }
        },
		columns: [
			{ data: "id" },
			{ data: "name" },
			{ data: "startDate", orderable: false },
			{ data: "dueDate", orderable: false },
			{
				data: "id",
				orderable: false,
				render: (id) =>
					`<a href='class-detail-management.html?id=${id}'>detail</a>`,
			},
            {
				data: "id",
				orderable: false,
				render: (id) =>
					`<a href='class-delete.html?id=${id}'>delete</a>`,
			},
		],
		columnDefs: [
            {
				targets: 0,
				className: "text-center"
			},
            {
				targets: 4,
				className: "text-center"
			}
            ,
			{
				targets: 5,
				className: "text-center"
			}
		],
	});
});

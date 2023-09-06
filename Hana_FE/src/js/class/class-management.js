$(() => {
	loadClassList();

	$("#add-btn").click(function () {
		$("#add-student-popup").modal("show");
	});

	$("#add-class").click(function () {
		addClass();
	});

	$("#start-date").change(function () {
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
		const startDateInput = $("#start-date");
		const currentStartDate = new Date(startDateInput.val());

		if (dueDate < currentStartDate) {
			startDateInput.val($(this).val());
		}

		startDateInput.attr("max", $(this).val());
	});
});

function loadClassList() {
	$("#dataTable").DataTable({
		ajax: {
			url: `${API_START_URL}/api/Class/GetAllClasses`,
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
			{ data: "id" },
			{ data: "name" },
			{ data: "startDate", orderable: false },
			{ data: "dueDate", orderable: false },
			{
				data: "id",
				orderable: false,
				render: (id) =>
					`<a href='class-detail-management.html?id=${id}'>
                        <i class="fas fa-edit"></i>
                    </a>
                    <a href='javascript:void(0)' style="color: red; margin-left: 10px" onclick='deleteClass(${id})'>
                        <i class="fas fa-trash"></i>
                    </a>`,
			},
		],
		columnDefs: [
			{
				targets: 0,
				className: "text-center",
			},
			{
				targets: 4,
				className: "text-center",
			},
		],
	});
}

function deleteClass(classId) {
	Swal.fire({
		title: "Are you sure?",
		text: "You won't be able to revert this!",
		icon: "warning",
		showCancelButton: true,
		confirmButtonColor: "#3085d6",
		cancelButtonColor: "#d33",
		confirmButtonText: "Yes, delete it!",
	}).then((result) => {
		if (result.isConfirmed) {
			$.ajax({
				url: `${API_START_URL}/api/Class/DeleteClass/${classId}`,
				method: "DELETE",
				success: function (response) {
					$.toast({
						heading: "Success!",
						text: response.message,
						icon: "success",
						position: "top-right",
						showHideTransition: "plain",
					});
					loadClassList();
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
	});
}

function addClass() {
	const className = $("#class-name").val();
	const desc = $("#desc").val();
	const startDate = $("#start-date").val();
	const dueDate = $("#due-date").val();

	const newClass = {
		name: className,
		desc: desc,
		startDate: startDate,
		dueDate: dueDate,
	};

	$.ajax({
		url: `${API_START_URL}/api/Class/AddNewClass`,
		method: "POST",
		contentType: "application/json",
		data: JSON.stringify(newClass),
		success: function (response) {
			console.log("Class added:", response);

			$("#add-class-modal").modal("hide");
			$.toast({
				heading: "Success!",
				text: response.message,
				icon: "success",
				position: "top-right",
				showHideTransition: "plain",
			});
			location.reload();
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

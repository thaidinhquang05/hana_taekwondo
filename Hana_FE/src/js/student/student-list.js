$(() => {
	$(document).ajaxStart(() => {
		$(".loading-div").show();
	});

	$(document).ajaxStop(() => {
		$(".loading-div").hide();
	});

	loadStudentList();
});

function loadStudentList() {
	$("#dataTable").DataTable({
		ajax: {
			url: `${API_START_URL}/api/Student/GetAllStudents`,
			type: "GET",
			contentType: "application/json",
			error: function (xhr) {
				$.toast({
					heading: "Error",
					text: "Have something wrong while load student list!!!",
					icon: "error",
					position: "top-right",
					showHideTransition: "plain",
				});
			},
		},
		destroy: true,
		columns: [
			{ data: "id" },
			{
				data: "studentImg",
				orderable: false,
				render: (studentImg) =>
					`<img src=${
						studentImg !== null ? studentImg : "../../img/defaultavatar.png"
					} style="width: 110px; height: 120px;" alt=""/>`,
			},
			{ data: "fullName" },
			{ data: "dob", orderable: false },
			{ data: "gender", orderable: false },
			{ data: "parentName", orderable: false },
			{ data: "phone", orderable: false },
			{
				data: "id",
				orderable: false,
				render: (id) =>
					`<a href='../../public/student/student-detail.html?id=${id}'>
						<i class="fas fa-user-edit"></i>
					</a>
					<a href='#' style='color: red; margin-left: 10px'
						onclick='return deleteStudent(${id})'
					>
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
				targets: 1,
				className: "text-center",
			},
			{
				targets: 7,
				className: "text-center",
			},
		],
	});
}

function deleteStudent(studentId) {
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
				url: `${API_START_URL}/api/Student/DeleteStudent/${studentId}`,
				method: "DELETE",
				contentType: "application/json",
				success: (response) => {
					$.toast({
						heading: "Success!",
						text: response.message,
						icon: "success",
						position: "top-right",
						showHideTransition: "plain",
					});
					loadStudentList();
				},
				error: (xhr) => {
					$.toast({
						heading: "Error",
						text: xhr.responseJSON?.message,
						icon: "error",
						position: "top-right",
						showHideTransition: "plain",
					});
				},
			});
		}
	});

	return false;
}

$(() => {
	$(document).ajaxStart(() => {
		$(".loading-div").show();
	});

	$(document).ajaxStop(() => {
		$(".loading-div").hide();
	});

	loadSpendingList();

	$("#add-spending-btn").on("click", (e) => {
		e.preventDefault();

		let spending = {
			electric: $("#electric").val(),
			water: $("#water").val(),
			rent: $("#rent").val(),
			salary: $("#salary").val(),
			eating: $("#eating").val(),
			another: $("#another").val(),
			paidDate: $("#paid-date").val(),
			content: $("#note-text").val(),
		};
		addSpending(spending);
	});

	$("#update-spending-btn").on("click", (e) => {
		e.preventDefault();
		Swal.fire({
			title: "Do you want to save the changes?",
			icon: "question",
			showCancelButton: true,
			confirmButtonText: "Save",
		}).then((result) => {
			if (result.isConfirmed) {
				let spending = {
					electric: $("#electric-update").val(),
					water: $("#water-update").val(),
					rent: $("#rent-update").val(),
					salary: $("#salary-update").val(),
					eating: $("#eating-update").val(),
					another: $("#another-update").val(),
					paidDate: $("#paid-date-update").val(),
					content: $("#note-text-update").val(),
				};
				updateSpending($("#spendingId").val(), spending);
			}
		});
	});
});

function loadSpendingList() {
	$("#dataTable").DataTable({
		ajax: {
			url: `${API_START_URL}/api/Spending/GetListSpendingValue`,
			type: "GET",
			contentType: "application/json",
			error: (xhr) => {
				$.toast({
					heading: "Error",
					text: "Have something wrong while load spending list!!!",
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
				data: "electric",
				render: (electric) =>
					`${new Intl.NumberFormat("vi-VN", {
						style: "currency",
						currency: "VND",
					}).format(electric)}`,
			},
			{
				data: "water",
				render: (water) =>
					`${new Intl.NumberFormat("vi-VN", {
						style: "currency",
						currency: "VND",
					}).format(water)}`,
			},
			{
				data: "rent",
				render: (rent) =>
					`${new Intl.NumberFormat("vi-VN", {
						style: "currency",
						currency: "VND",
					}).format(rent)}`,
			},
			{
				data: "salary",
				render: (salary) =>
					`${new Intl.NumberFormat("vi-VN", {
						style: "currency",
						currency: "VND",
					}).format(salary)}`,
			},
			{
				data: "eating",
				render: (eating) =>
					`${new Intl.NumberFormat("vi-VN", {
						style: "currency",
						currency: "VND",
					}).format(eating)}`,
			},
			{
				data: "another",
				render: (another) =>
					`${new Intl.NumberFormat("vi-VN", {
						style: "currency",
						currency: "VND",
					}).format(another)}`,
			},
			{ data: "paidDate", orderable: false },
			{ data: "content", orderable: false },
			{
				data: "id",
				orderable: false,
				render: (id) =>
					`<a href='#' onclick='getSpendingById(${id})' 
						data-toggle="modal"
						data-target="#updateSpendingModal"
					>
						<i class="fas fa-edit"></i>
					</a>
					<a href='#' style='color: red; margin-left: 10px'
						onclick='return deleteSpending(${id})'
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
				targets: 9,
				className: "text-center",
			},
		],
	});
}

function addSpending(spending) {
	$.ajax({
		url: `${API_START_URL}/api/Spending/AddSpending`,
		method: "POST",
		contentType: "application/json",
		data: JSON.stringify(spending),
		success: (res) => {
			$.toast({
				heading: "Added Successfully!!!",
				text: res.message,
				icon: "success",
				position: "top-right",
				showHideTransition: "plain",
			});
			loadSpendingList();
			$("#addSpendingModal").modal("hide");
		},
		error: (xhr) => {
			$.toast({
				heading: "Updated Failed!!!",
				text: xhr.responseJSON?.message,
				icon: "error",
				position: "top-right",
				showHideTransition: "plain",
			});
		},
	});
}

function getSpendingById(spendingId) {
	$.ajax({
		url: `${API_START_URL}/api/Spending/GetSpendingItem/${spendingId}`,
		type: "GET",
		contentType: "application/json",
		success: (res) => {
			const spending = res.data;
			$("#spendingId").val(spending.id);
			$("#electric-update").val(spending.electric);
			$("#water-update").val(spending.water);
			$("#rent-update").val(spending.rent);
			$("#salary-update").val(spending.salary);
			$("#eating-update").val(spending.eating);
			$("#another-update").val(spending.another);
			$("#paid-date-update").val(spending.paidDate);
			$("#note-text-update").val(spending.content);
		},
		error: (xhr) => {
			$.toast({
				heading: "Error!!!",
				text: xhr.responseJSON?.message,
				icon: "error",
				position: "top-right",
				showHideTransition: "plain",
			});
		},
	});
}

function updateSpending(spendingId, spending) {
	$.ajax({
		url: `${API_START_URL}/api/Spending/UpdateSpending/${spendingId}`,
		method: "PUT",
		contentType: "application/json",
		data: JSON.stringify(spending),
		success: (res) => {
			$.toast({
				heading: "Success!!!",
				text: "Updated Successfully!!!",
				icon: "success",
				position: "top-right",
				showHideTransition: "plain",
			});
			loadSpendingList();
			$("#updateSpendingModal").modal("hide");
		},
		error: (xhr) => {
			$.toast({
				heading: "Error!!!",
				text: xhr.responseJSON.message,
				icon: "error",
				position: "top-right",
				showHideTransition: "plain",
			});
		},
	});
}

function deleteSpending(spendingId) {
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
				url: `${API_START_URL}/api/Spending/DeleteSpendingRecord/${spendingId}`,
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
					loadSpendingList();
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
	});

	return false;
}

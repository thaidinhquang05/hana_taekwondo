$(() => {
	$(document).ajaxStart(() => {
		$(".loading-div").show();
	});

	$(document).ajaxStop(() => {
		$(".loading-div").hide();
	});

	loadNotificationList();

	setInterval(updateNotificationTimes, 60000);
});

function loadNotificationList() {

	const notificationTableBody = document.getElementById("notificationTableBody");

	$.ajax({
		url: `${API_START_URL}/api/Tuition/GetDeadlineTutions`,
		type: "GET",
		contentType: "application/json",
		success: function (data) {

			const students = data.data;

			if (students && students.length > 0) {
				notificationTableBody.innerHTML = "";

				students.forEach((notification) => {
					const row = document.createElement("tr");
					row.innerHTML = `
                        <td class="border-less">
                            <img class="img-profile rounded-circle"
                                src="${notification.avatar}" style="width: 5%;" />
								</td>
								<td class="border-less">
                            <span>We would like to remind you that the final 
							deadline for paying <strong style="color: red;">${notification.fullName}</strong>'s course fees is approaching. There are only <strong style="color: red;">${getNotificationDueDate(notification.dueDate)}</strong> days left.
							 Please make sure to make the payment on time to avoid any interruptions in <strong style="color: red;">${notification.fullName}</strong> learning journey.</span>
							 </td>
							 <td>
							 <span class="notification-time" style="float: right;">${getNotificationTime(notification.notificationTime)}</span>
                        </td>
                    `;

					row.addEventListener("click", function (event) {
						const studentId = notification.studentId;
						event.preventDefault();
						window.location.href = `../../public/student/student-detail.html?id=${studentId}`;
					});

					notificationTableBody.appendChild(row);
				});
			} else {
				const noNotificationRow = document.createElement("tr");
				noNotificationRow.innerHTML = `
					<td colspan="3" style="text-align: center; font-size: 18px; font-weight: bold;">No Notifications Yet</td>
				`;
				notificationTableBody.appendChild(noNotificationRow);
			}
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

function updateNotificationTimes() {
	const notificationTimeElements = document.querySelectorAll(".notification-time");
	notificationTimeElements.forEach((element) => {
		const notificationTime = new Date(element.dataset.notificationTime);
		element.innerText = getNotificationTime(notificationTime);
	});
}

function getNotificationTime(notificationTime) {
	const notification = new Date(notificationTime);
	const currentTime = new Date();
	var elapsedMinutes = Math.floor((currentTime - notification) / 60000);
	if (elapsedMinutes === 0) {
		return "1 phút trước";
	} else if (elapsedMinutes < 60) {
		return `${elapsedMinutes} phút trước`;
	} else if (elapsedMinutes < 1440) {
		const hours = Math.floor(elapsedMinutes / 60);
		return `${hours} giờ trước`;
	} else {
		const days = Math.floor(elapsedMinutes / 1440);
		return `${days} ngày trước`;
	}
}

function getNotificationDueDate(notificationTime) {
	const notification = new Date(notificationTime);
	const currentTime = new Date();
	var elapsedMinutes = Math.floor((currentTime - notification) / 60000);

	const days = Math.floor(elapsedMinutes / 1440);
	return `${days}`;

}
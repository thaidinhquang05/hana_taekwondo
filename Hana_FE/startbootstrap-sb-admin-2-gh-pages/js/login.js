$(function () {
	$("#login-btn").on("click", (e) => {
		e.preventDefault();

		let user = {
			username: $("#email").val(),
			password: $("#password").val(),
		};

		$.ajax({
			url: "https://localhost:7010/api/Auth/Login",
			method: "POST",
			contentType: "application/json",
			data: JSON.stringify(user),
			success: function (data) {
                localStorage.setItem("token", data.data);
				setTimeout(() => {
					window.location.href = "../index.html";
				}, 3000);

				let toast = {
					title: "Login Success!",
					message: data.message,
					status: TOAST_STATUS.SUCCESS,
					timeout: 3000,
				};
				Toast.create(toast);
			},
			error: function (xhr) {
				let toast = {
					title: "Login Failed!",
					message: xhr.responseJSON.message,
					status: TOAST_STATUS.DANGER,
					timeout: 3000,
				};
				Toast.create(toast);
			},
		});
	});
});

$(function () {
	$("#login-btn").on("click", (e) => {
		e.preventDefault();

		let user = {
			username: $("#email").val(),
			password: $("#password").val(),
		};

		$.ajax({
			url: `${API_START_URL}/api/Auth/Login`,
			method: "POST",
			contentType: "application/json",
			data: JSON.stringify(user),
			success: function (data) {
				localStorage.setItem("token", data.data);
				setTimeout(() => {
					window.location.href = "../../public/index.html";
				}, 3000);

				$.toast({
					heading: "Login Successfully!",
					text: "",
					icon: "success",
					position: "top-right",
					showHideTransition: "plain",
				});
			},
			error: function (xhr) {
				$.toast({
					heading: "Login Failed!",
					text: xhr.responseJSON.message,
					icon: "error",
					position: "top-right",
					showHideTransition: "plain",
				});
			},
		});
	});
});

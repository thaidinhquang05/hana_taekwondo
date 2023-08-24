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
					window.location.href = "../../public/student/student-list.html";
				}, 3000);

				$.toast({
                    heading: 'Login Successfully!',
					text: '',
                    icon: 'success',
                    position: 'top-right',
                    showHideTransition: 'plain'
                })
			},
			error: function (xhr) {
				$.toast({
                    heading: 'Login Failed!',
                    text: xhr.responseJSON.message,
                    icon: 'error',
                    position: 'top-right',
                    showHideTransition: 'plain'
                })
			},
		});
	});
});

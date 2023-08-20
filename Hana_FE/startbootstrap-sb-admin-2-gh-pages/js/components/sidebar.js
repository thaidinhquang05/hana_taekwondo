class Sidebar extends HTMLElement {
	constructor() {
		super();
	}

	connectedCallback() {
		this.innerHTML = 
            `<ul
                class="navbar-nav bg-gradient-primary sidebar sidebar-dark accordion"
                id="accordionSidebar"
            >
                <!-- Sidebar - Brand -->
                <a
                    class="sidebar-brand d-flex align-items-center justify-content-center"
                    href="index.html"
                >
                    <div class="sidebar-brand-icon rotate-n-15">
                        <i class="fas fa-school"></i>
                    </div>
                    <div class="sidebar-brand-text mx-3">
                        Hana Taekwondo
                    </div>
                </a>

                <!-- Divider -->
                <hr class="sidebar-divider my-0" />

                <!-- Nav Item - Dashboard -->
                <li class="nav-item active">
                    <a class="nav-link" href="index.html">
                        <i class="fas fa-fw fa-tachometer-alt"></i>
                        <span>Dashboard</span></a
                    >
                </li>

                <!-- Divider -->
                <hr class="sidebar-divider" />

                <!-- Heading -->
                <div class="sidebar-heading">Management</div>

                <!-- Nav Item - Student Management -->
                <li class="nav-item">
                    <a class="nav-link" href="student-management.html">
                        <i class="fas fa-user-graduate"></i>
                        <span>Student Management</span></a
                    >
                </li>

                <!-- Nav Item - Class Management -->
                <li class="nav-item">
                    <a class="nav-link" href="#">
                        <i class="fas fa-university"></i>
                        <span>Class Management</span></a
                    >
                </li>

                <!-- Divider -->
                <hr class="sidebar-divider d-none d-md-block" />

                <!-- Sidebar Toggler (Sidebar) -->
                <div class="text-center d-none d-md-inline">
                    <button
                        class="rounded-circle border-0"
                        id="sidebarToggle"
                    ></button>
                </div>
            </ul>`;

		$("#sidebarToggle, #sidebarToggleTop").on("click", function (e) {
			$("body").toggleClass("sidebar-toggled");
			$(".sidebar").toggleClass("toggled");
			if ($(".sidebar").hasClass("toggled")) {
				$(".sidebar .collapse").collapse("hide");
			}
		});
	}
}

customElements.define("sidebar-component", Sidebar);

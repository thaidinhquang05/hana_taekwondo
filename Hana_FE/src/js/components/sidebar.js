class Sidebar extends HTMLElement {
	constructor() {
		super();
	}

	connectedCallback() {
		this.innerHTML = `<ul
                class="navbar-nav bg-gradient-primary sidebar sidebar-dark accordion"
                id="accordionSidebar"
            >
                <!-- Sidebar - Brand -->
                <a
                    class="sidebar-brand d-flex align-items-center justify-content-center"
                    href="../../public/index.html"
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
                    <a class="nav-link" href="../../public/index.html">
                        <i class="fas fa-fw fa-tachometer-alt"></i>
                        <span>Dashboard</span>
                    </a>
                </li>

                <!-- Divider -->
                <hr class="sidebar-divider" />

                <!-- Heading -->
                <div class="sidebar-heading">Student</div>

                <!-- Nav Item - Student Management Collapse Menu -->
                <li class="nav-item">
                    <a class="nav-link collapsed" href="#" data-toggle="collapse" data-target="#collapseStudent"
                        aria-expanded="true" aria-controls="collapseStudent">
                        <i class="fas fa-user-graduate"></i>
                        <span>Student Management</span>
                    </a>
                    <div id="collapseStudent" class="collapse" aria-labelledby="headingUtilities"
                        data-parent="#accordionSidebar">
                        <div class="bg-white py-2 collapse-inner rounded">
                            <h6 class="collapse-header">Student Management:</h6>
                            <a class="collapse-item" href="../../public/student/student-list.html">Student List</a>
                            <a class="collapse-item" href="../../public/student/add-student.html">Add New Student</a>
                        </div>
                    </div>
                </li>

                <!-- Divider -->
                <hr class="sidebar-divider" />

                <!-- Heading -->
                <div class="sidebar-heading">Class</div>

                <!-- Nav Item - Class Management Collapse Menu -->
                <li class="nav-item">
                    <a class="nav-link collapsed" href="#" data-toggle="collapse" data-target="#collapseClass"
                        aria-expanded="true" aria-controls="collapseClass">
                        <i class="fas fa-chalkboard-teacher"></i>
                        <span>Class Management</span>
                    </a>
                    <div id="collapseClass" class="collapse" aria-labelledby="headingUtilities"
                        data-parent="#accordionSidebar">
                        <div class="bg-white py-2 collapse-inner rounded">
                            <h6 class="collapse-header">Class Management:</h6>
                            <a class="collapse-item" href="../../public/class/class-management.html">Class List</a>
                        </div>
                    </div>
                </li>

                <!-- Divider -->
                <hr class="sidebar-divider" />

                <!-- Heading -->
				<div class="sidebar-heading">Spending</div>

                <!-- Nav Item - Spending -->
				<li class="nav-item">
					<a class="nav-link" href="../../public/spending/spending-management.html">
                        <i class="fas fa-dollar-sign"></i>
						<span>Spending Management</span></a
					>
				</li>

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

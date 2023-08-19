class Footer extends HTMLElement {
	constructor() {
		super();
	}

	connectedCallback() {
		this.innerHTML = 
            `<footer class="sticky-footer bg-white">
                <div class="container my-auto">
                    <div class="copyright text-center my-auto">
                        <span>Copyright &copy; Your Website </span>
                    </div>
                </div>
            </footer>`;

		const d = new Date();
		let year = d.getFullYear();
		$(".copyright").append(year);
	}
}

customElements.define("footer-component", Footer);

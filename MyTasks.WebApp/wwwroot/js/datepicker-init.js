function initDatepicker(selector) {
	$(selector).datepicker({
		format: "dd-mm-yyyy",
		language: "pl",
		multidate: false,
		autoclose: true,
		todayHighlight: true,
		forceParse: false
	});
}
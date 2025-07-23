function deleteRowFromTable(id, btn, url, message) {
	if (!confirm(message)) {
		return;
	}

	$.ajax({
		type: "POST",
		url: url,
		data: {
			id: id
		},
		success: function (data) {
			if (data.success) {
				var row = btn.parentNode.parentNode;
				row.parentNode.removeChild(row);
			} else {
				alert(data.message);
			}
		},
		error: function (data) {
			alert(data.message || "Wystąpił błąd.");
		},
		dataType: "json"
	});
}
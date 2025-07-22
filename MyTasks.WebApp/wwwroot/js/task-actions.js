function handleSubmit(form, table, url)
{
	// jeśli na formularzu zostanie kliknięty przycisk Submit, to zostanie wywołana ta funkcja:
	$(form).submit(function () {
		$.ajax({
				type: "POST",

				// akcja POST zwróci widok cząstkowy, który później jest użyty jako data w success
				url: url,

				// przekazanie danych - zserializowane dane z formularza (this oznacza tutaj właśnie formularz)
				data: $(this).serialize(),

				// typ argumentu funkcji - "data" określony jest na końcu jako "dataType"
				// w data jest odpowiedź zwrócona z kontrolera
				success: function (data) {
					// to jest tabela w partial view _TasksTable
					$(table).html(data);
				},

				error: function (data) {
					alert(data.message);
				},

				// typ trzeba określić jako html, bo ten ajax zwróci cały widok html
				// typ zwracany jest używany jako argument "data" w wywołaniu funkcji jeśli success albo error
				dataType: "html"
			});

		// to jest potrzebne żeby nie wywoływała sie akcja submit, a dokładnie żeby tylko wykonał się ajax i koniec,
		// bo domyślnie każdy submit ma ustawione jakieś akcje, które bez dodania tego zostały by dodatkowo wywołane
		return false;
	});
}

function deleteOrFinishTask(isDelete, taskId, btn, url)
{
	let message = isDelete ? 'Czy na pewno chcesz usunąć zadanie?' : 'Czy na pewno chcesz oznaczyć zadanie jako zrealizowane?';

	if (!confirm(message)) {
		return;
	}

	$.ajax({
		type: "POST",
		url: url,
		data: {
			// pierwsze taskId to nazwa parametru w akcji kontrolera, drugie taskId to argument przekazany do ajaxa w funkcji deleteTask
			taskId: taskId
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
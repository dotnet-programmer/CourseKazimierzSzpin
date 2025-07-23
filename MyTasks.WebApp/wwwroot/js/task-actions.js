function handleSubmit(form, table, url) {
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
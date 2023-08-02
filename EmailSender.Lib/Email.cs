using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using EmailSender.Lib.Extensions;

namespace EmailSender.Lib;

public class Email
{
	private readonly string _hostSmtp;
	private readonly bool _enableSsl;
	private readonly int _port;
	private readonly string _senderEmail;
	private readonly string _senderEmailPassword;
	private readonly string _senderName;

	private SmtpClient _smtp;
	private MailMessage _mail;

	public Email(EmailParams emailParams)
	{
		_hostSmtp = emailParams.HostSmtp;
		_enableSsl = emailParams.EnableSsl;
		_port = emailParams.Port;
		_senderEmail = emailParams.SenderEmail;
		_senderEmailPassword = emailParams.SenderEmailPassword;
		_senderName = emailParams.SenderName;
	}

	public async Task Send(string subject, string body, string to)
	{
		using (_mail = new MailMessage())
		using (_smtp = new SmtpClient())
		{
			_mail.To.Add(new MailAddress(to));
			_mail.From = new MailAddress(_senderEmail, _senderName);
			_mail.IsBodyHtml = true;
			_mail.Subject = subject;
			_mail.BodyEncoding = Encoding.UTF8;
			_mail.SubjectEncoding = Encoding.UTF8;
			_mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(body.StripHTML(), null, MediaTypeNames.Text.Plain));
			_mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html));

			_smtp.Host = _hostSmtp;
			_smtp.EnableSsl = _enableSsl;
			_smtp.Port = _port;
			_smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
			_smtp.UseDefaultCredentials = false;
			_smtp.Credentials = new NetworkCredential(_senderEmail, _senderEmailPassword);

			await _smtp.SendMailAsync(_mail);
		}
	}
}
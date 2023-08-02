using System;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Timers;
using EmailSender.NetFramework.Lib;
using SendReport.Core.NetFramework.Lib;
using SendReport.Core.NetFramework.Lib.Repositories;
using TextEncryption.NetFramework.Lib;

namespace SendReport.ServiceApp
{
	public partial class ReportService : ServiceBase
	{
		private const string NotEncryptedPasswordPrefix = "encrypt:";
		private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

		private readonly int _intervalInMinutes;
		private readonly int _sendHour;
		private readonly bool _isReportSend;
		private readonly Timer _timer;
		private readonly Email _email;
		private readonly string _emailReceiver;
		private readonly ErrorRepository _errorRepository = new ErrorRepository();
		private readonly ReportRepository _reportRepository = new ReportRepository();
		private readonly GenerateHtmlEmail _htmlEmail = new GenerateHtmlEmail();
		private readonly StringCipher _stringCipher = new StringCipher("4838731F-FC44-40B9-9952-EE5CCB6C198E");

		public ReportService()
		{
			InitializeComponent();

			try
			{
				_timer = new Timer(_intervalInMinutes * 60000);

				_emailReceiver = GetStringValueFromConfig("ReceiverEmail");
				_sendHour = GetIntValueFromConfig("SentHour");
				_isReportSend = GetBoolValueFromConfig("IsReportSend");
				_intervalInMinutes = GetIntValueFromConfig("IntervalInMinutes");

				_email = new Email(new EmailParams
				{
					HostSmtp = GetStringValueFromConfig("HostSmtp"),
					Port = GetIntValueFromConfig("Port"),
					EnableSsl = GetBoolValueFromConfig("EnableSsl"),
					SenderName = GetStringValueFromConfig("SenderName"),
					SenderEmail = GetStringValueFromConfig("SenderEmail"),
					SenderEmailPassword = DecryptSenderEmailPassword(),
				});
			}
			catch (Exception ex)
			{
				Logger.Error(ex, ex.Message);
				throw;
			}
		}

		protected override void OnStart(string[] args)
		{
			_timer.Elapsed += DoWork;
			_timer.Start();
			Logger.Info("Service started...");
		}

		protected override void OnStop() => Logger.Info("Service stopped...");

		private static string GetStringValueFromConfig(string key) => ConfigurationManager.AppSettings[key];

		private static int GetIntValueFromConfig(string key) => int.Parse(ConfigurationManager.AppSettings[key]);

		private static bool GetBoolValueFromConfig(string key) => Convert.ToBoolean(ConfigurationManager.AppSettings[key]);

		private string DecryptSenderEmailPassword()
		{
			var encryptedPassword = GetStringValueFromConfig("SenderEmailPassword");
			if (encryptedPassword.StartsWith(NotEncryptedPasswordPrefix))
			{
				encryptedPassword = _stringCipher.Encrypt(encryptedPassword.Replace(NotEncryptedPasswordPrefix, string.Empty));
				var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
				configFile.AppSettings.Settings["SenderEmailPassword"].Value = encryptedPassword;
				configFile.Save();
			}
			return _stringCipher.Decrypt(encryptedPassword);
		}

		private async void DoWork(object sender, ElapsedEventArgs e)
		{
			try
			{
				await SendError();
				await SendReport();
			}
			catch (Exception ex)
			{
				Logger.Error(ex, ex.Message);
				throw;
			}
		}

		private async Task SendError()
		{
			var errors = _errorRepository.GetLastErrors(_intervalInMinutes);

			if (errors == null || !errors.Any())
			{
				return;
			}

			await _email.Send("Błędy w aplikacji", _htmlEmail.GenerateErrors(errors, _intervalInMinutes), _emailReceiver);

			Logger.Info("Error sent.");
		}

		private async Task SendReport()
		{
			if (!_isReportSend)
			{
				return;
			}

			var actualHour = DateTime.Now.Hour;
			if (actualHour < _sendHour)
			{
				return;
			}

			var report = _reportRepository.GetLastNotSentReport();
			if (report == null)
			{
				return;
			}

			await _email.Send("Raport dobowy", _htmlEmail.GenerateReport(report), _emailReceiver);

			_reportRepository.ReportSent(report);
			Logger.Info("Report sent.");
		}
	}
}
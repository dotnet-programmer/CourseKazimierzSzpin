﻿using SendEmail.WebApp.Core.Models.Domains;

namespace SendEmail.WebApp.Core.Repositories;

public interface ISettingsRepository
{
	Settings? GetSettings(string userId);
	void AddSettings(Settings settings);
	void UpdateSettings(Settings settings);
}
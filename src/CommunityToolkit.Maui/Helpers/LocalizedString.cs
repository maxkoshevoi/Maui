﻿#nullable enable
using CommunityToolkit.Maui.ObjectModel;
using Microsoft.Maui.Controls.Internals;
using System;

namespace CommunityToolkit.Maui.Helpers
{
    public class LocalizedString : ObservableObject
	{
		readonly Func<string> generator;

		public LocalizedString(Func<string> generator)
			: this(LocalizationResourceManager.Current, generator)
		{
		}

		public LocalizedString(LocalizationResourceManager localizationManager, Func<string> generator)
		{
			this.generator = generator;

			// This instance will be unsubscribed and GCed if no one references it
			// since LocalizationResourceManager uses WeekEventManger
			localizationManager.PropertyChanged += (sender, e) => OnPropertyChanged(null);
		}

		[Preserve(Conditional = true)]
		public string Localized => generator();

		[Preserve(Conditional = true)]
		public static implicit operator LocalizedString(Func<string> func) => new(func);
	}
}

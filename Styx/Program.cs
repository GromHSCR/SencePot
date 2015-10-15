﻿using System;
using System.Windows;
using Styx.GromHSCR.CompositionBase;

namespace Styx
{
	class Program
	{
		[STAThreadAttribute]
		static void Main(string[] args)
		{
			try
			{
				Composition.RegisterCatalogsFromConfig();

				var app = Composition.ComposeParts(Composition.Resolve<Application>());
				app.Run();
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}
	}
}

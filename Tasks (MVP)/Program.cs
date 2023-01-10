using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tasks__MVP_
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			//string path = @"C:\Users\shuka\Downloads\sqlite-tools-win32-x86-3360000\sqlite-tools-win32-x86-3360000";
			////string connectionString = $@"add name=""JOBSEntities"" connectionString=""metadata = res://*/JobsModel.csdl|res://*/JobsModel.ssdl|res://*/JobsModel.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=(LocalDB)\MSSQLLocalDB;attachdbfilename={path};integrated security=True;connect timeout=30;MultipleActiveResultSets=True;App=EntityFramework&quot;"" providerName=""System.Data.EntityClient""";
			//AppDomain.CurrentDomain.SetData("DataDirectory", path);

			//using (var context = new JobsEntities())
			//{
			//	Console.WriteLine(context.Jobs.First().ToString());
			//}

			MainPresenter presenter = new MainPresenter(new MainView());
			presenter.StartApp();
		}
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tasks__MVP_
{
	public partial class MainView : Form, IMainView
	{
		public MainView()
		{
			InitializeComponent();
		}

		public void StartApp()
		{
			Application.Run(this);
		}

		Form SetupForm(Form frm)
		{
			frm.MdiParent = this;
			frm.FormBorderStyle = FormBorderStyle.None;
			frm.Dock = DockStyle.Fill;

			return frm;
		}

	#region Opening Forms
		//public IStartupView GetStartupView()
		//{
		//	return SetupForm(new frmStartupView()) as IStartupView;
		//}
	}
	#endregion
}

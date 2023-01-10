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
	public partial class ViewTasksView : Form, IViewTasksView
	{
		public ViewTasksView()
		{
			InitializeComponent();
		}

		public event EventHandler CloseRequested;

		public void CloseView()
		{
			Close();
		}

		public void ShowView()
		{
			Show();
		}
	}
}

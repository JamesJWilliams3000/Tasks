using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks__MVP_
{
	public class MainPresenter
	{
		public IMainView MainView;

		public void StartApp()
		{
			MainView.StartApp();
		}

		public MainPresenter(IMainView mainView)
		{
			MainView = mainView;
		}


	}
}

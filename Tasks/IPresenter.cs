using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks__MVP_
{
	public interface IPresenter
	{
		void Show();
		void Close();
		event EventHandler PresenterClosing;
	}
}

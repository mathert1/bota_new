using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfTest.GUI
{
	/// <summary>
	/// Interaction logic for ChatControl.xaml
	/// </summary>
	public partial class ChatControl : UserControl
	{
		public ChatControl(int position)
		{
			InitializeComponent();
			this.Margin = new System.Windows.Thickness { Left = (position*50) }; 
		}

		private void sendBox_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				e.Handled = true;
				TabItem t = (TabItem)((FrameworkElement)((FrameworkElement)sendBox.Parent).Parent).Parent;
				ServerConnection.SendMessage(t.Name, sendBox.Text);				
				ChatBox.AppendText("You: "+sendBox.Text+"\r");
				ChatBox.ScrollToEnd();
				sendBox.Text = "";
			}
		}

		public void AddMessage(string name, string msg)
		{
			ChatBox.AppendText(name + ": " + msg + "\r");
			ChatBox.ScrollToEnd();
		}
	}
}

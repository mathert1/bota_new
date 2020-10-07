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
using System.Windows.Shapes;
using MSCommon;
using WpfTest.GUI;
using WpfTest.Controls;

namespace WpfTest
{
	/// <summary>
	/// Interaction logic for MainScreen.xaml
	/// </summary>
	public partial class MainScreen : Window
	{
		private List<TabItem> _ChatTabItems;
		CharControl m_game;
		
		public MainScreen()
		{
			InitializeComponent();

			this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

			_ChatTabItems = new List<TabItem>();

            buddyListBox.Items.Clear();			
			AddTabItem("General");			
			_chatTabControl.SelectedIndex = 0;			

			//m_game = new CharacterControl(xnaCharControl.Handle);			
		}


		private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.DragMove();
		}

        //private void btnQuit_click(object sender, RoutedEventArgs e)
        //{
        //    screenMan.CloseMainScreen();			
        //}

		private void _chatTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			TabItem tab = _chatTabControl.SelectedItem as TabItem;

			if (tab != null && tab.Header != null)
			{
				ShowChat(true);                             
			}
		}

		private delegate void AddTabItemer(string name);
		private void AddTabItem(string Name)
		{
			if (this._chatTabControl.Dispatcher.CheckAccess())
			{
				bool chatExists = false;

				var tab = FindVisualChildren<TabItem>(_chatTabControl).ToList();
				List<TabItem> tabItems = tab as List<TabItem>;
				foreach (TabItem t in tabItems)
				{
					if (t.Name == Name)
					{
						chatExists = true;
						ShowChat(true);
						break;

					}
				}
				if (!chatExists)
				{
					TabItem tempTab = new TabItem();
					tempTab.Header = Name;
					tempTab.Name = Name;
					// add controls to tab item
					tempTab.Content = new ChatControl(_ChatTabItems.Count);
					tempTab.Visibility = System.Windows.Visibility.Visible;
					// insert tab item right before the last (+) tab item
					_ChatTabItems.Add(tempTab);

					_chatTabControl.DataContext = null;
					_chatTabControl.DataContext = _ChatTabItems;

					_chatTabControl.SelectedIndex = _chatTabControl.Items.Count - 1;

					//ShowChat(true); Commented out so chat starts up minimized.
				}
			}
			else
			{
				this._chatTabControl.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
					new AddTabItemer(this.AddTabItem), Name);
			}
			int count = _ChatTabItems.Count;

			// create new tab item
			
		}

		private void btnDelete_Click(object sender, RoutedEventArgs e)
		{
			string tabName = (sender as Button).CommandParameter.ToString();

			var item = _chatTabControl.Items.Cast<TabItem>().Where(i => i.Name.Equals(tabName)).SingleOrDefault();

			TabItem tab = item as TabItem;

			if (tab != null)
			{
				if (tab.Name.Equals("General"))
				{
					MessageBox.Show("Cannot remove the General tab.");
				}
				else if (MessageBox.Show(string.Format("Are you sure you want to remove the tab '{0}'?", tab.Header.ToString()),
					"Remove Tab", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
				{
					// get selected tab
					TabItem selectedTab = _chatTabControl.SelectedItem as TabItem;

					// clear tab control binding
					_chatTabControl.DataContext = null;

					_ChatTabItems.Remove(tab);

					// bind tab control
					_chatTabControl.DataContext = _ChatTabItems;

					// select previously selected tab. if that is removed then select first tab
					if (selectedTab == null || selectedTab.Equals(tab))
					{
						selectedTab = _ChatTabItems[0];
					}
					_chatTabControl.SelectedItem = selectedTab;
				}
			}
		}

		private void TabItemMouseClick(object sender, RoutedEventArgs e)
		{
            if (_chatTabControl.Height > 35.75)
            {
				ShowChat(false);
            }
            else
            {
				ShowChat(true);
            }
		}

		private delegate void AddMessager(string chat, string name, string msg);
		public void AddMessage(string chat, string name, string msg)
		{
			if (this._chatTabControl.Dispatcher.CheckAccess())
			{
                bool chatExists = false;

				var tab = FindVisualChildren<TabItem>(_chatTabControl).ToList();
				List<TabItem> tabItems = tab as List<TabItem>;
				foreach (TabItem t in tabItems)
				{
					if (t.Name == chat)
					{
                        chatExists = true;
						ChatControl cControl = (ChatControl)t.Content;
						cControl.AddMessage(name, msg);
                        
					}
				}
                if (!chatExists)
                {
                    AddTabItem(name);
					tab = FindVisualChildren<TabItem>(_chatTabControl).ToList();
					tabItems = tab as List<TabItem>;
                    foreach (TabItem t in tabItems)
                    {
						if (t.Name == chat)
                        {                            
                            ChatControl cControl = (ChatControl)t.Content;
                            cControl.AddMessage(name, msg);

                        }
                    }
                }
			}
			else
			{
				this._chatTabControl.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
					new AddMessager(this.AddMessage), chat, name, msg);
			}
			
		}

		private delegate void GameSearchOverlayShower();
		public void ShowGameSearchOverlay()
		{
			if (this.gameSearchOverlay.Dispatcher.CheckAccess())
			{
				gameSearchOverlay.Visibility = System.Windows.Visibility.Visible;
			}
			else
			{
				this.gameSearchOverlay.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
					new GameSearchOverlayShower(this.ShowGameSearchOverlay));
			}
		}

		private delegate void BuddyListChanger(List<string> buddyList);
        public void RefreshBuddyList(List<string> buddyList)
        {            
			if (this.buddyListBox.Dispatcher.CheckAccess())
            {
				buddyListBox.Items.Clear();
                foreach (string b in buddyList)
                {
					buddyListBox.Items.Add(b);
                }
            }
            else
            {
				this.buddyListBox.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                        new BuddyListChanger(this.RefreshBuddyList), buddyList);
            }            
        }

		private delegate void SetCharacterInfoer(Player p);
		public void SetCharacterInfo(Player p)
		{
			if (this.nameTxtBox.Dispatcher.CheckAccess())
			{
				int t; int s; int avh; int avs; int avc;

                t = p.Tank;
                s = p.Skin;
                avh = p.AvaHead;
                avs = p.AvaShoulder;
                avc = p.AvaChest;

               	UpgradeObject tank; UpgradeObject skin; UpgradeObject avaHead; UpgradeObject avaShoulder; UpgradeObject avaChest;
				string stank; string sskin; string savaHead; string savaShoulder; string savaChest;

				tank = TextFileHelper.GetUpgradeObjectFromTextFile(t, "Tanks");
				skin = TextFileHelper.GetUpgradeObjectFromTextFile(s, "Skins");
				avaHead = TextFileHelper.GetUpgradeObjectFromTextFile(avh, "AvatarHead");
				avaShoulder = TextFileHelper.GetUpgradeObjectFromTextFile(avs, "AvatarShoulder");
				avaChest = TextFileHelper.GetUpgradeObjectFromTextFile(avc, "AvatarChest");
				
				nameTxtBox.Text = p.UserName;
				rankTxtBox.Text = p.Rank.ToString();
                winsTxtBox.Text = p.Wins.ToString();
                lossesTxtBox.Text = p.Losses.ToString();
				GoldTxtBlk.Text = p.Gold.ToString();
				AttackTxtBlk.Text = (tank.Attack + skin.Attack + avaHead.Attack + avaShoulder.Attack + avaChest.Attack).ToString();
				DefenseTxtBlk.Text = (tank.Defense + skin.Defense + avaHead.Defense + avaShoulder.Defense + avaChest.Defense).ToString();
				HealthTxtBlk.Text = (tank.Health + skin.Health + avaHead.Health + avaShoulder.Health + avaChest.Health).ToString();
				MoveTxtBlk.Text = (tank.Move + skin.Move + avaHead.Move + avaShoulder.Move + avaChest.Move).ToString();
				ShieldTxtBlk.Text = (tank.Shield + skin.Shield + avaHead.Shield + avaShoulder.Shield + avaChest.Shield).ToString();
				PopularityTxtBlk.Text = (tank.Popularity + skin.Popularity + avaHead.Popularity + avaShoulder.Popularity + avaChest.Popularity).ToString();

				stank = tank.FilePath.Replace(".png", "");
				sskin = skin.FilePath.Replace(".png", "");
				savaHead = avaHead.FilePath.Replace(".png", "");
				savaShoulder = avaShoulder.FilePath.Replace(".png", "");
				savaChest = avaChest.FilePath.Replace(".png", "");
				
				if(m_game != null)
					m_game.Dispose();
				m_game = new CharControl(stank,sskin,savaHead,savaShoulder,savaChest,xnaCharControl.Handle);
			}
			else
			{
				this.nameTxtBox.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
						new SetCharacterInfoer(this.SetCharacterInfo), p);
			}
		}

		public IEnumerable<T> FindVisualChildren<T>(DependencyObject rootObject) where T : DependencyObject
		{
			if (rootObject != null)
			{
				for (int i = 0; i < VisualTreeHelper.GetChildrenCount(rootObject); i++)
				{
					DependencyObject child = VisualTreeHelper.GetChild(rootObject, i);

					if (child != null && child is T)
						yield return (T)child;

					foreach (T childOfChild in FindVisualChildren<T>(child))
						yield return childOfChild;
				}				
			}
		}		

		private void addBuddyBtn_Click(object sender, RoutedEventArgs e)
		{
            if (addBuddyPanel.Visibility == System.Windows.Visibility.Visible)
            {
                buddyTxtBox.Text = "";
                addBuddyPanel.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                addBuddyPanel.Visibility = System.Windows.Visibility.Visible;
                buddyTxtBox.Focus();
                //buddyTxtBox.SelectionStart = 0;
            }
		}

		private void addBtn_Click(object sender, RoutedEventArgs e)
		{
			ServerConnection.AddBuddy(buddyTxtBox.Text);
			buddyTxtBox.Text = "";
			addBuddyPanel.Visibility = System.Windows.Visibility.Hidden;
		}

		private void buddyListBox_MouseDoubleClick(object sender, RoutedEventArgs e)
		{
			ListBoxItem nm = new ListBoxItem();
			nm = sender as ListBoxItem;			
			AddTabItem(nm.Content.ToString());
		}

		private void ShowChat(bool show)
		{
			if (!show)
			{
				_chatTabControl.Height = 35.75;
			}
			else
			{
				_chatTabControl.Height = 342;
			}
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			ServerConnection.RequestBuddyList();
			ServerConnection.RequestCharacterInfo();
		}

        private void buddyTxtBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
			if (e.Key == Key.Return)
			{
				e.Handled = true;
				ServerConnection.AddBuddy(buddyTxtBox.Text);
				buddyTxtBox.Text = "";
				addBuddyPanel.Visibility = System.Windows.Visibility.Hidden;
			}
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            Variables.screenPos = new Tuple<double, double>(this.Left, this.Top);
            //screenMan.OpenEditCharacterWindow();
        }

		private void Window_ContentRendered(object sender, EventArgs e)
		{
			ShowChat(false);
		}

		private void playBtn_Click(object sender, RoutedEventArgs e)
		{
			Variables.screenPos = new Tuple<double, double>(this.Left, this.Top);
			bool bots = false;
			if ((bool)botsCheckBox.IsChecked)
				bots = true;
			//screenMan.OpenGameWindow();
			ServerConnection.StartGame(bots);
		}

		private void playersComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Variables.numPlayers = playersComboBox.SelectedIndex;
		}

		private void cancelBtn_Click(object sender, RoutedEventArgs e)
		{
			gameSearchOverlay.Visibility = System.Windows.Visibility.Hidden;
			ServerConnection.CancelStartGame();
		}		
	}
}

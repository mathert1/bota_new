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
using System.Collections.ObjectModel;
using MSCommon;

namespace WpfTest
{
	/// <summary>
	/// Interaction logic for ShopWindow.xaml
	/// </summary>
	public partial class ShopWindow : Window
	{
		ObservableCollection<UpgradeObject> AvatarHeadList = new ObservableCollection<UpgradeObject>();
		ObservableCollection<UpgradeObject> AvatarChestList = new ObservableCollection<UpgradeObject>();
		ObservableCollection<UpgradeObject> AvatarShoulderList = new ObservableCollection<UpgradeObject>();
		ObservableCollection<UpgradeObject> SkinList = new ObservableCollection<UpgradeObject>();
		ObservableCollection<UpgradeObject> TankList = new ObservableCollection<UpgradeObject>();

        public int tempTank = 0;
        public int tempSkin = 0;
        public int tempAvaHead = -1;
        public int tempAvaShoulder = -1;
        public int tempAvaChest = -1;

        Player player = new Player();

		CharControl m_game;
		ScreenManager screenMan;

		public ShopWindow(ScreenManager SM, double left, double top, Player p)
		{
			InitializeComponent();
            player = p;
			screenMan = SM;
			this.WindowStartupLocation = WindowStartupLocation.Manual;
			this.Left = left;
			this.Top = top;
		}

		public void UpdateChar()
		{
            SetCharacterInfo(player);
			//screenMan.SetCharacterInfo(player);
		}

		public void PurchaseItem(int type, int ID, int goldCost)
		{
            if (goldCost < player.Gold)
            {
                screenMan.PurchaseItem(type, ID);
            }
            else
                MessageBox.Show("You don't have enough gold.", "Uh oh!");			
		}

		private void SetUpTanks()
		{
			TankList.Clear();
			TankList = TextFileHelper.GetUpgradeObjectListFormattedFilePathFromTextFile("Tanks");
			TankListBox.ItemsSource = TankList;
		}

		private void SetUpAvaHead()
		{
			AvatarHeadList.Clear();
			AvatarHeadList = TextFileHelper.GetUpgradeObjectListFormattedFilePathFromTextFile("AvatarHead");
			AvaHeadListBox.ItemsSource = AvatarHeadList;
		}

		private void SetUpAvaChest()
		{
			AvatarChestList.Clear();
			AvatarChestList = TextFileHelper.GetUpgradeObjectListFormattedFilePathFromTextFile("AvatarChest");
			AvaChestListBox.ItemsSource = AvatarChestList;
		}

		private void SetUpAvaShoulder()
		{
			AvatarShoulderList.Clear();
			AvatarShoulderList = TextFileHelper.GetUpgradeObjectListFormattedFilePathFromTextFile("AvatarShoulder");
			AvaShoulderListBox.ItemsSource = AvatarShoulderList;
		}

		private void SetUpSkins()
		{
			SkinList.Clear();
			SkinList = TextFileHelper.GetUpgradeObjectListFormattedFilePathFromTextFile("Skins");
			SkinListBox.ItemsSource = SkinList;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			SetUpTanks();
			UpdateChar();
		}

		private void HeadHeader_GotFocus(object sender, RoutedEventArgs e)
		{
			if(AvatarHeadList.Count==0)
				SetUpAvaHead();			
		}

		private void ChestHeader_GotFocus(object sender, RoutedEventArgs e)
		{
			if (AvatarChestList.Count == 0)
				SetUpAvaChest();
		}

		private void ShoulderHeader_GotFocus(object sender, RoutedEventArgs e)
		{
			if (AvatarShoulderList.Count == 0)
				SetUpAvaShoulder();
		}

		private void SkinHeader_GotFocus(object sender, RoutedEventArgs e)
		{
			if (SkinList.Count == 0)
				SetUpSkins();
		}

		private void doneBtn_Click(object sender, RoutedEventArgs e)
		{
            ClearTempVars();			
		}

		private delegate void SetCharacterInfoer(Player p);
		public void SetCharacterInfo(Player p)
		{
			if (this.GoldTxtBlk.Dispatcher.CheckAccess())
			{
                player = p;
				//Checks if there are temp variables for all avatar/tank/skins and sends the correct item to 'Game1'
				int t; int s; int avh; int avs; int avc;

                if (tempTank != 0)
                    t = tempTank;
                else
                    t = p.Tank;
                if (tempSkin != 0)
                    s = tempSkin;
                else
                    s = p.Skin;
                if (tempAvaHead > -1)
                    avh = tempAvaHead;
                else
                    avh = p.AvaHead;
                if (tempAvaShoulder > -1)
                    avs = tempAvaShoulder;
                else
                    avs = p.AvaShoulder;
                if (tempAvaChest > -1)
                    avc = tempAvaChest;
                else
                    avc = p.AvaChest;
				UpgradeObject tank; UpgradeObject skin; UpgradeObject avaHead; UpgradeObject avaShoulder; UpgradeObject avaChest;
				string stank; string sskin; string savaHead; string savaShoulder; string savaChest;

				tank = TextFileHelper.GetUpgradeObjectFromTextFile(t, "Tanks");
				skin = TextFileHelper.GetUpgradeObjectFromTextFile(s, "Skins");
				avaHead = TextFileHelper.GetUpgradeObjectFromTextFile(avh, "AvatarHead");
				avaShoulder = TextFileHelper.GetUpgradeObjectFromTextFile(avs, "AvatarShoulder");
				avaChest = TextFileHelper.GetUpgradeObjectFromTextFile(avc, "AvatarChest");

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

				if (m_game != null)
					m_game.Dispose();
				m_game = new CharControl(stank, sskin, savaHead, savaShoulder, savaChest, xnaCharControl.Handle);
			}
			else
			{
				this.GoldTxtBlk.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
						new SetCharacterInfoer(this.SetCharacterInfo), p);
			}
		}

        private void ClearTempVars()
        {
            tempTank = 0;
            tempSkin = 0;
            tempAvaHead = -1;
            tempAvaChest = -1;
            tempAvaShoulder = -1;
        }
	}
}

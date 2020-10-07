using System;
using System.IO;
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
//using System.Windows.Shapes;
using System.Collections.ObjectModel;
using MSCommon;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for EditCharacterWindow.xaml
    /// </summary>
    public partial class EditCharacterWindow : Window
    {
        ObservableCollection<UpgradeObject> AvatarHeadList = new ObservableCollection<UpgradeObject>();
        ObservableCollection<UpgradeObject> AvatarChestList = new ObservableCollection<UpgradeObject>();
        ObservableCollection<UpgradeObject> AvatarShoulderList = new ObservableCollection<UpgradeObject>();
        ObservableCollection<UpgradeObject> SkinList = new ObservableCollection<UpgradeObject>();
        ObservableCollection<UpgradeObject> TankList = new ObservableCollection<UpgradeObject>();
        //ObservableCollection<CharTank> myTankList = new ObservableCollection<CharTank>();

        public int tempTank = 0;
        public int tempSkin = 0;
        public int tempAvaHead = -1;
        public int tempAvaShoulder = -1;
        public int tempAvaChest = -1;

        Player player = new Player();

        CharControl m_game;

        public EditCharacterWindow(double left, double top, Player p)//, int top, int left)
        {
            InitializeComponent();
            player = p;

            this.WindowStartupLocation = WindowStartupLocation.Manual;
            this.Left = left;
            this.Top = top;
            ServerConnection.RequestMyTanks();
            ServerConnection.RequestMyAvatar();
        }

        private delegate void TankListSetter(List<CharTank> tankList);
        public void SetUpTankList(List<CharTank> tankList)
        {
            if (this.tankListBox.Dispatcher.CheckAccess())
            {
                //myTankList.Clear();
                TankList.Clear();
                foreach (CharTank CT in tankList)
                {
                    UpgradeObject uO = TextFileHelper.GetUpgradeObjectFromTextFile(CT.ID, "Tanks");
                    uO.FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Content\\", uO.FilePath);
                    TankList.Add(uO);
                    //myTankList.Add(CT);
                }

                tankListBox.Items.Clear();
                tankListBox.ItemsSource = TankList;
            }
            else
            {
                this.tankListBox.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                    new TankListSetter(this.SetUpTankList), tankList);
            }
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

        private delegate void AvaListSetter(List<int> avaList);
        public void SetUpAvaList(List<int> avaList)
        {
            if (this.avatarTabItem.Dispatcher.CheckAccess())
            {
                AvatarHeadList.Clear();
                AvatarShoulderList.Clear();
                AvatarChestList.Clear();
                SkinList.Clear();
                int count = avaList.Count();
                for (int i = 0; i < count; i += 2)
                {
                    int avaID = avaList[i];
                    int type = avaList[i + 1];
                    if (type == 1)
                    {
                        UpgradeObject uO = TextFileHelper.GetUpgradeObjectFromTextFile(avaID, "AvatarHead");
                        uO.FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Content\\", uO.FilePath);
                        AvatarHeadList.Add(uO);
                    }
                    else if (type == 2)
                    {
                        UpgradeObject uO = TextFileHelper.GetUpgradeObjectFromTextFile(avaID, "AvatarShoulder");
                        uO.FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Content\\", uO.FilePath);
                        AvatarShoulderList.Add(uO);
                    }
                    else if (type == 3)
                    {
                        UpgradeObject uO = TextFileHelper.GetUpgradeObjectFromTextFile(avaID, "AvatarChest");
                        uO.FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Content\\", uO.FilePath);
                        AvatarChestList.Add(uO);
                    }
                    else
                    {
                        UpgradeObject uO = TextFileHelper.GetUpgradeObjectFromTextFile(avaID, "Skins");
                        uO.FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Content\\", uO.FilePath);
                        SkinList.Add(uO);
                    }
                }

                AvaHeadListBox.ItemsSource = AvatarHeadList;
                AvaChestListBox.ItemsSource = AvatarChestList;
                AvaShoulderListBox.ItemsSource = AvatarShoulderList;
                SkinListBox.ItemsSource = SkinList;
            }
            else
            {
                this.avatarTabItem.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                    new AvaListSetter(this.SetUpAvaList), avaList);
            }
        }

        //private void SetUpAvatarHead()
        //{
        //    AvatarHeadList.Clear();
        //    AvatarHeadList = TextFileHelper.GetUpgradeObjectListFormattedFilePathFromTextFile("AvatarHead");				
        //    AvaHeadListBox.ItemsSource = AvatarHeadList;
        //}

        //private void SetUpAvatarChest()
        //{
        //    AvatarChestList.Clear();
        //    AvatarChestList = TextFileHelper.GetUpgradeObjectListFormattedFilePathFromTextFile("AvatarChest");			
        //    AvaChestListBox.ItemsSource = AvatarChestList;
        //}

        //private void SetUpAvatarShoulder()
        //{
        //    AvatarShoulderList.Clear();
        //    AvatarShoulderList = TextFileHelper.GetUpgradeObjectListFormattedFilePathFromTextFile("AvatarShoulder");	
        //    AvaShoulderListBox.ItemsSource = AvatarShoulderList;
        //}	

        private void shopBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearTempVars();
            //screenMan.CloseEditCharacterWindow();
            //screenMan.OpenShopWindow();
        }

        private void doneBtn_Click(object sender, RoutedEventArgs e)
        {
            if (tempTank != 0)
                player.Tank = tempTank;            
            if (tempSkin != 0)
                player.Skin = tempSkin;            
            if (tempAvaHead > -1)
                player.AvaHead = tempAvaHead;            
            if (tempAvaShoulder > -1)
                player.AvaShoulder = tempAvaShoulder;           
            if (tempAvaChest > -1)
                player.AvaChest = tempAvaChest;
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearTempVars();
        }

        public void UpdateChar()
        {
            SetCharacterInfo(player);
            //screenMan.SetCharacterInfo(player);
        }

        //private void HeadHeader_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    if(AvatarHeadList.Count == 0)
        //        SetUpAvatarHead();
        //}		

        //private void ChestHeader_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    if (AvatarChestList.Count == 0)
        //        SetUpAvatarChest();
        //}

        //private void ShoulderHeader_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    if (AvatarShoulderList.Count == 0)
        //        SetUpAvatarShoulder();
        //}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateChar();
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

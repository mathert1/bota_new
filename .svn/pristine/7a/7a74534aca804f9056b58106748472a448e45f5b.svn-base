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
using WpfTest.Controls;

namespace WpfTest
{
	/// <summary>
	/// Interaction logic for GameWindow.xaml
	/// </summary>
	public partial class GameWindow : Window
	{
		public Game.Game game;
        GameInfo gameInfo;
        int playerID;

		public GameWindow(GameInfo gI, int pID, double left, double top) 
		{
			InitializeComponent();

            gameInfo = gI;
            playerID = pID;
			this.WindowStartupLocation = WindowStartupLocation.Manual;
			this.Left = left;
			this.Top = top;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			SetUpGame(gameInfo.playerList, gameInfo.mapName, gameInfo.backgroundName, gameInfo.gameID, gameInfo.teamLives, gameInfo.startPlayerID);
			//ServerConnection.StartGame();
            ServerConnection.GameLoaded(gameInfo.gameID);
		}

        private delegate void GameSetter(List<Player> pList, string mapName, string backName, int gID, int teamLives, int startPlayerId);
		public void SetUpGame(List<Player> pList, string mapName, string backName, int gID, int teamLives, int startPlayerId)
		{
            if (this.gameControl1.Dispatcher.CheckAccess())
            {
                int localPlayer = 0;
                int count = 0;
                List<GamePlayer> gamePlayerList = new List<GamePlayer>();
                foreach (Player p in pList)
                {
                    if (p.ID == playerID)
                        localPlayer = count;
                    GamePlayer gP = new GamePlayer();
                    gP.ID = p.ID;
                    gP.Name = p.UserName;
                    gP.Rank = p.Rank;
                    gP.Tank = TextFileHelper.GetFilePathByIDandType(p.Tank, "Tanks");
                    gP.Skin = TextFileHelper.GetFilePathByIDandType(p.Skin, "Skins");
                    gP.AvaHead = TextFileHelper.GetFilePathByIDandType(p.AvaHead, "AvatarHead");
                    gP.AvaShoulder = TextFileHelper.GetFilePathByIDandType(p.AvaShoulder, "AvatarShoulder");
                    gP.AvaChest = TextFileHelper.GetFilePathByIDandType(p.AvaChest, "AvatarChest");
                    gP.Health = p.Health;

                    gP.Tank = gP.Tank.Replace(".png", "");
                    gP.Skin = gP.Skin.Replace(".png", "");
                    gP.AvaHead = gP.AvaHead.Replace(".png", "");
                    gP.AvaShoulder = gP.AvaShoulder.Replace(".png", "");
                    gP.AvaChest = gP.AvaChest.Replace(".png", "");
                    gamePlayerList.Add(gP);
                    count++;
                }
                if (game != null)
                    game.Dispose();

                game = new Game.Game(gamePlayerList, mapName, backName, gID, teamLives, startPlayerId, localPlayer, gameControl1.Handle, ServerConnection.m_client);
            }
            else
            {
                this.gameControl1.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                    new GameSetter(this.SetUpGame), pList, mapName, gID, teamLives, startPlayerId);
            }
		}

        private delegate void GameSearchOverlayShower();
        public void ShowGameOverOverlay()
        {
            if (this.gameOverOverlay.Dispatcher.CheckAccess())
            {
                gameOverOverlay.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                this.gameOverOverlay.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                    new GameSearchOverlayShower(this.ShowGameOverOverlay));
            }
        }
	}
}

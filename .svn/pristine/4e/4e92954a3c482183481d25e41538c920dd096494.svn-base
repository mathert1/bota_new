using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MSCommon;

namespace WpfTest.Game
{
    public class textPopup
    {
        public int PlayerID;
        public Vector2 Position;
        public TextPopupTypes textType;
        public Timer aTimer;
        public int intText;
        public bool elapsed = false;

        public textPopup(int pID, int posX, int posY, int iTxt, int duration, TextPopupTypes type)
        {
            PlayerID = pID;
            Position = new Vector2(posX, posY);
            intText = iTxt;
            textType = type;
            aTimer = new Timer(duration);
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            elapsed = true;
        }
    }
}

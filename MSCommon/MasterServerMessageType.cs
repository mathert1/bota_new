using System;

namespace MSCommon
{
	public enum MasterServerMessageType
	{
		RegisterHost,
		RequestHostList,
		RequestIntroduction,
		RequestGameRooms,
		CreateAccount,
		RequestLogin,
		SendMessage,
		AddBuddy,
		SendBuddyList,
		CharacterInfo,
		UpdateCharacterInfo,
		RequestTanks,
		RequestAvatar,
		PurchaseItem,
		FindingGame,
		StartGame,
		CancelStartGame,
		QuitGame,
		Move,
		CreateRocket,
		ChangeWind,
        RocketCollisionScreen,
		RocketCollisionPlayer,
		RocketCollisionLand,
        KillPlayer,
        DamagePlayer,
        GivePlayerMoney,
		Disconnect,
        EndGame,
        ChangeTurn,
        GameLoaded
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayMode
{
    TwoPlayers, WithAI, TwoAI, Multiplayer
}
public static class GameManager
{
    public static PlayerSkin firstPlayerSkin;
    public static PlayerSkin secondPlayerSkin;
    public static PlayMode CurrentPlayMode;
}

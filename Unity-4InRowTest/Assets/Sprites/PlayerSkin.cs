using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New player skin", menuName = "Skins")]
public class PlayerSkin : ScriptableObject
{
    public string SkinName;
    public Sprite squareSrtite;
    public Sprite roundSprite;
    public float SelectButtonScale;
    public Animation winAnimation;
    
    public override int GetHashCode()
    {
        return (SkinName, squareSrtite, roundSprite).GetHashCode();
    }
    public override bool Equals(object other)
    {
        if(other is PlayerSkin player)
        return this == player;
        else
        return false;
    }
    public static bool operator ==(PlayerSkin first, PlayerSkin second)
    {
        return first?.SkinName == second?.SkinName;
    }
    public static bool operator !=(PlayerSkin first, PlayerSkin second)
    {
        return !(first == second);
    }
}

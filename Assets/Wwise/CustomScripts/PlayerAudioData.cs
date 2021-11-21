using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Audio/Player", fileName = "PlayerAudio")]

public class PlayerAudioData : ScriptableObject
{
    [Header("Ambience")]

    public AK.Wwise.Event playAmbience = null;

    public AK.Wwise.Event stopAmbience = null;

    [Header("Music")]

    public AK.Wwise.Event playMusic = null;

    public AK.Wwise.Event stopMusic = null;

    public AK.Wwise.Event musicWinTrigger = null;

    [Header("UI")]

    public AK.Wwise.Event restartAudio = null;

    public AK.Wwise.Event menuButton = null;

    [Header("Helicopter")]

    public AK.Wwise.Event heliGetIn = null;

    public AK.Wwise.Event heliGetOut = null;

    public AK.Wwise.Event heliGrabObj = null;

    public AK.Wwise.Event heliMoveStart = null;

    public AK.Wwise.Event heliMoveStop = null;

    public AK.Wwise.Event heliReleaseObj = null;

    [Header("Interactive Objects")]

    public AK.Wwise.Event switchButtonOff = null;

    public AK.Wwise.Event switchButtonOn = null;

    public AK.Wwise.Event gateOpen = null;

    [Header("Legs")]

    public AK.Wwise.Event legsFstep = null;

    public AK.Wwise.Event legsGetIn = null;

    public AK.Wwise.Event legsGetOut = null;

    public AK.Wwise.Event legsJump = null;

    public AK.Wwise.Event legsLand = null;

    [Header("Tank")]

    public AK.Wwise.Event tankGetIn = null;

    public AK.Wwise.Event tankGetOut = null;

    public AK.Wwise.Event tankMoveStart = null;

    public AK.Wwise.Event tankMoveStop = null;

    public AK.Wwise.Event tankShot = null;

    public AK.Wwise.Event tankShotExplosion = null;

    [Header("Wheels")]

    public AK.Wwise.Event wheelsMoveStart = null;

    public AK.Wwise.Event wheelsMoveStop = null;








}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne
{
    public enum GameState
    {
        Menu,
        Adventure,
        Fight,
        Paused,
        End
    }


    public enum DialogueState
    {
        INTERACTION,
        INTERACTION_ACTION,
        DRAW,
        DRAW_ACTION
    }
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData{

    public int gems;
    public int highScore;
    public int currentHandIndex;

    public int playedGames;

    public bool adFree;
    public bool adFreeRevive;

    public List<int> unlockedHandIndexes = new List<int>();

    public PlayerData(GameManager gameInstance) {

        gems = gameInstance.gems;
        highScore = gameInstance.highScore;
        currentHandIndex = gameInstance.currentHandIndex;

        playedGames = gameInstance.playedGames;

        adFree = gameInstance.adFree;
        adFreeRevive = gameInstance.adFreeRevive;

        unlockedHandIndexes = new List<int>();

        for (int i = 0; i < gameInstance.handSkins.Length; i++) {

            if (!gameInstance.handSkins[i].isLocked) {

                unlockedHandIndexes.Add(i);

            }

        }

    }

}

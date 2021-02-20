using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Barrier : MonoBehaviour, IAttacknotable
{
    [SerializeField] private int playerId = 0;

   public bool barriered(int hitId)
   {
        if (playerId == 1 && hitId == 1)
        {
            return false;
        }
        else if (playerId == -1 && hitId == -1)
        {
            return false;
        }
        
        return true;
   }

}

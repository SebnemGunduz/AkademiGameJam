using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HisaGames.EasyChara
{
    public class PanelCharaDemo : MonoBehaviour
    {
        [SerializeField] private Animator characterAnimator;

        public void ChangeAnimation(string animationName)
        {
            if (characterAnimator != null)
            {
                characterAnimator.Play(animationName);
            }
            else
            {
                Debug.LogError("Animator is not assigned!");
            }
        }
    }
}
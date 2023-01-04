using System;
using UnityEngine;

namespace TowerDefense.Player.Runtime
{
    [CreateAssetMenu(fileName = "New Player", menuName = "TowerDefense/Player")]
    public sealed class PlayerSO : ScriptableObject
    {
        public event Action OnDead;

        [SerializeField] private int hp;
        
        private int Hp { get; set; }

        private void OnEnable()
        {
            Hp = hp;
        }

        public void ResetHp()
        {
            Hp = hp;
        }

        public void GetDamage(int damage)
        {
            if (Hp - damage > 0)
            {
                Hp -= damage;
                Debug.Log($"HP = {Hp}");
            }
            else
            {
                Hp = 0;
                Debug.Log($"HP = {Hp}");
                OnDead?.Invoke();
            }
        }
    }
}
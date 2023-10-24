using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBehavior : MonoBehaviour
{
        int _score;
        public int Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
                _scoreGui.text = Score.ToString();
            }
        }

        [SerializeField] TextMeshProUGUI _scoreGui;

        int _health;
        public int Health
        {
            get
            {
                return _health;
            }
            set
            {
                _health = value;
                _healthGui.text = Health.ToString();
            }
        }

        [SerializeField] TextMeshProUGUI _healthGui;
}

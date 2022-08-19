using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace UI
{
    public class UIController : MonoBehaviour
    {
        static public Action AddScore;
        static public Action<float> AddDistanse;

        [SerializeField] private TMPro.TextMeshProUGUI _ScoreText;
        [SerializeField] private TMPro.TextMeshProUGUI _DistanseText;

        int _score;
        float _distanse;

        private void Start()
        {
            GetSavedData();

            AddScore += AddToScore;
            AddDistanse += AddToDistanse;

            _ScoreText.text = _score.ToString();
            _DistanseText.text = _distanse.ToString();
        }
        private void OnDestroy()
        {
            AddScore -= AddToScore;
            AddDistanse -= AddToDistanse;
            PlayerPrefs.SetInt("Score", _score);
            PlayerPrefs.SetFloat("Distanse", _distanse);
            PlayerPrefs.Save();
        }

        void AddToDistanse(float value)
        {
            _distanse += value;
            _DistanseText.text = _distanse.ToString();
        }
        void GetSavedData()
        {
            if (PlayerPrefs.HasKey("Score"))
            {
                _score = PlayerPrefs.GetInt("Score");
            }
            else PlayerPrefs.SetInt("Score", 0);

            if (PlayerPrefs.HasKey("Distanse"))
            {
                _distanse = PlayerPrefs.GetFloat("Distanse");
            }
            else PlayerPrefs.SetFloat("Distanse", 0);
        }
        void AddToScore()
        {
            _score++;
            _ScoreText.text = _score.ToString();
        }
    }
}

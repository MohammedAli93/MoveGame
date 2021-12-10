using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maze
{
    public class MazeChallenge : Challenge
    {
        [SerializeField] private Player _player;
        [SerializeField] private GameObject _Maze;

        private Vector3 _playerStartPosition;
        private void Awake()
        {
            _playerStartPosition = _player.transform.position;
        }

        public override void StartChallenge()
        {
            _player.transform.position = _playerStartPosition;
            _player.gameObject.SetActive(true);
            _player.enabled = true;
            _Maze.SetActive(true);
        }
    
        public override void ResetChallenge()
        {
            StartChallenge();
        }

        public void OnChallengeFinished()
        {
            _player.gameObject.SetActive(false);
            _player.enabled = false;
            _Maze.SetActive(false);
        }
    }
}


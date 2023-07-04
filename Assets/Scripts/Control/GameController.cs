using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TBRPG.Attributes;
using TBRPG.Commands;
using TBRPG.StateMachine;
using UnityEditor;
using UnityEngine;

namespace TBRPG.Control
{
    public class GameController : MonoBehaviour
    {
        private IGameState _currentState;
        private List<GameObject> _playerTeam = new List<GameObject>();
        private List<GameObject> _enemies = new List<GameObject>();
        private Queue<Transform> _turnQueue = new Queue<Transform>();
        private Stack<ICommand> _commandStack = new Stack<ICommand>();
        private List<ICommand> Insertedcommands = new List<ICommand>();

        private GameObject PlayerActionHud;


        //Debug Purposes
        private List<Transform> turnPeek;
        [SerializeField]private Transform currentStateCharacter;


        private Transform remainingPlayer;
        private Transform remainingEnemy;

        private bool allowInput=false;
        private bool isturnOver=false;      

        [SerializeField] private Transform Target;
        private int command;

        private void Start()
        {
            // Initialize player team and enemies

            _playerTeam = GameObject.FindGameObjectsWithTag(Tags.PLAYERTEAM_TAG).ToList();
            _enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG).ToList();
            
            // Set initial turn queue
            SetTurnQueue();
            _currentState = new PlayerTurnState(_playerTeam[0].transform, _playerTeam[0].transform.position, _playerTeam[0].transform.rotation,4);
        }

        private void Update()
        {
            _currentState.HandleInput(this);
            _currentState.Update(this);
            //Debug.Log(_currentState.ToString());
            //Debug.Log("Turn Owner:"+ _turnQueue.Peek().name);
           
            //if (_commandStack.Peek()!=null)
            //{
            //    Debug.Log(_commandStack.Peek());
            //}         
        }

        public void ChangeState(IGameState newState)
        {
            if (_currentState.GetHashCode() != newState.GetHashCode())
            {
                // Exit the current state
                _currentState.Exit(this);

                // Set the new state
                _currentState = newState;


                _currentState.Enter(this);
            }
           
        }

        public void StartNextTurn()
        {
            //Update List to Check the enemy
            _playerTeam = _playerTeam.Where(x => x.GetComponent<Health>().IsDead() != true).ToList();
            _enemies = _enemies.Where(x => x.GetComponent<Health>().IsDead() != true).ToList();

            if(_playerTeam.Count==0)
            {
                Debug.Log("Enemy win");
                return;
            }
            else if (_enemies.Count == 0)
            {
                Debug.Log("Player win");
                return;
            }

            allowInput = false;
            if (_turnQueue.Count != 0 )
            {
                // Get the next character in the turn queue
                Transform currentCharacter = _turnQueue.Dequeue();
                turnPeek = _turnQueue.ToList();
                Debug.Log(currentCharacter.name);
                currentStateCharacter = currentCharacter;

                // Check if the current character is a player character or an enemy
                if (_playerTeam.Contains(currentCharacter.gameObject))
                {
                    // Set the current state to the player turn state
                    ChangeState(new PlayerTurnState(currentCharacter, currentCharacter.position, currentCharacter.rotation, 4));
                    allowInput = true;
                    isturnOver = false;
                    
                }
                else
                {
                    // Set the current state to the enemy turn state
                    ChangeState(new EnemyTurnState(currentCharacter, currentCharacter.position, currentCharacter.rotation, 4));
                    isturnOver = false;
                }
            }
            else
            {
                _turnQueue.Clear();
                // Add player team to turn queue
                foreach (GameObject character in _playerTeam)
                {
                    _turnQueue.Enqueue(character.transform);
                }

                // Add enemies to turn queue
                foreach (GameObject enemy in _enemies)
                {
                    _turnQueue.Enqueue(enemy.transform);
                }
            }
        }

        public void SetTurnQueue()
        {
            _turnQueue.Clear();
            // Add player team to turn queue
            foreach (GameObject character in _playerTeam)
            {
                _turnQueue.Enqueue(character.transform);
            }

            // Add enemies to turn queue
            foreach (GameObject enemy in _enemies)
            {
                _turnQueue.Enqueue(enemy.transform);
            }
            currentStateCharacter = _turnQueue.Dequeue();
            turnPeek = _turnQueue.ToList();
            allowInput = true;
        }

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            _commandStack.Push(command);
        }

        public void ExecuteCommandWithDelay(float delaySeconds, ICommand command)
        {
            StartCoroutine(ExecuteCommandWithDelayCoroutine(delaySeconds, command));
        }
        public void ExecuteUndoCommandWithDelay(float delaySeconds)
        {
            StartCoroutine(ExecuteUndoCommandWithDelayCoroutine(delaySeconds));
        }

        private IEnumerator ExecuteCommandWithDelayCoroutine(float delaySeconds, ICommand command)
        {
            yield return new WaitForSeconds(delaySeconds);
            ExecuteCommand(command);
        }
        private IEnumerator ExecuteUndoCommandWithDelayCoroutine(float delaySeconds)
        {
            yield return new WaitForSeconds(delaySeconds);
            UndoCommand();
        }
        public void UndoCommand()
        {
            if (_commandStack.Count > 0)
            {
                ICommand lastCommand = _commandStack.Pop();
                lastCommand.Undo();
            }
        }

        public Transform GetNextTurn()
        {
            Transform nextTurn = _turnQueue.Dequeue();
            _turnQueue.Enqueue(nextTurn);
            return nextTurn;
        }

        public bool IsPlayerRemaining()
        {
            remainingPlayer = _turnQueue.Where(x => x.tag == Tags.PLAYERTEAM_TAG).FirstOrDefault();
            if (remainingPlayer == null)
            {
                return false;
            }
            return true;
        }
        public bool IsEnemyRemaining()
        {
            remainingEnemy = _turnQueue.Where(x => x.tag == Tags.ENEMY_TAG).FirstOrDefault();
            if (remainingEnemy == null)
            {
                return false;
            }
            return true;
        }

        public Queue<Transform> GetTurnQueue()
        {
            return _turnQueue;
        }


        public GameObject GetTarget()
        {
            return Target.gameObject;
        }
        public int GetCommandIndex()
        {
            return command;
        }
        public Transform GetCurrentChracter()
        {
            return _turnQueue.Peek();
        }

        public void AddCommands(ICommand command)
        {
            allowInput = false;
            Insertedcommands.Add(command);
        }
        public void ExecuteCommandListWithDelay()
        {
            StartCoroutine(ExecuteCommandWithDelayCoroutine(Insertedcommands));
        }
        private IEnumerator ExecuteCommandWithDelayCoroutine(List<ICommand> CommandList)
        {
            foreach (var command in CommandList)
            {
                //Debug.Log(command.ToString());
                switch (command)
                {
                    case MoveCommand moveCommand:
                        moveCommand.Execute();
                        yield return new WaitForSeconds(6);
                        break;
                    case AttackCommand attackCommand:
                        attackCommand.Execute();
                        yield return new WaitForSeconds(6);
                        break;
                    case RotateCommand rotateCommand:
                        rotateCommand.Execute();
                        yield return new WaitForSeconds(1);
                        break;

                    default:
                        Debug.LogError("Unknown command type: " + command.GetType());
                        break;
                }
            }
            Insertedcommands.Clear();
            isturnOver = true;

        }

        public bool IsTurnOver()
        {
            return isturnOver;
        }

        public bool IsInputAllowed()
        {
            return allowInput;
        }

        public List<GameObject> GetPlayerTeam()
        {
            return _playerTeam;
        }
        public List<GameObject> GetEnemyTeam()
        {
            return _enemies;
        }


        public void SetTarget(Transform Target,int Command)
        {
            this.Target = Target;
            command= Command;
            _currentState.HandleInput(this);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TBRPG.Attributes;
using TBRPG.Commands;
using TBRPG.Control;
using TBRPG.Stats;
using UnityEngine;

namespace TBRPG.StateMachine
{
    public class EnemyTurnState : IGameState
    {
        Transform enemy;
        Vector3 InitialLocation;
        Quaternion intialrotation;
        float speed;
        List<GameObject> playerteam;
        List<GameObject> enemyteam;
        bool hasCompletedAction = false;

        public EnemyTurnState()
        {
        }

        public EnemyTurnState(Transform Enemy,Vector3 initialLocation, Quaternion intialrotation, float speed)
        {
            enemy = Enemy;
            InitialLocation = initialLocation;
            this.intialrotation = intialrotation;
            this.speed = speed;
        }

        public void Enter(GameController controller)
        {
            controller.ChangeState(new EnemyTurnState());
        }

        public void Exit(GameController controller)
        {
            ResetVariables();
        }

        public void HandleInput(GameController controller)
        {

            if (!hasCompletedAction)
            {
                CharacterClass characterClass = enemy.GetComponent<BaseStats>().GetCharacterClass();
                
                switch (characterClass)
                {
                    case CharacterClass.Grunt:
                        AttackRandomly(controller);
                        hasCompletedAction = true;
                        Debug.Log(enemy .name+ " Attack");
                        break;
                    case CharacterClass.Mage:
                        AttackRandomly(controller);
                        hasCompletedAction=true;
                        Debug.Log(enemy.name+" Attack");
                        break;
                    case CharacterClass.Archer:
                        AttackRandomly(controller);
                        hasCompletedAction=true;
                        Debug.Log(enemy.name+" Attack");
                        break;
                    case CharacterClass.Heavy:
                        AttackRandomly(controller);
                        hasCompletedAction=true;
                        Debug.Log(enemy.name+" Attack");
                        break;
                    case CharacterClass.Player:
                        Debug.LogError("Invalid this is used in the Enemy Turn"); break;
                    default:
                        break;
                } 
            }
        }

        public void Update(GameController controller)
        {
            if (controller.GetTurnQueue().Count == 0)
            {
                controller.SetTurnQueue();
            }

            if (controller.IsTurnOver())
            {
                Debug.Log("Next turn");
                controller.StartNextTurn();
            }


           
        }

        private void AttackRandomly(GameController controller)
        {
            playerteam = controller.GetPlayerTeam();
            controller.AddCommands(new AttackCommand(enemy, playerteam[Random.Range(0, playerteam.Count)]));
            controller.AddCommands(new MoveCommand(enemy, InitialLocation, speed));
            controller.AddCommands(new RotateCommand(enemy, intialrotation));
            controller.ExecuteCommandListWithDelay();
        }

        private void HealPriority(GameController controller)
        {
            enemyteam = controller.GetEnemyTeam();
            GameObject EnemywithLowestHealth = enemyteam.Where(x => x.GetComponent<Health>().GetFraction() < 50).OrderBy(x => x.GetComponent<Health>().GetHealthPoints()).FirstOrDefault();
            //USe the HealCommand
        }
        private void ResetVariables()
        {
            enemy = null;
            InitialLocation = Vector3.zero;
            intialrotation = Quaternion.identity;
            speed = 0;
            hasCompletedAction = false;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.Battle
{
    public class Unit : MonoBehaviour, IUnit
    {
        const float jumpWhen = 0.8f;//ONE / 2 or 3 //I gotta think about new name

        private short _hp;
        //private Timer attackTimer;
        private IUnit target;
        private Func<Vector3> getDestination;
        private Animator anim; 

        [SerializeField]private Vector3 savePoint; 
        [SerializeField]private float jumpForce = 1;
        [SerializeField]private Image hpBar;

        public UnitStats Stats { get; set; }
        public IAI AI { get; set; }
        public State state { get; set; }
        public IUnit Target { get; set; }
        public short hp 
        { 
            get
            {
                return _hp;
            } 
            set
            {
                if (_hp < value)
                    GetComponent<Animator>().Play("GetHit");

                _hp = value;
                
                hpBar.fillAmount = _hp / Stats.hitPoinsts;    

                if (_hp <= 0)
                {
                    Die();
                }
            }
        }
        public IWeapon Weapon {get; set; }

        public Vector3 SafePoing 
        {
            get
            {
                return savePoint;
            } 
        }

        public Vector3 Destination 
        {
            get
            {
                return getDestination();
            }
            set
            {
                getDestination = () => value;
            }
        }

        //private float AttackTimer
        //{
        //    get
        //    {
        //        return attackTimer;
        //    }
        //    set
        //    {
        //        attackTimer = value;
        //
        //        if (attackTimer < 0)
        //        {
        //            Attack();
        //            attackTimer = 1/Weapon.Speed;
        //        }
        //    }
        //}

        public void Die()
        {
            Destroy(gameObject);
        }

        private void Start()
        {
            AI = ArtificalIntelect.GetAI(this);
            Target = AI.FindPriorityTarget();
            //attackTimer = new Timer(Attack, 1 / Weapon.Speed);
            print(Stats.hitPoinsts);
            _hp = Stats.hitPoinsts;
            anim = GetComponent<Animator>();
            name = Stats.name;

            if (Target != null)
                BindDestinationToTransform(Target.transform);
            else
                Destination = transform.position;
        }

        private void Update()
        {
            state = AI.WhatToDo();

            switch (state)
            {
                case State.Going:
                    int direction = Math.Sign(Destination.x - transform.position.x);

                    if (direction != 0)
                        Move(direction);
                break;

                case State.Attack:
                    //attackTimer -=Time.deltaTime;
                    anim.Play("Hit");
                break;

                case State.Hide:
                    Destination = SafePoing;
                    break;
            }
        }

        //private void FixedUpdate()
        //{
        //    if(state == State.Attack)
        //    {
        //        attackTimer -= Time.fixedDeltaTime;
        //    }
        //}

        private void Move(int direction)// -1 - left; 1 - right
        {
            transform.position += Vector3.right * direction * Stats.speed * Time.deltaTime;
            transform.localScale = new Vector3(direction, 1);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.right * direction, jumpWhen);
            if (hit ? hit.collider.tag == "Groung" : false)
            {
                Jump();
            }
        }

        private void Jump()
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce); //I oughta replace GetComponent with field
        }

        private void Attack()
        {
            if (state == State.Attack)
            Target.hp -= Weapon.Damage;
        } 

        public void BindDestinationToTransform(Transform bindTo)
        {
            getDestination = () =>
            {
                if (bindTo != null)
                    return bindTo.position;

                getDestination = () => transform.position;
                return getDestination();
            };
        } 
    }
}

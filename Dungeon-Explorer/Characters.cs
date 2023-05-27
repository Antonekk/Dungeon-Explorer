using System;
using System.Collections.Generic;
using items;

namespace characters
{
    class Character
    {
        protected int max_hp;
        protected int current_hp;
        protected int luck;
        protected int defence;
        protected int level;

        public Character(){
            level = 1;
            //todo
        }

        public void Atack(Character atacked){
            //todo
        }

        public void Recive_Damage(int dmg){
            //todo
        }

        bool is_dead(){
            //todo
            return false;
        }
    }

    class Player : Character
    {
        int gold_coins;
        int current_exp;
        int exp_to_level;
        List<Item> Items;


        public Player(){
            //todo
        }
    }


    class Enemy : Character
    {
        List<Item> items_to_drop;
        int gold_to_drop;
        public Enemy(){
            //todo
        }
    }
}

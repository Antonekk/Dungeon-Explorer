using System;
using System.Collections.Generic;
using items;

namespace characters
{
    abstract class Character
    {
        protected int max_hp;
        protected int current_hp;
        protected int luck;
        protected int defence;
        protected int level;



        abstract public void Atack(Character atacked);

        abstract public void Recive_Damage(int dmg);

        bool is_dead(){
            return current_hp <= 0;
        }

        public int get_max_hp(){
            return max_hp;
        }
        public int get_current_hp(){
            return current_hp;
        }
        public int get_luck(){
            return luck;
        }
        public int get_defence(){
            return defence;
        }
        public int get_level(){
            return level;
        }

        protected void character_scale(){
            scale_hp();
            scale_defence();
            scale_luck();
        }

        protected void scale_hp(){
            max_hp = level*10;
        }
        protected void scale_luck(){
            luck = level*3;
        }
        protected void scale_defence(){
            defence = level*5;
        }
    }

    class Player : Character
    {
        int gold_coins;
        int current_exp;
        int exp_to_level;
        List<Item> items;


        public Player(){
            level = 1;
            gold_coins = 5;
            current_exp = 0;
            exp_to_level = 10;
            character_scale();
            current_hp = max_hp;
            items = new List<Item>();
        }

        public bool pay(int gc){
            if (gc<=gold_coins){
                gold_coins -= gc;
                return true;
            }
            return false;
        }

        public void heal(int h){
            int hp_increase = h+current_hp;
            if(hp_increase >= max_hp){
                current_hp = max_hp;
            }
            else{
                current_hp = hp_increase;
            }
        }

        public int get_gold_coins(){ //{get;set}
            return gold_coins;
        }
        public int get_current_exp(){ //{get;set}
            return current_exp;
        }
        public int get_exp_to_level(){ //{get;set}
            return exp_to_level;
        }

        public override void Atack(Character atacked)
        {
            throw new NotImplementedException();
        }

        public override void Recive_Damage(int dmg)
        {
            throw new NotImplementedException();
        }
    }


    class Enemy : Character
    {
        List<Item> items_to_drop;
        int gold_to_drop;
        public Enemy(){
            //todo
        }

        public override void Atack(Character atacked)
        {
            throw new NotImplementedException();
        }

        public override void Recive_Damage(int dmg)
        {
            throw new NotImplementedException();
        }
    }
}

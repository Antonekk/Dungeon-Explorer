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

        protected int damage;



        abstract public void Atack(Character atacked);

        abstract public void Recive_Damage(int dmg);

        public bool is_dead(){
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
        public int get_damage(){
            return damage;
        }

        protected void character_scale(){
            scale_hp();
            scale_defence();
            scale_luck();
            scale_damage();
        }

        protected void scale_hp(){
            max_hp = level*50;
        }
        protected void scale_luck(){
            luck = level*3;
        }
        protected void scale_defence(){
            defence = level*5;
        }
        protected void scale_damage(){
            damage = level*10;
        }
    }

    class Player : Character
    {
        int gold_coins;
        int current_exp;
        int exp_to_level;
        List<Item> items;
        int bag_size = 3;


        public Player(){
            level = 1;
            gold_coins = 35;
            current_exp = 0;
            exp_to_level = 10;
            character_scale();
            current_hp = max_hp;
            items = new List<Item>{null,null,null};
        }

        public bool pay(int gc){
            if (gc<=gold_coins){
                gold_coins -= gc;
                return true;
            }
            return false;
        }

        public void equip_item(int pos, Item i){
            if(items[pos] != null){
                remove_item(i);
            }
            add_item(i);
            items[pos] = i;
        }

        private void add_item(Item i){
            defence += i.get_defence();
            damage += i.get_damage();
            luck += i.get_luck();
        }

        private void remove_item(Item i){
            defence -= i.get_defence();
            damage -= i.get_damage();
            luck -= i.get_luck();
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

        public string get_item_string(int n){
            if(items[n] == null){
                return "Empty Slot";
            }
            return items[n].ToString();
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


    abstract class Enemy : Character
    {
        List<Item> items_to_drop;
        int gold_to_drop;


        public override void Atack(Character atacked)
        {
            throw new NotImplementedException();
        }

        public override void Recive_Damage(int dmg)
        {
            throw new NotImplementedException();
        }
    }

    class Orc : Enemy
    {


        public override void Recive_Damage(int dmg)
        {
            throw new NotImplementedException();
        }
    }

    class Skeleton : Enemy
    {

        public override void Atack(Character atacked)
        {
            throw new NotImplementedException();
        }
    }

    class Goblin : Enemy
    {

    }
}



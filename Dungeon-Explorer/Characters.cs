using System;
using System.Collections.Generic;
using items;

namespace characters
{
    abstract class Character
    {
        public int max_hp {get; private set;} //Change all the others
        protected int current_hp;
        protected int luck;
        protected int defence;
        protected int level;

        protected int damage;

        protected Random rnd = new Random();



        abstract public void Atack(Character atacked, double bonus);

        abstract public void Recive_Damage(int dmg);

        public bool is_dead(){
            return current_hp <= 0;
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

        public void add_gold(int g){
            gold_coins += g;
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

        public bool is_in_danger(){
            if(current_hp < max_hp/2){
                return true;
            }
            else{
                return false;
            }
        }

        private void lose_gold(){
            int gtd =  Convert.ToInt32((rnd.NextDouble() * (0.60 - 0.40) + 0.15) * gold_coins);
            gold_coins -= gtd;

        }

        public void run_away(){
            lose_gold();
            current_hp -= Convert.ToInt32( 0.10 * max_hp);
            if( current_hp < 0){
                current_hp = 0;
            }
        }

        public int get_gold_coins(){
            return gold_coins;
        }
        public int get_current_exp(){
            return current_exp;
        }
        public int get_exp_to_level(){
            return exp_to_level;
        }

        public string get_item_string(int n){
            if(items[n] == null){
                return "Empty Slot";
            }
            return items[n].ToString();
        }



        public override void Atack(Character atacked, double bonus)
        {
            atacked.Recive_Damage(Convert.ToInt32(damage*bonus));

        }

        public override void Recive_Damage(int dmg)
        {

            if(rnd.Next(1, 101) < luck){
                Console.WriteLine("Doged");
                return;
            }

            if(dmg >= defence){
                dmg -= defence;
            }
            else{
                dmg = 0;
            }
            if(current_hp-dmg <= 0){
                current_hp = 0;
            }
            else{
                current_hp -= dmg;
            }
        }
    }


    abstract class Enemy : Character
    {
        //Item item_to_drop = null;
        //protected Random rnd = new Random();
        //Item_Generator ig;
        protected string enemy_class = "";

        int gold_to_drop;

        private void set_gold(){
            gold_to_drop =  level * 3;
        }

        public int drop_gold(){
            if (is_dead()){
                return gold_to_drop;
            }
            throw new Exception("Can't drop gold");
        }

        public string get_class_name(){
            return enemy_class;
        }
        /*
        private void items_to_drop(){
            if (rnd.Next(0,8) == 0)
            {
                item_to_drop = ig.generate_item(rnd.Next(0,3));
            }
        }
        */


        public override void Atack(Character atacked, double bonus)
        {
            atacked.Recive_Damage(damage);

        }

        public override void Recive_Damage(int dmg)
        {
            if(current_hp-dmg <= 0){
                current_hp = 0;
            }
            else{
                current_hp -= dmg;
            }
        }
    }

    class Orc : Enemy
    {


        public Orc (int lvl){
            enemy_class = "Orc";
            level = 1;
            character_scale();
            current_hp = max_hp;
        }


        public override void Recive_Damage(int dmg)
        {
            if(dmg >= defence){
                dmg -= defence;
            }
            else{
                dmg = 0;
            }
            if(current_hp-dmg <= 0){
                current_hp = 0;
            }
            else{
                current_hp -= dmg;
            }
        }
    }

    class Skeleton : Enemy
    {

        public Skeleton (int lvl){
            enemy_class = "Skeleton";
            level = 1;
            character_scale();
            current_hp = max_hp;
        }

        public override void Atack(Character atacked, double bonus)
        {
            atacked.Recive_Damage(damage);
            if(rnd.Next(1) == 0){
                atacked.Recive_Damage(damage);
                Console.WriteLine("Skeleton attacked twice");
            };

        }

    }

    class Goblin : Enemy
    {

        public Goblin (int lvl){
            enemy_class = "Goblin";
            level = 1;
            character_scale();
            current_hp = max_hp;
        }

    }
}



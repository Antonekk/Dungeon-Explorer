using System;
using System.Collections.Generic;
using items;
using cto;

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

        protected Random rnd = new Random();
        protected CTO term = new CTO();



        abstract public void Atack(Character atacked, double bonus);

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

        virtual protected void character_scale(){
            scale_hp();
            scale_defence();
            scale_luck();
            scale_damage();
        }

        virtual protected void scale_hp(){
            max_hp = 95 + (int)(Math.Ceiling(Math.Log(level*100))) + (level-1)*10 ;
        }
        virtual protected void scale_luck(){
            luck = 8 + (int)(Math.Ceiling(Math.Log(level*50))) + (level-1)*2;
        }
        virtual protected void scale_defence(){
            defence = 3 + (int)(Math.Ceiling(Math.Log(level*50))) + (level-1)*2;
        }
        virtual protected void scale_damage(){
            damage = 8 + (int)(Math.Ceiling(Math.Log(level*60))) + (level-1)*4;
        }


    }

    class Player : Character
    {
        int gold_coins;
        int current_exp;
        int exp_to_level;
        IDictionary<string, Item> items;
        int bag_size = 3;


        public Player(){
            level = 1;
            gold_coins = 35;
            current_exp = 0;
            scale_exp();
            character_scale();
            current_hp = max_hp;
            items = new Dictionary<string, Item>();
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

        public void equip_item(Item i){
            if(items.ContainsKey(i.GetType().Name)){
                remove_item(i);
            }
            add_item(i);
            items[i.GetType().Name] = i;
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

        public void heal(){
            current_hp = max_hp;
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

        public List<items.Item> get_items(){
            return items.Values.ToList();
        }

        public void add_exp(int e){
            while (e != 0){
                if(current_exp + e > exp_to_level){
                    level += 1;
                    character_scale();
                    e -=  (exp_to_level - current_exp );
                    scale_exp();
                }
                else{
                    current_exp += e;
                    e = 0;
                }
            }
        }

        private void scale_exp(){
            exp_to_level = level*10;
        }



        public override void Atack(Character atacked, double bonus)
        {
            atacked.Recive_Damage(Convert.ToInt32(damage*bonus));

        }

        public override void Recive_Damage(int dmg)
        {

            if(rnd.Next(101) < luck){
                term.ClearCurrentConsoleLine();
                Console.WriteLine("Doged");
                Thread.Sleep(1000);
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
        protected string enemy_class = "";

        int gold_to_drop;

        protected void set_gold(){
            gold_to_drop =  level*rnd.Next(9,14);
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

        protected override void character_scale(){
            scale_hp();
            scale_defence();
            scale_luck();
            scale_damage();
        }

        protected override void scale_hp(){
            max_hp = 20 + (int)(Math.Ceiling(Math.Log(level*100))) + (level-1)*8 ;
        }
        protected override void scale_luck(){
            luck = 8 + (int)(Math.Ceiling(Math.Log(level*50))) + (level-1)*2;
            if(luck > 80){
                luck = 80;
            }
        }
        protected override void scale_defence(){
            defence = 2 + (int)(Math.Ceiling(Math.Log(level*50))) + (level-1)*2;
        }
        protected override void scale_damage(){
            damage = 3 + (int)(Math.Ceiling(Math.Log(level*60))) + (level-1)*3;
        }
    }

    class Orc : Enemy
    {


        public Orc (int lvl){
            enemy_class = "Orc";
            level = lvl;
            character_scale();
            set_gold();
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
            level = lvl;
            character_scale();
            set_gold();
            current_hp = max_hp;
        }

        public override void Atack(Character atacked, double bonus)
        {
            atacked.Recive_Damage(damage);
            if(rnd.Next(3) == 0){
                atacked.Recive_Damage(damage);
                Console.Write("\n");
                Console.WriteLine("Skeleton attacked twice");
                Thread.Sleep(1000);
            };

        }

    }

    class Goblin : Enemy
    {

        public Goblin (int lvl){
            enemy_class = "Goblin";
            level = lvl;
            character_scale();
            set_gold();
            current_hp = max_hp;
        }


        public override void Recive_Damage(int dmg)
        {
            int l;
            if(luck>80){
                l = 80;
            }
            else{
                l = luck;
            }
            if(rnd.Next(101) < l){
                term.ClearCurrentConsoleLine();
                Console.WriteLine("Goblin doged");
                Thread.Sleep(1000);
                return;
            }

            if(current_hp-dmg <= 0){
                current_hp = 0;
            }
            else{
                current_hp -= dmg;
            }
        }



    }
}



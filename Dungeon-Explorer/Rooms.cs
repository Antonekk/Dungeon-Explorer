using System;
using characters;
using items;
using cto;


namespace rooms
{
    abstract class Room
    {
        protected Random rnd ;
        protected CTO term = new CTO();
        protected int room_level;

        public abstract void start(Player p);
    }


    class Fight_Room : Room
    {
        Enemy foe;

        public Fight_Room(){
            //todo
        }
        public Fight_Room(int lvl){
            //todo
        }

        public override void start(Player p)
        {
            throw new NotImplementedException();
        }

        public void Start_Fight(){
            //todo
        }

        public bool Has_Ended(){
            //todo
            return false;
        }

        public bool check_player_win(){
            //todo
            return false;
        }

    }

    class Shop : Room
    {
        List<Tuple<int, Item>> available_items;
        public Shop(){

        }
        public Shop(int level){

        }

        public override void start(Player p)
        {
            throw new NotImplementedException();
        }

    }

    class Healing_fountain : Room
    {
        int price;
        int chance;
        int heal;
        public Healing_fountain(){
            rnd = new Random();
            room_level = 1;
            price = Scale_price(room_level);
            heal = Scale_hp(room_level);
            chance = rnd.Next(40,90);

        }
        public Healing_fountain(int level){
            rnd = new Random();
            room_level = level;
            price = Scale_price(room_level);
            heal = Scale_hp(room_level);
            chance = rnd.Next(40,90);
        }

        public override void start(Player p)
        {
            Console.Clear();
            term.WritePlayerData(p);
            term.Write_Center("You found Healing Fountain\n");
            Console.WriteLine($"[1] Pay {price} gold, heal {heal}hp with {chance}%\n");
            Console.WriteLine("[Other] Leave:\n");
            ConsoleKeyInfo key = Console.ReadKey();
            if(key.Key == ConsoleKey.D1){
                buy_state(p);
            }

        }

        private void buy_state(Player p){
            term.ClearCurrentConsoleLine();
            int r = rnd.Next(100);
            if(p.pay(price))
            {
                if((r >= chance && r<=90)){
                    Console.WriteLine("Success");
                    p.heal(this.heal);
                    return;
                }
                Console.WriteLine("Healing was not successful");
                return;

            }
            Console.WriteLine("Not enough gold coins");

        }

        int Scale_price(int lvl){
            int xlvl = 2*lvl;
            return (xlvl* (int)(Math.Ceiling(Math.Log(xlvl))));
        }
        int Scale_hp(int lvl){
            int xlvl = 5*lvl;
            return (xlvl* (int)(Math.Ceiling(Math.Log(xlvl)))) ;
        }
    }
}

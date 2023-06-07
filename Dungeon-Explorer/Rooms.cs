using System;
using characters;
using items;
using cto;


namespace rooms
{
    abstract class Room
    {
        protected Random rnd = new Random();
        protected CTO term = new CTO();
        protected int room_level;

        public abstract void start(Player p);
    }


    class Fight_Room : Room
    {
        Enemy enemy;

        public Fight_Room(){
            //todo
        }
        public Fight_Room(int lvl){
            //todo
        }

        public override void start(Player p)
        {
            Console.Clear();
            term.WritePlayerData(p);
            term.Write_Center("You've encountered an enemy\n");
            ConsoleKeyInfo key = Console.ReadKey();


        }

        public void Start_Fight(){
            //todo
            // 1. Player attacks by choosing from which side to attack (L M R)
            // 2. Player can either doge to the left or right to mitigate fall damage if guess is good or stay in the middle to boost next atack
        }

        public bool Has_Ended(Player p){
            return (p.is_dead() || enemy.is_dead());
        }

        public bool check_player_win(){
            return enemy.is_dead();
        }

    }

    class Shop : Room
    {
        List<Tuple<int, Item>> available_items;
        Item_Generator ig;
        public Shop(){
            ig = new Item_Generator();
            available_items = new List<Tuple<int, Item>>();
            add_items();

        }
        public Shop(int level){
            ig = new Item_Generator(level);
            available_items = new List<Tuple<int, Item>>();
            add_items();

        }

        void add_items(){
            Tuple <int, Item> t;
            for (int i=0;i<3;i++){
                t = Tuple.Create(10, ig.generate_item(i));
                available_items.Add(t);
            }

        }

        void buy(int item, Player p){
            int price = available_items[item].Item1;
            if (p.pay(price)){
                Console.WriteLine("You bought" + available_items[item].Item2.ToString() + "\n");
                p.equip_item(item,available_items[item].Item2);
                return;
            }
            Console.WriteLine("Not enough gold coins\n");
        }

        public override void start(Player p)
        {
            Console.Clear();
            term.WritePlayerData(p);
            term.Write_Center("You found Shop\n");
            for(int i = 0; i<3; i++){
                Console.WriteLine($"[{i+1}] Pay {available_items[i].Item1} for {available_items[i].Item2} \n");
            }
            Console.WriteLine("[Other] Leave:\n");
            ConsoleKeyInfo key = Console.ReadKey();
            term.ClearCurrentConsoleLine();
                switch(key.Key){
                    case ConsoleKey.D1:
                        buy(0, p);
                        break;

                    case ConsoleKey.D2:
                        buy(1,p);
                        break;

                    case ConsoleKey.D3:
                        buy(2,p);
                        break;
                    default:
                        return;
            }
            Console.WriteLine("[Any] Go to the next room");
            ConsoleKeyInfo key2 = Console.ReadKey();
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
            Console.WriteLine($"[1] Pay {price} gold, heal {heal}hp with {chance}% success rate\n");
            Console.WriteLine("[Other] Leave:\n");
            ConsoleKeyInfo key = Console.ReadKey();
            if(key.Key == ConsoleKey.D1){
                buy_state(p);
                Console.WriteLine("[Any] Go to the next room");
                ConsoleKeyInfo key2 = Console.ReadKey();
            }

        }

        private void buy_state(Player p){
            term.ClearCurrentConsoleLine();
            int r = rnd.Next(100);
            if(p.pay(price))
            {
                if((r >= chance && r<=90)){
                    Console.WriteLine("Success\n");
                    p.heal(this.heal);
                    return;
                }
                Console.WriteLine("Healing was not successful\n");
                return;

            }
            Console.WriteLine("Not enough gold coins\n");

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

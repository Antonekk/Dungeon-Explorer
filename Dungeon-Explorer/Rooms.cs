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
            room_level = 1;
            enemy = generate_enemy();
        }
        public Fight_Room(int lvl){
            room_level = lvl;
            enemy = generate_enemy();
        }

        Enemy generate_enemy(){
            int e = rnd.Next(1,4);
            switch(e){
                case 1:
                    return new Orc(room_level);
                case 2:
                    return new Skeleton(room_level);
                case 3:
                    return new Goblin(room_level);
            }
            throw new Exception("Wrong character");
        }



        public override void start(Player p){
            // Player can either focus to boost doge chance or prepare to boost next atack (effects can stack)
            int doge_chance = 0;
            double damage_mult = 1.0;
            while(!enemy.is_dead()){
                Console.Clear();
                term.WritePlayerData(p);
                term.Write_Center($"You've encountered  [{enemy.get_class_name()} {enemy.get_current_hp()}/{enemy.get_max_hp()}]\n");
                Console.WriteLine("[1] Atack:\n");
                Console.WriteLine($"[2] Prepare (Damage multiplier [+ 1x - 1.25x]) [Current multiplier: {Math.Round(damage_mult, 2)}x]:\n");
                Console.WriteLine($"[3] Focus (Bonus doge chance [10% - 15%]) [Current bonus: {doge_chance}]\n");
                if(p.is_in_danger()){
                    Console.WriteLine("[4] Run Away (-10% of max health ) (Chance to lose gold )\n");
                }
                ConsoleKeyInfo key = Console.ReadKey();
                switch(key.Key){
                    case ConsoleKey.D1:
                        p.Atack(enemy, damage_mult);
                        break;

                    case ConsoleKey.D2:
                        damage_mult += (rnd.NextDouble() * (1.25 - 1.0) + 1.0);
                        break;

                    case ConsoleKey.D3:
                        int bc = rnd.Next(10,16);
                        if (doge_chance + bc <= 80){
                            doge_chance += bc;
                            break;
                        }
                        doge_chance = 100;
                        break;

                    case ConsoleKey.D4:
                        p.run_away();
                        is_dead_info(p);
                        Console.WriteLine("You ran away:\n");
                        return;
                    default:
                        break;
                }
                if(enemy.is_dead()){
                    p.add_gold(enemy.drop_gold());
                    p.add_exp(rnd.Next(Convert.ToInt32(p.get_exp_to_level()*0.35), Convert.ToInt32(p.get_exp_to_level()*0.65)));
                    term.ClearCurrentConsoleLine();
                    Console.Write($"[Any] Enemy died, and droped {enemy.drop_gold()} gold\n");
                    break;
                }


                if(rnd.Next(1, 101) < doge_chance){
                    term.ClearCurrentConsoleLine();
                    Console.WriteLine("Doged");
                    Thread.Sleep(1000);
                }
                else{
                    enemy.Atack(p,1);
                }

                is_dead_info(p);


            }
            Console.ReadKey();


        }

        private void is_dead_info(Player p){
            if(p.is_dead()){
                Console.Clear();
                term.Write_Center($"You died in room {room_level}");
                System.Environment.Exit(0);
            }
        }


    }

    class Shop : Room
    {
        List<Tuple<int, Item>> available_items;
        Item_Generator ig;
        public Shop(){
            room_level = 1;
            ig = new Item_Generator();
            available_items = new List<Tuple<int, Item>>();
            add_items();


        }
        public Shop(int level){
            room_level = level;
            ig = new Item_Generator(level);
            available_items = new List<Tuple<int, Item>>();
            add_items();


        }

        void add_items(){
            Tuple <int, Item> t;
            for (int i=0;i<3;i++){
                t = Tuple.Create(generate_price(), ig.generate_item(i));
                available_items.Add(t);
            }
        }

        int generate_price(){
            return room_level * rnd.Next(8,12);
        }

        void buy(int item, Player p){
            int price = available_items[item].Item1;
            if (p.pay(price)){
                Console.WriteLine("You bought" + available_items[item].Item2.ToString() + "\n");
                p.equip_item(available_items[item].Item2);
                return;
            }
            Console.WriteLine("Not enough gold coins\n");
        }

        public override void start(Player p)
        {
            Console.Clear();
            term.WritePlayerData(p);
            term.Write_Center("Shop\n");
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
        public Healing_fountain(){
            room_level = 1;
            price = Scale_price(room_level);
            chance = rnd.Next(90,100); //(90-99)

        }
        public Healing_fountain(int level){
            room_level = level;
            price = Scale_price(room_level);
            chance = rnd.Next(90,100);//(90-99)
        }

        public override void start(Player p)
        {
            Console.Clear();
            term.WritePlayerData(p);
            term.Write_Center("Healing Fountain\n");
            Console.WriteLine($"[1] Pay {price} gold, heal to max hp with {chance}% success rate\n");
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
            int r = rnd.Next(101);
            if(p.pay(price))
            {
                if((r <= chance)){
                    Console.WriteLine("Success\n");
                    p.heal();
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
    }
}

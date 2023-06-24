using System;
using rooms;
using cto;
using characters;

namespace dungeon
{
    class Dungeon
    {
        CTO term = new CTO();
        Random rnd = new Random();
        bool is_fight;
        Room current_room;

        Player player;

        int game_level;

        public Dungeon(){
            game_level = 1;
            player = new Player();
            current_room = new Fight_Room();
            is_fight = true;
        }

        public void menu(){
            while(true){
                Console.Clear();
                term.Write_Center("Dungeon Explorer");
                Console.WriteLine("[1] Start game:\n");
                Console.WriteLine("[2] Read info:\n");
                Console.WriteLine("[3] Exit:\n");
                ConsoleKeyInfo key = Console.ReadKey();
                term.ClearCurrentConsoleLine();
                switch(key.Key){
                    case ConsoleKey.D1:
                        start_game();
                        break;

                    case ConsoleKey.D2:
                        game_info();
                        break;

                    case ConsoleKey.D3:
                        Console.WriteLine("Exit");
                        System.Environment.Exit(1);
                        break;
                }
            }


        }

        void start_game(){
            while(true){
                current_room.start(player);
                current_room = generate_new_room();
                game_level++;
            }

        }

        void game_info(){
            Console.Clear();
            term.Write_Center("Game Info");
            Console.WriteLine("[Click any button to return]");
            Console.ReadKey();
        }

        Room generate_new_room(){
            if(is_fight){
                is_fight = false;
                int r = rnd.Next(100);
                if(r <=25){
                    return new Shop(game_level);
                }
                else if(r <= 45){
                    return new Healing_fountain(game_level);
                }
                else {
                    return new Fight_Room(game_level);
                }
            }
            else{
                is_fight = true;
                return new Fight_Room(game_level);
            }

        }
    }
}

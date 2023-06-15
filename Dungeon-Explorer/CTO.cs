using characters;
namespace cto
{
    class CTO
    {

        public void WritePlayerData(Player p){
            Write_Center("Player");

            Console.Write("Level: " + p.get_level() + " | ");
            Console.Write("Hp: " + p.get_current_hp() + "/" + p.max_hp +" | ");
            Console.Write("Defence: " + p.get_defence() + " | ");
            Console.Write("Luck: " + p.get_luck() + " | ");
            Console.Write("Damage: " + p.get_damage() + " | ");
            Console.Write("Gold coins: " + p.get_gold_coins() + " | ");
            Console.Write("Exp: " + p.get_current_exp() + "/" + p.get_exp_to_level() +"\n");
            Console.Write("Equipment " );
            for (int i = 0; i<3; i++){
                Console.Write(" | " + p.get_item_string(i));
            }
            Console.Write("\n");

            for (int i=0;i<Console.WindowWidth;i++){
                Console.Write("-");
            }
            Console.Write("\n");

        }


        public void ClearCurrentConsoleLine()
        {
            //https://stackoverflow.com/a/8946847
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        public void Write_Center(String txt){
            Console.SetCursorPosition((Console.WindowWidth - txt.Length) / 2, Console.CursorTop);
            Console.WriteLine(txt);
        }
    }
}

using System;

namespace items
{
    class Item_Generator{
        int level;
        int names_arr_len;
        Random rnd = new Random();
        static string[] names = { "Alchemy Chalice", "Invincibility Jar", "Key of Wealth", "Hand of Transmutation", "Gem of Bane",
                        "Shade Circlet", "Philosopher's Ichor", "Thaumaturgy Fleece", "Crown of Athanasia"};

        public Item_Generator(){
            level = 1;
            names_arr_len = names.Length;

        }
        public Item_Generator(int l){
            level = l;
            names_arr_len = names.Length;
        }




        public Item generate_item(){
            int item_index = rnd.Next(names_arr_len);
            return new Item(level, names[item_index]);
        }
    }
    class Item
    {
        string item_name;
        int item_level;
        int bonus_hp;
        int bonus_luck;
        int bonus_damage;

        public Item(){
            item_name = "Key";
            item_level = 1;
            bonus_hp = 0;
            bonus_luck = 1;
            bonus_damage = 0;
        }
        public Item(int level, string name){
            item_level = level;
            item_name = name;
        }

        int scale_level(){
            return item_level*3;
        }

        public override string ToString()
        {
            return $"[{item_level}] {item_name} ";
        }
    }
}

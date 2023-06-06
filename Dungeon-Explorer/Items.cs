using System;

namespace items
{
    class Item_Generator{
        int level;
        static int armor_names_arr_len;
        static int weapon_names_arr_len;
        static int talisman_names_arr_len;
        Random rnd = new Random();

        //Names generated from www.fantasynamegenerators.com
        static string[] armor_names = { "Vest of Demonic Worlds", "Cuirass of Haunted Illusions", "Bronzed Armor of Infernal Dreams", "Skeletal Vest of Ending Warlords", "Sinister Iron Armor",
                        "Blood Infused Obsidian Armor", "Chestplate of Miracles", "Roaring Chestguard of Dragonsouls", "Chestpiece of the Victor"};
        static string[] weapon_names = { "Vindication Sword", "Sharpened Bronze Quickblade", "Doom's Iron Defender", "Kinslayer", "Warmonger",
                        "Soulrapier", "Knightly Warblade", "Furious Silver Shortsword", "Furious Copper Mageblade"};
        static string[] talisman_names = { "Alchemy Chalice", "Cube of Guardians", "Key of Wealth", "Hand of Transmutation", "Gem of Bane",
                        "Shade Circlet", "Philosopher's Ichor", "Thaumaturgy Fleece", "Crown of Athanasia"};


        public Item_Generator(){
            level = 1;
            init_len();

        }
        public Item_Generator(int l){
            level = l;
            init_len();
        }
        void init_len(){
            armor_names_arr_len = armor_names.Length;
            weapon_names_arr_len = weapon_names.Length;
            talisman_names_arr_len = talisman_names.Length;
        }

        private Armor generate_armor(){
            int armor_name_index = rnd.Next(armor_names_arr_len);
            return new Armor(level,armor_names[armor_name_index]);
        }

        private Weapon generate_weapon(){
            int weapon_name_index = rnd.Next(weapon_names_arr_len);
            return new Weapon(level,weapon_names[weapon_name_index]);
        }

        private Talisman generate_talisman(){
            int talisman_name_index = rnd.Next(talisman_names_arr_len);
            return new Talisman(level,talisman_names[talisman_name_index]);
        }

        public Item generate_item(int category){
            switch(category){
                case 0:
                    return generate_armor();
                case 1:
                    return generate_weapon();
                case 2:
                    return generate_talisman();
            }
            throw new Exception("Wrong item category");
        }

        /*public Item generate_item(){
            int type = rnd.Next(item_categories.Count);
            int item_index = rnd.Next(item_categories[type].Item1);
            switch (type){
                case 0:
                    return new Armor(level, item_categories[type].Item2[item_index]);
                case 1:
                    return new Weapon(level, item_categories[type].Item2[item_index]);
                case 2:
                    return new Talisman(level, item_categories[type].Item2[item_index]);
            }
            throw new Exception("Can't generate new item");

        }*/
    }
    abstract class Item
    {
        protected string item_name = "";
        protected int item_level;
        protected int bonus_defence ;
        protected int bonus_luck;
        protected int bonus_damage;

        protected int scale_level(){
            return item_level*3; //todo
        }

        public override string ToString()
        {
            return $"[{item_level}] {item_name} | [DEF: {bonus_defence} | LCK: {bonus_luck} | DMG: {bonus_damage}]";
        }

        public int get_defence(){
            return bonus_defence;
        }
        public int get_luck(){
            return bonus_luck;
        }
        public int get_damage(){
            return bonus_damage;
        }

        public string get_item_name(){
            return item_name;
        }
    }


    class Armor : Item
    {
        public Armor(){
            item_name = "Stick";
            item_level = 1;
            bonus_defence = 0;
            bonus_luck = 0;
            bonus_damage = 1;
        }

        public Armor(int level , string name){
            item_name = name;
            item_level = level;
            bonus_defence = 0;
            bonus_luck = 0;
            bonus_damage = scale_level();
        }


    }

    class Weapon : Item
    {
        public Weapon(){
            item_name = "Hauberk";
            item_level = 1;
            bonus_defence = 1;
            bonus_luck = 0;
            bonus_damage = 0;
        }

        public Weapon(int level , string name){
            item_name = name;
            item_level = level;
            bonus_damage = 0;
            bonus_luck = 0;
            bonus_defence = scale_level();
        }

    }

    class Talisman  : Item
    {
        public Talisman(){
            item_name = "Colored Stone";
            item_level = 1;
            bonus_defence = 0;
            bonus_luck = 1;
            bonus_damage = 0;
        }

        public Talisman(int level , string name){
            item_name = name;
            item_level = level;
            bonus_damage = 0;
            bonus_defence = 0;
            bonus_luck = scale_level();
        }

    }
}


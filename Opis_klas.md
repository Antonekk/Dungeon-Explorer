# Opis klas
Oznaczenia typów funkcji : ( Typy argumentów wejścia -> Typ argumentu wyjścia)

# Dungeon



## Funkcje publiczne

### menu (typ : (-> void))
Odpowiedzialna za wyświetlenie głównego menu gry

## Funkcje prywatne

### game_info (typ : (-> void))
Pobiera z pliku i wyświetla aktualny opis gry

### start_game (typ : (-> void))
Generuje w pętli nowe pokoje dopóki gra nie zostanie przerwana

### generate_new_room (typ : (-> Room))
Generuje nowy obiekt typu Room w zależności od obecnego stanu gry


# CTO

## Funkcje publiczne

### WritePlayerData (typ : (Player -> void))
Wypisuje dane gracza (zdrowie, statystyki, przedmioty) w górnej części terminalu

### ClearCurrentConsoleLine (typ : (-> void))
Czyści obecną linię w terminalu

### Write_Center (typ : (string -> void))
Wypisuje tekst na środku terminalu

# Room (abstrakcyjna)
Abstrakcyjna klasa pokoju po której dziedziczą : Fight_Room, Shop oraz Healing_fountain

## Funkcje publiczne

### start (typ : (Player -> void)) (abstrakcyjna)
Rozpoczyna akcje dostępne w pokoju

## Funkcje chronione

### is_dead_info (typ : (Player -> void))
W przypadku śmierci gracza wypisuje jego wynik i kończy działanie programu


# Fight_Room (dziedziczy po Room)
W tym pokoju odbywa się walka gracza z losowym przeciwnikiem.

## Funkcje publiczne

### start (typ : (Player -> void)) (override)
Rozpoczyna główną pętlę w której odbywa się walka gracza z potworem. Zostaje przerwana w przypadku śmierci gracza bądź potwora

## Funkcje prywatne

### generate_enemy (typ  ( -> Enemy))
Generuje i zwraca losowego przeciwnika




# Shop (dziedziczy po Room)
W tym pokoju gracz może za zabrane złoto zakupić nowe przedmioty do swojego ekwipunku

## Funkcje publiczne

### start (typ : (Player -> void)) (override)
Wyświetla dostępne do kupienia przedmioty oraz ich ceny i czeka na wybór gracza

## Funkcje prywatne

### add_items (typ : ( -> void))
Dodaje przedmioty i ich ceny do listy dostępnych do kupienia przedmiotów
### generate_price (typ : ( -> int))
Generuje po części losową cenę przedmiotu
### buy ( typ : (int Player -> void))
Jeśli gracz posiada odpowiednią ilość złota, dodaje wskazany przedmiot do ekwipunku gracza


# Healing_fountain (dziedziczy po Room)
Pokój w którym za drobną opłatą gracz może przywrócić swoje zdrowie do maksymalnego poziomu

## Funkcje publiczne

### start (typ : (Player -> void)) (override)
Wyświetla cenę oraz szansę na powodzenie uzdrawiania. Czeka na wybór gracza

## Funkcje prywatne

### buy_state (typ : (Player -> void))
Funkcja wywoływana kiedy gracz zdecyduje się na leczenie. Jeśli gracz ma wystarczająco złota oraz szczęście zostaje wyleczony.

### scale_price (typ : (int-> int))
Wylicza odpowiednią cenę za uzdrowienie

# Snake_Room (dziedziczy po Room)
Pokój w którym gracz poświęcając losową część zdrowia zyskuje określoną ilość złota

## Funkcje publiczne

### start (typ : (Player -> void)) (override)
Wyświetla zdrowie do poświęcenia oraz ilość złota którą może zystać gracz.

## Funkcje prywatne

### sacrifice_state (typ : (Player -> void))
Funkcja wywoływana kiedy gracz zdecyduje się na poświęcenie. Gracz poświęca część życia w zamian za złoto

### scale_gold (typ : (int-> int))
Wylicza odpowiednią nagrodę w zamian za poświęcenie zdrowia

# Character (abstrakcyjna)
Abstrakcyjna klasa Postaci po której dziedziczą : Player oraz Enemy

## Funkcje publiczne

### Atack (typ : (Character double-> void)) (wirtualna)
Podstawowa funkcja służąca do atakowania przeciwnika

### Recive_Damage (typ : (int-> void)) (wirtualna)
Podstawowa funkcja służąca do przyjmowania obrażeń

### is_dead (typ : (-> bool))
Zwraca prawdę, w przypadku śmierci postaci, fałsz wpp.

## Funkcje chronione

### scale_hp (typ : (-> void)) (wirtualna)
Wylicza odpowiednią ilość życia na podstawie poziomu postaci

### scale_luck (typ : (-> void)) (wirtualna)
Wylicza odpowiednią ilość szczęścia na podstawie poziomu postaci

### scale_defence (typ : (-> void)) (wirtualna)
Wylicza odpowiednią ilość obrony na podstawie poziomu postaci

### scale_damage (typ : (-> void)) (wirtualna)
Wylicza odpowiednią ilość obrażeń na podstawie poziomu postaci

# Player (dziedziczy po Character)
Podklasa postaci reprezentująca postać gracza

## Funkcje publiczne

### pay (typ : (int -> bool))
Jeśli gracz posiada odpowiednią ilość złota, zostaje ona zabrana oraz funkcja zwraca Prawdę, Fałsz wpp.

### add_gold (typ : (int -> void))
Dodaje podaną ilość złota do zasobów gracza

### equip_item (typ : (Item-> void))
W przypadku nowego typu broni dodaje podaną broń do ekwipunku gracza, w przeciwnym przypadku wyrzuca stary przedmiot i dodaje nowy.

### heal (typ : (-> void))
Leczy gracz do maksymalnego poziomu zdrowia

### is_in_danger (typ : (-> bool))
Sprawdza czy poziom zdrowia jest poniżej połowy

### run_away (typ : (-> void))
Odejmuje 10% maksymalnego zdrowia gracza oraz wywołuje funkcję **lose_gold**()

### add_exp (typ : (int -> void))
Dodaje podaną ilość doświadczenia graczowi. Zajmuje się również odpowiednim ustawianiem poziomu w razie potrzeby


### Atack (typ : (Character double-> void)) (override)
Zadaje obrażenia z dodatkowo podanym bonusem podanej postaci

### Recive_Damage (typ : (int-> void)) (override)
Przyjmuje obrażenia, niwelując część z nich za pomocą obrony oraz z szansą na unik



## Funkcje prywatne

### add_item (typ : (Item-> void))
Dodaje statystyki przedmiotu do statystyk gracza

### remove_item (typ : (Item-> void))
Odejmuje statystyki przedmiotu do statystyk gracza

### lose_gold (typ : (-> void))
Odejmuje pewną część złota zgromadzonego przez gracza

### scale_exp (typ : (-> void))
Wylicza odpowiednią ilość doświadczenia potrzebnego do kolejnego poziomu na podstawie poziomu postaci


# Enemy (dziedziczy po Character) (abstrakcyjna)
Podklasa postaci reprezentująca przeciwnika pojawiającego się w pokojach walki

## Funkcje publiczne

### drop_gold (typ : (-> int))
Jeśli przeciwnik nie żyje zwraca przypisaną ilość złota, błąd wpp.

### get_class_name(typ : (-> string))
Zwraca przypisaną nazwę klasy

## Funkcje chronione

### set_gold (typ : (-> void))
Ustawia (na podstawie poziomu oraz elementu losowego) ilość złota którą upuści przeciwnik po śmierci

### scale_hp (typ : (-> void)) (override)
Zmienia formułę na wyliczanie odpowiedniej ilości życia na podstawie poziomu postaci

### scale_luck (typ : (-> void)) (override)
Zmienia formułę na wyliczanie odpowiedniej ilości szczęścia na podstawie poziomu postaci

### scale_defence (typ : (-> void)) (override)
Zmienia formułę na wyliczanie odpowiedniej ilości obrony na podstawie poziomu postaci

### scale_damage (typ : (-> void)) (override)
Zmienia formułę na wyliczanie odpowiedniej ilości obrażeń na podstawie poziomu postaci


# Orc (dziedziczy po Enemy)
Klasa reprezentująca orka, potwora potrafiącego korzystać z obrony

## Funkcje publiczne

### Recive_Damage (typ : (int-> void)) (override)
Przyjmuje obrażenia, niwelując część z nich za pomocą obrony


# Skeleton (dziedziczy po Enemy)
Klasa reprezentująca szkieleta, potwora potrafiącego korzystać atakować dwa razy

## Funkcje publiczne


### Atack (typ : (Character double-> void)) (override)
Zadaje obrażenia z dodatkową szansą na zadanie ich dwukrotnie

# Goblin (dziedziczy po Enemy)
Klasa reprezentująca goblina, potwora potrafiącego unikać ataków gracza

## Funkcje publiczne

### Recive_Damage (typ : (int-> void)) (override)
Przyjmuje obrażenia, z pewną szansą na ich uniknięcie


# Item_Generator
Służy do generowania obiektów różnych podklas klasy **Item** nadając im nazwy z bazy

## Funkcje publiczne

### generate_item (typ : (int -> Item))
Generuje przedmiot o podanym indeksie. W przypadku złego indeksu zwraca błąd

## Funkcje prywatne

### init_len (typ : ( -> void))
Inicjalizuje długości tablic z nazwami przedmiotów

### generate_armor (typ : ( -> Armor))
Generuje nowy obiekt klasy Armor o odpowiednim poziomie wraz z losową nazwą

### generate_weapon (typ : ( -> Weapon))
Generuje nowy obiekt klasy Weapon o odpowiednim poziomie wraz z losową nazwą

### generate_talisman (typ : ( -> Talisman))
Generuje nowy obiekt klasy Talisman o odpowiednim poziomie wraz z losową nazwą




# Item (abstrakcyjna)
Posiadając dany przedmiot gracz otrzyma przypisane do niego statystyki. Można posiadać tylko jeden przedmiot danego typu (Armor, Weapon, Talisman)

## Funkcje publiczne

### ToString(typ : (-> string)) (override)
Zwraca tekstową reprezentację przedmiotu

## Funkcje chronione

### scale_level (typ : (-> int))
Zwraca wartość statystyki w zależności od poziomu

### scale_side (typ : (-> int))
Zwraca wartość dla pobocznej statystyki w zależności od poziomu oraz losowości


# Armor (dziedziczy po Item)
Klasa dziedzicząca po klasie Item. W której obrona wyliczana jest funkcją **scale_level()** natomiast pozostałe statystyki funkcją **scale_side()**

# Weapon (dziedziczy po Item)
Klasa dziedzicząca po klasie Item. W której obrażenia wyliczane są funkcją **scale_level()** natomiast pozostałe statystyki funkcją **scale_side()**

# Talisman (dziedziczy po Item)
Klasa dziedzicząca po klasie Item. W której szczęście wyliczane jest funkcją **scale_level()** natomiast pozostałe statystyki funkcją **scale_side()**

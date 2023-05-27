namespace cto
{
    class CTO
    {

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

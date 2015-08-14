namespace Generics
{
    class Program
    {
        static void Main()
        {
            var numList = new CustomList<int>(8);
            numList.Add(5);
            numList.Add(11);
            numList.Add(2);
            numList.Add(4);

            numList.Print();

            var nameList = new CustomList<string>();
            nameList.Add("Joro");
            nameList.Add("Gunio");
            nameList.Add("Penio");
            nameList.Add("Pesho");

            nameList.Print();
        }
    }
}

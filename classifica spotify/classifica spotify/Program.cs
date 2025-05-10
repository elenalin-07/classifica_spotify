string[] spotify = File.ReadAllLines("classifica.txt");

List<string> list = new List<string>(spotify);
list.RemoveAt(0);

StreamWriter writer1 = new StreamWriter("titoli_artista.txt");
StreamWriter writer2 = new StreamWriter("super_streams.txt");

Console.WriteLine("inserisci il nome di un artista");
string nome = Console.ReadLine();

foreach (string s in list)
{
    string[] riga = s.Split('|');

        if (s.Contains(nome))
        {
            writer1 = new StreamWriter($"{riga[1]} - {riga[2]}");
            Console.WriteLine($"{riga[1]} - {riga[2]}");
        }
        if (long.Parse(riga[4]) > 3000000000)
        {
            writer2.WriteLine(s);
        }
    
}

bool controllo(string n,  string[] s)
{
    bool check = false;
    while (!check)
    {
        if (s.Contains(n))
        {
            check = true;
            return true;
        }
        else
        {
            Console.WriteLine("errore");
            n = Console.ReadLine();
        }
    }
    return false;
}
writer1.Close();
writer2.Close();
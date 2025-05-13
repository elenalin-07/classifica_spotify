string[] spotify = File.ReadAllLines("classifica.txt");

List<string> list = new List<string>(spotify);
list.RemoveAt(0);

StreamWriter writer1 = new StreamWriter("titoli_artista.txt");
StreamWriter writer2 = new StreamWriter("super_streams.txt");
StreamWriter writer3 = new StreamWriter("anni.txt");

void titoli (ref string art, ref string anno, List<string> list, string nome)
{
    foreach (string s in list)
    {
        string[] riga = s.Split('|');
        if (s.Contains(nome))
        {
            writer1.WriteLine($"{riga[1]} - {riga[2]}");
            Console.WriteLine($"{riga[1]} - {riga[2]}");
        }
        if (long.Parse(riga[4]) > 3000000000)
        {
            writer2.WriteLine(riga[1]);
        }
        if (riga[2].Contains("&"))
        {
            string[] a4 = riga[2].Split(" & ");
            foreach (string x in a4)
            {
                if (!art.Contains(x))
                {
                    art += x + ",";
                }
            }
        }
        else if (!art.Contains(riga[2]))
        {
            art += riga[2] + ",";
        }
        if (!anno.Contains(riga[3]))
        {
            anno += riga[3] + ",";
        }
    }
}

void brani(List<string> list, List<string> artista, ref int[] n_ar)
{
    foreach (string s in list)
    {
        string[] riga = s.Split('|');
        for (int i = 0; i < artista.Count; i++)
        {
            if (riga[2].Contains("&"))
            {
                string[] a4 = riga[2].Split(" & ");

                for (int j = 0; j < a4.Length; j++)
                {
                    if (a4[j] == artista[i])
                    {
                        n_ar[i]++;
                    }
                }
            }
            else if (riga[2] == artista[i])
            {
                n_ar[i]++;
            }
        }
    }
}

void stampa(List<int> a, List<string> list)
{
    foreach (int x in a)
    {
        writer3.WriteLine($"Anno:{x}");

        foreach (string s in list)
        {
            string[] riga = s.Split('|');
            if (x.ToString() == riga[3])
            {
                writer3.WriteLine($"{riga[1]} - {riga[2]}");
            }
        }
        writer3.WriteLine();
    }
}

Console.WriteLine("inserisci il nome di un artista");
string nome = Console.ReadLine();

string art = "";
string anno = "";

titoli(ref art, ref anno, list, nome);

art = art.Substring(0, art.Length - 1);
anno = anno.Substring(0, anno.Length - 1);

List<string> a1 = new List<string>(anno.Split(","));
List<int> a = new List<int>();

foreach (string s in a1)
{
    a.Add(int.Parse(s));
}

a.Sort();

List<string> artista = new List<string>(art.Split(","));
int[] num_ar = new int[artista.Count];

brani(list, artista, ref num_ar);

int max = 0;
List<int> n_ar = new List<int>(num_ar);
foreach (int s in n_ar)
{
    if (s > max)
    {
        max = s;
    }
}

string max_ar = artista[n_ar.IndexOf(max)];

Console.WriteLine();

for (int i = 0; i < artista.Count; i++)
{
    Console.WriteLine($"{artista[i]} = {n_ar[i]}");
}

Console.WriteLine($"artista con più brani in classifica: {max_ar}");
Console.WriteLine();

stampa(a, list);

writer1.Close();
writer2.Close();
writer3.Close();